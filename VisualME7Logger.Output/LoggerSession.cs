using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using VisualME7Logger.Log;

namespace VisualME7Logger.Session
{
    public class ME7LoggerSession
    {
        public enum Statuses
        {
            New = 0,
            Opening = 1,
            Open = 2,
            Closing = 3,
            Closed = 4
        }

        public enum SessionTypes
        {
            RealTime,
            File
        }

        public delegate void LoggerSessionStatusChanged(Statuses status);

        private Statuses _status;
        public Statuses Status
        {
            get { return _status; }
            private set
            {
                bool changed = value != _status;
                this._status = value;
                if (changed && StatusChanged != null)
                {
                    StatusChanged(this._status);
                }
            }
        }
        public LoggerSessionStatusChanged StatusChanged;
        public SessionTypes SessionType { get; private set; }
        public CommunicationInfo CommunicationInfo { get; private set; }
        public IdentificationInfo IdentificationInfo { get; private set; }
        public SessionVariables Variables { get; private set; }
        public ME7LoggerLog Log { get; private set; }

       
        private string parameters;
        private string configFilePath;
        public ME7LoggerSession(string parameters, string configFilePath, bool ey)
        {
            this.Status = Statuses.New;
            this.SessionType = SessionTypes.RealTime;
            this.Log = new ME7LoggerLog(this);
            this.parameters = parameters;
            this.configFilePath = configFilePath;
        }

        private string sessionTextFilePath;
        public ME7LoggerSession(string sessionTextFilePath, string logFilePath)
        {
            this.Status = Statuses.New;
            this.SessionType = SessionTypes.File;
            this.Log = new ME7LoggerLog(this, logFilePath);
            this.sessionTextFilePath = sessionTextFilePath;
        }

        Process p;
        public string CommandLine
        {
            get { return @"C:\ME7Logger.exe -p COM1 -s 10 -R C:\ME7Logger\logs\Allroad-Config.cfg"; }
        }
        public int ExitCode { get; private set; }
        public string ErrorText { get; private set; }
        public void Open()
        {
            this.Status = Statuses.Opening;

            if (this.SessionType == SessionTypes.File)
            {
                using (StreamReader sr = new StreamReader(this.sessionTextFilePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (this.ReadLine(line))
                            break;
                    }
                }
                this.Status = Statuses.Open;
                this.Log.Open();
            }
            else
            {
                logReady = false;
                p = new Process();
                p.StartInfo = new ProcessStartInfo("C:\\ME7Logger\\bin\\ME7Logger.exe", string.Format("{0} {1}", parameters, configFilePath));
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.RedirectStandardInput = true;
                p.EnableRaisingEvents = true;
                p.Exited += p_Exited;
                p.OutputDataReceived += p_OutputDataReceived;
                p.ErrorDataReceived += p_ErrorDataReceived;
                p.Start();
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();
            }
        }
        
        void p_Exited(object sender, EventArgs e)
        {
            this.ExitCode = p.ExitCode;
            this.Close();
        }

        void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            ErrorText += string.Format("{0}{1}", string.IsNullOrEmpty(ErrorText) ? string.Empty : Environment.NewLine, e.Data);
        }

