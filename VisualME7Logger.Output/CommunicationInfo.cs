using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualME7Logger.Common
{
    public class CommunicationInfo
    {
        private string connect;
        public string Connect { get { return connect; } internal set { connect = value; connectBytes = null; } }
        private List<string> connectPossibleValues = new List<string>();
        public IEnumerable<string> ConnectPossibleValues { get { return connectPossibleValues; } }
        byte[] connectBytes = null;
        public byte[] ConnectBytes
        {
            get
            {
                if (connectBytes == null)
                {
                    connectBytes = GetBytes(string.Format("Connect      = {0}", Connect.PadRight(13)));
                }
                return connectBytes;
            }
        }

        private string communicate;
        public string Communicate { get { return communicate; } internal set { communicate = value; communicateBytes = null; } }
        private List<string> communicatePossibleValues = new List<string>();
        public IEnumerable<string> CommunicatePossibleValues { get { return communicatePossibleValues; } }
        byte[] communicateBytes;
        public byte[] CommunicateBytes
        {
            get
            {
                if (communicateBytes == null)
                {
                    communicateBytes = GetBytes(string.Format("Communicate  = {0}", Communicate.PadRight(13)));
                }
                return communicateBytes;
            }
        }

        private string logSpeed;
        public string LogSpeed { get { return logSpeed; } internal set { logSpeed = value; logSpeedBytes = null; } }
        private List<string> logSpeedPossibleValues = new List<string>();
        public IEnumerable<string> LogSpeedPossibleValues { get { return logSpeedPossibleValues; } }
        byte[] logSpeedBytes;
        public byte[] LogSpeedBytes
        {
            get
            {
                if (logSpeedBytes == null)
                {
                    logSpeedBytes = GetBytes(string.Format("LogSpeed     = {0}", LogSpeed.PadRight(13)));
                }
                return logSpeedBytes;
            }
        }

        static byte[] GetBytes(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

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
                            SamplesPerSecond = short.Parse(parts[1].Substring(0, 2).Trim());
                        }
                        catch { }
                    }
                    else if (parts[0] == "Used speed is")
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
                        if (DateTime.TryParse(line.Replace("Log started at:", "").Trim(), out started))
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

                    string[] possibleValues = new string[0];
                    string[] split = line.Split(';');
                    if (split.Length == 2)
                    {
                        split = split[1].Split(':');
                        if (split.Length == 2)
                        {
                            possibleValues = split[1].Trim().Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                        }
                    }

                    if (parts[0] == "Connect")
                    {
                        Connect = parts[1];
                        connectPossibleValues = possibleValues.ToList();
                    }
                    else if (parts[0] == "Communicate")
                    {
                        Communicate = parts[1];
                        communicatePossibleValues = possibleValues.ToList();
                    }
                    else if (parts[0] == "LogSpeed")
                    {
                        LogSpeed = parts[1];
                        logSpeedPossibleValues = possibleValues.ToList();
                        this.Complete = true;
                    }

                    return;
                }
                throw new Exception("Invalid line for [Communication]");
            }
        }
    }
}
