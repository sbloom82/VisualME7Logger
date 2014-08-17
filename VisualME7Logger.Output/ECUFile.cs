using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisualME7Logger.Common;
using System.IO;
using System.Diagnostics;

namespace VisualME7Logger.Configuration
{
    public class ECUFile
    {
        public VersionInfo VersionInfo { get; private set; }
        public CommunicationInfo CommunicationInfo { get; private set; }
        public IdentificationInfo IdentificationInfo { get; private set; }
        public Measurements Measurements { get; private set; }

        public string FilePath { get; private set; }
        public string FileName { get { return Path.GetFileName(this.FilePath); } }

        public ECUFile(string filePath)
        {
            this.FilePath = filePath;
            VersionInfo = new VersionInfo();
            CommunicationInfo = new CommunicationInfo();
            IdentificationInfo = new IdentificationInfo();
            Measurements = new Measurements();
        }

        public void Open()
        {
            VersionInfo = null;
            CommunicationInfo = null;
            IdentificationInfo = null;
            Measurements = null;

            try
            {
                using (StreamReader sr = new StreamReader(this.FilePath, Encoding.UTF7))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (VersionInfo == null && line == "[Version]")
                        {
                            VersionInfo = new VersionInfo();
                        }
                        else if (VersionInfo != null && !VersionInfo.Complete)
                        {
                            VersionInfo.ReadLine(line);
                        }
                        else if (CommunicationInfo == null && line == "[Communication]")
                        {
                            CommunicationInfo = new CommunicationInfo();
                        }
                        else if (CommunicationInfo != null && !CommunicationInfo.Complete)
                        {
                            CommunicationInfo.ReadLine(line);
                        }
                        else if (IdentificationInfo == null && line == "[Identification]")
                        {
                            IdentificationInfo = new IdentificationInfo();
                        }
                        else if (IdentificationInfo != null && !IdentificationInfo.Complete)
                        {
                            IdentificationInfo.ReadLine(line);
                        }
                        else if (Measurements == null && line == "[Measurements]")
                        {
                            Measurements = new Measurements();
                        }
                        else if (Measurements != null && !Measurements.Complete)
                        {
                            Measurements.ReadLine(line);
                        }
                    }
                }
            }
            catch { }

