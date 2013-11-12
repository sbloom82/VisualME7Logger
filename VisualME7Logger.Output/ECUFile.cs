using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisualME7Logger.Common;
using System.IO;

namespace VisualME7Logger.Configuration
{
    public class ECUFile
    {
        public VersionInfo VersionInfo { get; private set; }
        public CommunicationInfo CommunicationInfo { get; private set; }
        public IdentificationInfo IdentificationInfo { get; private set; }
        public Measurements Measurements { get; private set; }

        public string FilePath { get; private set; }
        public ECUFile(string filePath)
        {
            this.FilePath = filePath;
        }

        public bool Open()
        {
            try
            {
                using (StreamReader sr = new StreamReader(this.FilePath))
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
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }

    public class Measurements
    {
        private List<Measurement> measurements = new List<Measurement>();
        public IEnumerable<Measurement> Values
        {
            get { return this.measurements; }
        }

        internal bool Complete { get; private set; }
        internal void ReadLine(string line)
        {
            if (!string.IsNullOrEmpty(line) &&
                !line.TrimStart().StartsWith(";"))
            {
                Measurement m = new Measurement();
                if (m.Read(line))
                    measurements.Add(m);
            }
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

        internal bool Read(string line)
        {
            string[] parts = line.Split(',');
            if (parts.Length >= 11)
            {
                Name = parts[0].Trim();
                Alias = parts[1].Trim().Replace("{", "").Replace("}", "");
                Address = parts[2].Trim();
                Size = short.Parse(parts[3].Trim());
                BitMask = parts[4].Trim();
                Unit = parts[5].Trim().Replace("{", "").Replace("}", "");
                Signed = parts[6].Trim() == "1";
                Inverse = parts[7].Trim() == "2";
                try { Factor = decimal.Parse(parts[8].Trim()); }
                catch { }
                Offset = decimal.Parse(parts[9].Trim());
                Comment = parts[10].Trim().Replace("{", "").Replace("}", "");
                return true;
            }
            return false;
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
