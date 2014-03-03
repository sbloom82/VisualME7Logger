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
        public short SamplesPerSecond { get; private set; }
        public DateTime LogStarted { get; private set; }
        internal CommunicationInfo() 
        {
            Connect = string.Empty;
            Communicate = string.Empty;
            LogSpeed = string.Empty;
            SamplesPerSecond = 0;
            LogStarted = DateTime.MinValue;
        }

        internal bool Complete { get; private set; }
        internal void ReadLine(string line, bool fromLog = false)
        {
            if (fromLog)
            {
                string[] parts = line.Split(':');
                if (parts.Length >= 2)
                { 
                    parts[0] = parts[0].Trim();
                    parts[1] = parts[1].Trim();
                    if (parts[0] == "Log packet size")
                    {
 
                    }
                    else if (parts[0] == "Logging with")
                    {
                        try
                        {
                            SamplesPerSecond = short.Parse(parts[1].Replace("samples/second", "").Trim());
                        }
                        catch { }
                    }
                    else if(parts[0] == "Used speed is")
                    {
                        LogSpeed = parts[1];
                    }
                    else if (parts[0] == "Used mode is")
                    {
                        Communicate = parts[1];
                    }
                    else if (parts[0] == "Log started at")
                    {
                        DateTime started;
                        if(DateTime.TryParse(line.Replace("Log started at:", "").Trim(), out started))
                            LogStarted = started;
                        Complete = true;
                    }
                }
            }
            else
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
}