            if (VersionInfo == null)
            {
                VersionInfo = new VersionInfo();
            }
            if (CommunicationInfo == null)
            {
                CommunicationInfo = new CommunicationInfo();
            }
            if (IdentificationInfo == null)
            {
                IdentificationInfo = new IdentificationInfo();
            }
            if (Measurements == null)
            {
                Measurements = new Measurements();
            }
        }

        public void UpdateCommunicationInfo(string connect, string communicate, string logSpeed)
        {
            bool communicateUpdated = false;
            bool connectUpdated = false;
            bool logSpeedUpdated = false;

            byte[] bytes;
            using (FileStream reader = new FileStream(this.FilePath, FileMode.Open))
            {
                bytes = new byte[(int)reader.Length];
                reader.Read(bytes, 0, (int)reader.Length);
            }

            byte[] writeBytes = new byte[bytes.Length];
            Array.Copy(bytes, writeBytes, bytes.Length);

            for (int i = 0; i < bytes.Length; ++i)
            {
                byte[] compare = new byte[28];
                for (int j = 0; j < compare.Length; ++j)
                {
                    if (bytes.Length > i + j)
                    {
                        compare[j] = bytes[i + j];
                    }
                }

                if (ArraysEqual(compare, this.CommunicationInfo.ConnectBytes))
                {
                    this.CommunicationInfo.Connect = connect;
                    Array.Copy(this.CommunicationInfo.ConnectBytes, 0, writeBytes, i, this.CommunicationInfo.ConnectBytes.Length);
                    connectUpdated = true;   
                }
                else if (ArraysEqual(compare, this.CommunicationInfo.CommunicateBytes))
                {
                    this.CommunicationInfo.Communicate = communicate;
                    Array.Copy(this.CommunicationInfo.CommunicateBytes, 0, writeBytes, i, this.CommunicationInfo.CommunicateBytes.Length);
                    communicateUpdated = true;
                }
                else if (ArraysEqual(compare, this.CommunicationInfo.LogSpeedBytes))
                {
                    this.CommunicationInfo.LogSpeed = logSpeed;
                    Array.Copy(this.CommunicationInfo.LogSpeedBytes, 0, writeBytes, i, this.CommunicationInfo.LogSpeedBytes.Length);
                    logSpeedUpdated = true;
                    break;
                }
            }

            using (FileStream writer = new FileStream(this.FilePath, FileMode.Create, FileAccess.Write))
            {
                writer.Write(writeBytes, 0, writeBytes.Length);
            }

            if (!connectUpdated || !communicateUpdated || !logSpeedUpdated)
            {
                throw new InvalidDataException("[Communication] settings failed to update properly in ecu file.");
            }
        }

        static bool ArraysEqual(byte[] a1, byte[] a2)
        {
            if (a1 == a2)
                return true;

            if (a1 == null || a2 == null)
                return false;

            if (a1.Length != a2.Length)
                return false;

            for (int i = 0; i < a1.Length; i++)
            {
                if (a1[i] != a2[i])
                    return false;
            }
            return true;
        }

        public static ECUFile Create(string ME7LoggerDirectory, string imageFilePath)
        {
            using (Process p = new Process())
            {
                p.StartInfo = new ProcessStartInfo(Path.Combine(ME7LoggerDirectory, "bin\\ME7Info.exe"), "\"" + imageFilePath + "\"");
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.RedirectStandardInput = true;
                p.Start();
                p.WaitForExit(10000);

                if (p.ExitCode == 0)
                {
                    string line;
                    while ((line = p.StandardError.ReadLine()) != null)
                    {
                        if (line.StartsWith("written output to file "))
                        {
                            return new ECUFile(line.Replace("written output to file ", string.Empty));
                        }
                    }
                }
                else
                {
                    throw new Exception(p.StandardError.ReadToEnd());
                }
            }
            throw new Exception("Unknown error occurred");
        }
    }

    public class ConfigFile
    {
        public Measurements Measurements { get; private set; }
        public string ECUCharacteristics { get; private set; }
        public short SamplesPerSecond { get; private set; }
        public string FilePath { get; set; }

        public ConfigFile(string filePath)
        {
            this.FilePath = filePath;
            this.Measurements = new Measurements();
        }
        public ConfigFile(string filePath, string ecuCharacteristics)
            : this(filePath)
        {
            this.ECUCharacteristics = ecuCharacteristics;
        }
        public ConfigFile(string filePath, string ecuCharacteristics, Measurements measurements)
            : this(filePath, ecuCharacteristics)
        {
            this.Measurements = measurements;
        }

        public void Write()
        {
            using (StreamWriter writer = new StreamWriter(this.FilePath))
            {
                writer.WriteLine("[Configuration]");
                writer.WriteLine("ECUCharacteristics = {0}", ECUCharacteristics);
                writer.WriteLine("SamplesPerSecond = 20");
                writer.WriteLine("");
                writer.WriteLine("[LogVariables]");
                writer.WriteLine(";Name            [Alias]                             [; Comment]");

                foreach (Measurement m in Measurements.Values)
                {
                    writer.WriteLine("{0};{1}; {2}",
                        m.Name.PadRight(16),
                        m.Alias.PadRight(37),
                        m.Comment);
                }
            }
        }

        public void Read()
        {
            this.Measurements = null;
            try
            {
                using (StreamReader reader = new StreamReader(this.FilePath, Encoding.UTF7))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            line = line.Trim();
                            if (!line.StartsWith(";"))
                            {
                                if (this.Measurements == null && line == "[LogVariables]")
                                {
                                    this.Measurements = new Measurements();
                                }
                                else if (this.Measurements != null && !this.Measurements.Complete)
                                {
                                    this.Measurements.ReadLine(line);
                                }
                                else if (line.StartsWith("ECUCharacteristics"))
                                {
                                    this.ECUCharacteristics = line.Split('=')[1].Trim();
                                }
                                else if (line.StartsWith("SamplesPerSecond"))
                                {
                                    try
                                    {
                                        this.SamplesPerSecond = short.Parse(line.Split('=')[1].Trim());
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                }
            }
            catch { }

            if (Measurements == null)
            {
                Measurements = new Measurements();
            }
        }
    }

    public class Measurements
    {
        private List<Measurement> measurements = new List<Measurement>();
        private Dictionary<string, Measurement> measurementsByName = new Dictionary<string, Measurement>();
        public IEnumerable<Measurement> Values
        {
            get { return this.measurements; }
        }
        public Measurement this[string name]
        {
            get
            {
                if (this.measurementsByName.ContainsKey(name))
                {
                    return measurementsByName[name];
                }
                return null;
            }
        }

        internal bool Complete { get; private set; }
        internal void ReadLine(string line)
        {
            if (!string.IsNullOrWhiteSpace(line) &&
                !line.TrimStart().StartsWith(";"))
            {
                Measurement m = new Measurement();
                if (m.Read(line))
                {
                    this.AddMeasurement(m);
                }
            }
        }

        public void AddMeasurement(Measurement m)
        {
            this.measurements.Add(m);
            this.measurementsByName[m.Name] = m;
        }
    }

    public class Measurement
    {
        public string Name { get; private set; }
        public string Alias { get; private set; }
        public string Address { get; private set; }
        public short Size { get; private set; }
        public string BitMask { get; private set; }
        public string Unit { get; private set; }
        public bool Signed { get; private set; }
        public bool Inverse { get; private set; }
        public decimal Factor { get; private set; }
        public decimal Offset { get; private set; }
        public string Comment { get; private set; }

        public bool Selected { get; set; }

        internal bool Read(string line)
        {
            string[] parts = line.Split(',');
            if (parts.Length >= 11)
            {
                Name = parts[0].Trim();
                Alias = parts[1].Trim().Replace("{", "").Replace("}", "");
                Address = parts[2].Trim();
                try { Size = short.Parse(parts[3].Trim()); }
                catch { }
                BitMask = parts[4].Trim();
                Unit = parts[5].Trim().Replace("{", "").Replace("}", "");
                Signed = parts[6].Trim() == "1";
                Inverse = parts[7].Trim() == "1";
                try { Factor = decimal.Parse(parts[8].Trim()); }
                catch { }
                try { Offset = decimal.Parse(parts[9].Trim()); }
                catch { }
                Comment = parts[10].Trim().Replace("{", "").Replace("}", "");
                return true;
            }
            else
            {
                this.Name = line;
                if (Name.IndexOf(" ") > -1)
                    this.Name = Name.Substring(0, Name.IndexOf(" "));
                return true;
            }
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Alias))
            {
                return string.Format("{0} - ({1})",
                    this.Alias,
                    this.Name);
            }

            return string.Format("{0}",
                this.Name);
        }
    }

    public class VersionInfo
    {
        public string Version { get; private set; }

        internal bool Complete { get; private set; }
        internal void ReadLine(string line)
        {
            string[] parts = line.Split('=');
            if (parts.Length == 2)
            {
                parts[0] = parts[0].Trim();
                parts[1] = parts[1].Trim().Trim('"');
                if (parts[0] == "Version")
                {
                    Version = parts[1];
                    this.Complete = true;
                }
                return;
            }
            throw new Exception("Invalid line for [Version]");
        }
    }
}