        public bool logReady = false;
        void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (this.Status == Statuses.Opening)
            {
                if (this.ReadLine(e.Data))
                {
                    this.Status = Statuses.Open;
                    this.Log.Open();
                }
            }
            else if (this.Status == Statuses.Open)
            {
                if (!logReady && e.Data == "Press ^C to stop logging")
                {
                    logReady = true;
                }
                else if (logReady)
                {
                    this.Log.ReadLine(e.Data);
                }
            }
        }

        public void Close()
        {
            Status = Statuses.Closing;
            if (p != null)
            {
                if (!p.HasExited)
                {
                    p.Kill();
                    //don't know how to send in control + c;
                    //string end = "\x3";
                    //p.StandardInput.WriteLine(end);
                    //p.StandardInput.Close();
                    return;
                }
                p.Close();
                p.Dispose();
            }

            this.Log.Close();
            Status = Statuses.Closed;
        }

        private string logConfigFile;
        private string ecuCharacteristicsFile;
        private string ecuDef;

        public short SamplesPerSecond { get; private set; }
        private bool ReadLine(string line)
        {
            if (line == null)
            {
                //nothing
            }
            else if (string.IsNullOrEmpty(logConfigFile))
            {
                logConfigFile = line;
            }
            else if (string.IsNullOrEmpty(ecuCharacteristicsFile))
            {
                ecuCharacteristicsFile = line;
            }
            else if (string.IsNullOrEmpty(ecuDef))
            {
                ecuDef = line;
            }
            else if (CommunicationInfo == null)
            {
                //[Communication]
                CommunicationInfo = new CommunicationInfo();
            }
            else if (!CommunicationInfo.Complete)
            {
                CommunicationInfo.ReadLine(line);
            }
            else if (IdentificationInfo == null)
            {
                //[Identification]
                IdentificationInfo = new IdentificationInfo();
            }
            else if (!IdentificationInfo.Complete)
            {
                IdentificationInfo.ReadLine(line);
            }
            else if (Variables == null)
            {
                //logged variables are:
                Variables = new SessionVariables();
            }
            else if (!Variables.Complete)
            {
                Variables.ReadLine(line);
            }
            else if (line.StartsWith("-> Start logging"))
            {
                //TODO
                SamplesPerSecond = 10;
                return true;
            }
            return false;
        }
    }
    public class CommunicationInfo
    {
        public string Connect { get; private set; }
        public string Communicate { get; private set; }
        public string LogSpeed { get; private set; }
        internal CommunicationInfo() { }

        internal bool Complete { get; private set; }
        internal void ReadLine(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                this.Complete = true;
                return;
            }

            string[] parts = line.Split('=');
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
                }
                return;
            }
            throw new Exception("Invalid line for [Communication]");
        }
    }

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
            if (string.IsNullOrEmpty(line))
            {
                Complete = true;
            }

            //TODO
        }
    }

    public class SessionVariable
    {
        public int Number { get; private set; }
        public string Name { get; private set; }
        public string Alias { get; private set; }
        public string Address { get; private set; }
        public short Size { get; private set; }
        public string BitMask { get; private set; }
        public short S { get; private set; }
        public short I { get; private set; }
        public decimal A { get; private set; }
        public short B { get; private set; }
        public string Unit { get; private set; }

        internal SessionVariable(string line)
        {
            int indexOfColon = line.IndexOf(':');
            this.Number = int.Parse(line.Substring(1, indexOfColon - 1));
            string[] parts = line.Substring(indexOfColon + 1, line.Length - 1 - indexOfColon).Split(',');
            this.Name = parts[0].Trim();
            this.Alias = parts[1].Trim();
            this.Address = parts[2].Trim();
            this.Size = short.Parse(parts[3].Trim());
            this.BitMask = parts[4].Trim();
            this.S = short.Parse(parts[5].Trim());
            this.I = short.Parse(parts[6].Trim());
            this.A = decimal.Parse(parts[7].Trim());
            this.B = short.Parse(parts[8].Trim());
            this.Unit = parts[9].Trim();
            this.Unit = Unit.Substring(1, Unit.Length - 2);
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Alias, Name);
        }
    }

    public class SessionVariables
    {
        public short LoggedDataSize { get; private set; }

        private List<SessionVariable> list = new List<SessionVariable>();
        private Dictionary<int, SessionVariable> byNumber = new Dictionary<int, SessionVariable>();
        private Dictionary<string, SessionVariable> byName = new Dictionary<string, SessionVariable>(StringComparer.InvariantCultureIgnoreCase);

        internal SessionVariables() { }

        internal bool Add(SessionVariable loggedVariable)
        {
            if (!byNumber.ContainsKey(loggedVariable.Number) && !byName.ContainsKey(loggedVariable.Name))
            {
                list.Add(loggedVariable);
                byNumber.Add(loggedVariable.Number, loggedVariable);
                byName.Add(loggedVariable.Name, loggedVariable);
                return true;
            }
            return false;
        }

        public IEnumerable<SessionVariable> Values
        {
            get { return this.list; }
        }

        public SessionVariable GetByNumber(int number)
        {
            if (byNumber.ContainsKey(number))
                return byNumber[number];
            return null;
        }

        public SessionVariable GetByName(string name)
        {
            if (byName.ContainsKey(name))
                return byName[name];
            return null;
        }

        internal bool Complete { get; private set; }
        internal void ReadLine(string line)
        {
            if (line.StartsWith("Really logged are"))
            {
                Complete = true;
            }
            else if (line[0] == '#' && line[1] != 'n' && line[1] != '-')
            {
                this.Add(new SessionVariable(line));
            }
            else if (line.StartsWith("Logged data size"))
            {
                //todo set loggeddatasize
            }
        }
    }
}