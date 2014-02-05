using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using VisualME7Logger.Log;
using VisualME7Logger.Common;
using System.Xml.Linq;

namespace VisualME7Logger.Session
{
    public class ME7LoggerSession
    {
        public enum Statuses
        {
            New,
            Opening,
            Initialized,
            Open,
            Closing,
            Closed,
            Paused
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
                    StatusChanged(_status);
                }
            }
        }
        public short SamplesPerSecond { get; private set; }
        public DateTime LogStarted { get; private set; }
        public LoggerSessionStatusChanged StatusChanged;
        public SessionTypes SessionType { get; private set; }
        public CommunicationInfo CommunicationInfo { get; private set; }
        public IdentificationInfo IdentificationInfo { get; private set; }
        public SessionVariables Variables { get; private set; }
        public ME7LoggerLog Log { get; private set; }

        public bool CanPause { get { return this.SessionType == SessionTypes.File; } }
        
        private LoggerOptions options;
        private string configFilePath;
        private string ME7LoggerDirectory;
        public ME7LoggerSession(string ME7LoggerDirectory, LoggerOptions options, string configFilePath)
        {
            this.Status = Statuses.New;
            this.SessionType = SessionTypes.RealTime;
            this.Log = new ME7LoggerLog(this);
            this.ME7LoggerDirectory = ME7LoggerDirectory;
            this.options = options;
            this.configFilePath = configFilePath;
        }

        public ME7LoggerSession(string logFilePath)
        {
            this.Status = Statuses.New;
            this.SessionType = SessionTypes.File;
            this.Log = new ME7LoggerLog(this, logFilePath);
        }

        Process p;        
        StreamWriter outputWriter = null;
        public bool logReady = false;
        public int ExitCode { get; private set; }
        StringBuilder errorTextBuilder;
        public string ErrorText { get { return errorTextBuilder != null ? errorTextBuilder.ToString() : string.Empty; } }
        public void Open()
        {
            this.Status = Statuses.Opening;
            this.errorTextBuilder = new StringBuilder();

            if (this.SessionType == SessionTypes.File)
            {
                this.IdentificationInfo = new IdentificationInfo();
                this.CommunicationInfo = new CommunicationInfo();
                this.Variables = new SessionVariables();
                this.Log.InitializeSession(IdentificationInfo, CommunicationInfo, Variables);
                this.SamplesPerSecond = CommunicationInfo.SamplesPerSecond;
                this.Status = Statuses.Initialized; 
                this.LogStarted = CommunicationInfo.LogStarted;
                this.Status = Statuses.Open;
                this.Log.Open();
            }
            else
            {
                logReady = false;

                if (options.WriteOutputFile)
                {
                    outputWriter = new StreamWriter(Path.Combine(ME7LoggerDirectory, "VisualME7LoggerOutput.txt"), false);
                    outputWriter.AutoFlush = true;
                }

                p = new Process();
                p.StartInfo = new ProcessStartInfo(
                    Path.Combine(ME7LoggerDirectory, "bin", "ME7Logger.exe"),
                    string.Format("{0} \"{1}\"", options, configFilePath));
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

        public void Pause()
        {
            if (this.Status == Statuses.Open)
            {
                if (this.SessionType == SessionTypes.File)
                {
                    this.Log.Pause();
                    this.Status = Statuses.Paused;
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
        }

        public void Resume()
        {
            if (this.Status == Statuses.Paused)
            {
                this.Log.Resume();
                this.Status = Statuses.Open;
            }
        }

        void p_Exited(object sender, EventArgs e)
        {
            this.ExitCode = p.ExitCode;
            this.Close();
        }

        void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            lock (this)
            {
                if (options.WriteOutputFile && outputWriter != null)
                {
                    outputWriter.WriteLine("**error**{0}", e.Data);
                }
                errorTextBuilder.AppendFormat("{0}{1}", string.IsNullOrEmpty(ErrorText) ? string.Empty : Environment.NewLine, e.Data);
            }
        }

        void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            lock (this)
            {
                if (options.WriteOutputFile && outputWriter != null)
                {
                    outputWriter.WriteLine(e.Data);
                }

                try
                {
                    if (this.Status == Statuses.Opening)
                    {
                        if (this.ReadLine(e.Data))
                        {
                            this.Status = Statuses.Initialized;
                            this.Status = Statuses.Open;
                            this.Log.Open();
                            this.LogStarted = DateTime.Now;
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
                catch (Exception ex)
                {
                    errorTextBuilder.AppendFormat("Error when reading line {0}\r\n{1}", e.Data, ex);
                }
            }
        }

        public void Close()
        {
            lock (this)
            {
                if (this.Status != Statuses.Closed)
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

                    if (this.outputWriter != null)
                    {
                        outputWriter.Close();
                        outputWriter.Dispose();
                        outputWriter = null;
                    }
                    
                    Status = Statuses.Closed;
                }
            }
        }

        private string logConfigFile;
        private string ecuCharacteristicsFile;
        private string ecuDef;
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
            else if (CommunicationInfo == null && line == "[Communication]")
            {
                //[Communication]
                CommunicationInfo = new CommunicationInfo();
            }
            else if (CommunicationInfo != null && !CommunicationInfo.Complete)
            {
                CommunicationInfo.ReadLine(line);
            }
            else if (IdentificationInfo == null && line == "[Identification]")
            {
                //[Identification]
                IdentificationInfo = new IdentificationInfo();
            }
            else if (IdentificationInfo != null && !IdentificationInfo.Complete)
            {
                IdentificationInfo.ReadLine(line);
            }
            else if (Variables == null && line == "Logged variables are:")
            {
                //logged variables are:
                Variables = new SessionVariables();
            }
            else if (Variables != null && !Variables.Complete)
            {
                Variables.ReadLine(line);
            }
            else if (line.StartsWith("-> Start logging"))
            {
                SamplesPerSecond = short.Parse(line.Substring(line.IndexOf(",") + 2,
                    line.IndexOf("samples/second") - line.IndexOf(",") - 3));
                return true;
            }
            return false;
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
        public bool Signed { get; private set; }
        public bool Inverse { get; private set; }
        public decimal Factor { get; private set; }
        public decimal Offset { get; private set; }
        public string Unit { get; private set; }

        internal SessionVariable(int number, string name, string unit, string alias) 
        {
            this.Number = number;
            this.Name = name;
            this.Unit = unit;
            this.Alias = alias;
        }

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
            this.Signed = parts[5].Trim() == "1";
            this.Inverse = parts[6].Trim() == "1";
            try { Factor = decimal.Parse(parts[7].Trim()); }
            catch { }
            this.Offset = decimal.Parse(parts[8].Trim());
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
        public SessionVariable this[int number] 
        { 
            get 
            {
                if(this.byNumber.ContainsKey(number))
                    return this.byNumber[number];
                return null;
            } 
        }
        private Dictionary<string, SessionVariable> byName = new Dictionary<string, SessionVariable>(StringComparer.InvariantCultureIgnoreCase);
        public SessionVariable this[string name] 
        { 
            get
            {
                if(this.byName.ContainsKey(name))
                    return this.byName[name];
                return null;
            }
        }

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

        public int Count
        {
            get { return this.list.Count; }
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

        private List<string> variableLines;
        internal bool Complete { get; private set; }
        internal void ReadLine(string line, bool fromLog = false)
        {
            if (fromLog)
            {
                if (line.StartsWith("TimeStamp,"))
                {
                    variableLines = new List<string>();
                    variableLines.Add(line);
                }
                else if (variableLines.Count < 3)
                {
                    variableLines.Add(line);
                    if (variableLines.Count == 3)
                    {
                        string[] names = variableLines[0].Split(',');
                        string[] units = variableLines[1].Split(',');
                        string[] aliases = variableLines[2].Split(',');

                        for (int i = 1; i < names.Length; ++i)
                        {
                            this.Add(new SessionVariable(i, names[i].Trim(), units[i].Trim(), aliases[i].Replace("\"", "").Trim()));
                        }
                        Complete = true;
                    }
                }
            }
            else
            {
                if (line.StartsWith("Really logged are"))
                {
                    Complete = true;
                }
                else if (line.Length > 1 && line[0] == '#' && line[1] != 'n' && line[1] != '-')
                {
                    this.Add(new SessionVariable(line));
                }
                else if (line.StartsWith("Logged data size is"))
                {
                    LoggedDataSize = short.Parse(line.Replace("Logged data size is ", "").Replace(" bytes.", "").Trim());
                }
            }
        }
    }

    public class LoggerOptions
    {
        public enum ConnectionTypes
        {
            Default,
            COM,
            FTDI,
            FTDISerial,
            FTDIDescription,
            FTDILocation,
            LogFile
        }

        public ConnectionTypes ConnectionType { get; set; }
        public string COMPort { get; set; }
        public string FTDIInfo { get; set; }
        public int BaudRate { get; set; }
        public bool OverrideBaudRate { get; set; }

        public bool OverrideSampleRate { get; set; }
        public int SampleRate { get; set; }

        public bool TimeSync { get; set; }
        public bool WriteAbsoluteTimestamp { get; set; }
        public bool WriteMilliSecondTimestamps { get; set; }
        public bool SuppressHeaderInfoInLog { get; set; }
        public bool ReadSingleMeasurement { get; set; }
        public bool WriteLogRealTime { get; set; }
        public bool RealTimeOutput { get; set; }

        public bool WriteLogToFile { get; set; }
        public string LogFile { get; set; }
        public bool WriteOutputFile { get; set; }

        public LoggerOptions(string ME7LoggerDirectory)
        {
            ConnectionType = ConnectionTypes.Default;
            COMPort = "COM1";
            FTDIInfo = string.Empty;
            WriteLogToFile = true;
            LogFile = System.IO.Path.Combine(ME7LoggerDirectory, "logs", "VisualME7Logger.log");
            RealTimeOutput = true;
            BaudRate = 56000;
            SampleRate = 20;
        }

        public void Read(XElement element)
        {
            if (element.Name.LocalName == "Options")
            {
                foreach (XAttribute att in element.Attributes())
                {
                    switch (att.Name.LocalName)
                    {
                        case "WriteLogRealTime":
                            this.WriteLogRealTime = bool.Parse(att.Value);
                            break;
                        case "TimeSync":
                            this.TimeSync = bool.Parse(att.Value);
                            break;
                        case "WriteAbsoluteTimestamp":
                            this.WriteAbsoluteTimestamp = bool.Parse(att.Value);
                            break;
                        case "ReadSingleMeasurement":
                            this.ReadSingleMeasurement = bool.Parse(att.Value);
                            break;
                        case "WriteOutputFile":
                            this.WriteOutputFile = bool.Parse(att.Value);
                            break;
                    }
                }

                foreach (XElement ele in element.Elements())
                {
                    switch (ele.Name.LocalName)
                    {
                        case "Communication":

                            foreach (XAttribute att in ele.Attributes())
                            {
                                switch (att.Name.LocalName)
                                {
                                    case "Type":
                                        this.ConnectionType = (ConnectionTypes)Enum.Parse(typeof(ConnectionTypes), att.Value);
                                        break;
                                    case "COMPort":
                                        this.COMPort = att.Value;
                                        break;
                                    case "FTDIInfo":
                                        this.FTDIInfo = att.Value;
                                        break;
                                }
                            }
                            XElement baud = ele.Elements().FirstOrDefault();
                            if (baud != null)
                            {
                                this.BaudRate = int.Parse(baud.Value);
                                this.OverrideBaudRate = bool.Parse(baud.Attributes().First().Value);
                            }
                            break;
                        case "SampleRate":
                            this.OverrideSampleRate = bool.Parse(ele.Attributes().First().Value);
                            this.SampleRate = int.Parse(ele.Value);
                            break;
                        case "LogFile":
                            this.WriteLogToFile = bool.Parse(ele.Attributes().First().Value);
                            this.LogFile = ele.Value;
                            break;
                    }
                }
            }
        }

        public XElement Write()
        {
            XElement root = new XElement("Options");

            XElement communication = new XElement("Communication");
            communication.Add(new XAttribute("Type", ConnectionType));
            communication.Add(new XAttribute("COMPort", COMPort));
            communication.Add(new XAttribute("FTDIInfo", FTDIInfo));

            XElement baud = new XElement("BaudRate", this.BaudRate);
            baud.Add(new XAttribute("Override", this.OverrideBaudRate));
            communication.Add(baud);

            root.Add(communication);

            XElement sampleRate = new XElement("SampleRate", SampleRate);
            sampleRate.Add(new XAttribute("Override", this.OverrideSampleRate));
            root.Add(sampleRate);

            XElement logFile = new XElement("LogFile", this.LogFile);
            logFile.Add(new XAttribute("WriteToFile", this.WriteLogToFile));
            root.Add(logFile);

            root.Add(new XAttribute("WriteLogRealTime", WriteLogRealTime));
            root.Add(new XAttribute("TimeSync", TimeSync));
            root.Add(new XAttribute("WriteAbsoluteTimestamp", WriteAbsoluteTimestamp));
            root.Add(new XAttribute("ReadSingleMeasurement", ReadSingleMeasurement));
            root.Add(new XAttribute("WriteOutputFile", WriteOutputFile));
            
            return root;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (this.ConnectionType == ConnectionTypes.COM)
            {
                sb.Append(" -p ").Append(this.COMPort);
            }
            else if (this.ConnectionType != ConnectionTypes.Default)
            {
                sb.Append(" -f");
                if (this.ConnectionType == ConnectionTypes.FTDISerial)
                {
                    sb.AppendFormat(" -S{0}", FTDIInfo);
                }
                else if (this.ConnectionType == ConnectionTypes.FTDIDescription)
                {
                    sb.AppendFormat(" -D{0}", FTDIInfo);
                }
                else if (this.ConnectionType == ConnectionTypes.FTDILocation)
                {
                    sb.AppendFormat(" -L{0}", FTDIInfo);
                }
            }
            if (OverrideSampleRate)
            {
                sb.Append(" -s ").Append(this.SampleRate);
            }
            if (OverrideBaudRate)
            {
                sb.Append(" -b ").Append(this.BaudRate);
            }
            if (TimeSync)
            {
                sb.Append(" -t");
            }
            if (WriteAbsoluteTimestamp)
            {
                sb.Append(" -a");
            }
            if (WriteMilliSecondTimestamps)
            {
                sb.Append(" -m");
            }
            if (SuppressHeaderInfoInLog)
            {
                sb.Append(" - h");
            }
            if (ReadSingleMeasurement)
            {
                sb.Append(" -1");
            }
            if (WriteLogRealTime)
            {
                sb.Append(" -r");
            }
            if (RealTimeOutput)
            {
                sb.Append(" -R");
            }
            if (WriteLogToFile)
            {
                sb.AppendFormat(" -o {0}", LogFile);
            }
            return sb.ToString();
        }
    }
}