using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualME7Logger.Common
{
    public class CommunicationInfo
    {
        public string Connect { get; private set; }
        public string Communicate { get; private set; }
        public string LogSpeed { get; private set; }
        internal CommunicationInfo() { }

        internal bool Complete { get; private set; }
        internal void ReadLine(string line)
        {
            string[] parts = line.Split(';')[0].Split('=');
            if (parts.Length == 2)
            {
                parts[0] = parts[0].Trim();
                parts[1] = parts[1].Trim();
                if (parts[0] == "Connect")
                {
                    Connect = parts[1];
                }
                else if (parts[0] == "Communicate")
                {
                    Communicate = parts[1];
                }
                else if (parts[0] == "LogSpeed")
                {
                    LogSpeed = parts[1];
                    this.Complete = true;
                }
                return;
            }
            throw new Exception("Invalid line for [Communication]");
        }
    }
}
