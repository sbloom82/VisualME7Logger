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
        }
        
        public void Open()
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

        public ConfigFile(string ecuCharacteristics)
        {
            this.ECUCharacteristics = ecuCharacteristics;
        }
        public ConfigFile(string ecuCharacteristics, Measurements measurements)
            : this(ecuCharacteristics)
        {
            this.Measurements = measurements;
        }

        public void Write(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
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

        public void Read(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath, Encoding.UTF7))
            {
                this.Measurements = null;
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
