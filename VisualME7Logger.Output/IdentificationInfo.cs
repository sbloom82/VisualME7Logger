using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualME7Logger.Common
{
    public class IdentificationInfo
    {
        public string HWNumber { get; private set; }
        public string SWNumber { get; private set; }
        public string PartNumber { get; private set; }
        public string EngineId { get; private set; }

        internal IdentificationInfo() { }

        internal bool Complete { get; private set; }
        internal void ReadLine(string line)
        {
            string[] parts = line.Split('=');
            if (parts.Length == 2)
            {
                parts[0] = parts[0].Trim();
                parts[1] = parts[1].Trim().Trim('"').TrimStart('{').TrimEnd('}');
                if (parts[0] == "HWNumber")
                {
                    HWNumber = parts[1];
                }
                else if (parts[0] == "SWNumber")
                {
                    SWNumber = parts[1];
                }
                else if (parts[0] == "PartNumber")
                {
                    PartNumber = parts[1];
                }
                else if (parts[0] == "EngineId")
                {
                    EngineId = parts[1];
                    this.Complete = true;
                }
                return;
            }
            throw new Exception("Invalid line for [Identification]");
        }
    }
}
