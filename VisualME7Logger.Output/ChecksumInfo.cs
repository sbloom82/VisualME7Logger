using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Diagnostics;

namespace VisualME7Logger.Output
{
    public class ChecksumInfo
    {
        public string ApplicationPath = string.Empty;
        public string BinPath = string.Empty;        

        public void Read(XElement element)
        {
            foreach (XAttribute att in element.Attributes())
            {
                switch (att.Name.LocalName)
                {
                    case "ApplicationPath":
                        this.ApplicationPath = att.Value;
                        break;
                    case "BinPath":
                        this.BinPath = att.Value;
                        break;
                }
            }
        }

        public XElement Write()
        {
            XElement ele = new XElement("ChecksumInfo");
            ele.Add(new XAttribute("ApplicationPath", this.ApplicationPath));
            ele.Add(new XAttribute("BinPath", this.BinPath));
            return ele;
        }

        public ChecksumResult Check()
        {
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(
               this.ApplicationPath, 
               "\"" + this.BinPath + "\"");
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            if (p.Start())
            {
                string output = p.StandardOutput.ReadToEnd();
                string error = p.StandardError.ReadToEnd();
                return new ChecksumResult() { Success = p.ExitCode == 0, Output = output + "\r\n" + error };
            }
            else
            {
                return new ChecksumResult() { Success = false, Output = "Unable to start process" };
            }
        }
    }

    public class ChecksumResult
    {
        public bool Success { get; set; }
        public string Output { get; set; }
    }
}
