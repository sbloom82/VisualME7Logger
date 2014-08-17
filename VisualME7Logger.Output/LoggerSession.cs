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
            LogFile,
            SessionOutput
        }

        public delegate void LoggerSessionStatusChanged(Statuses status);
        public delegate void LogLineReadDel(LogLine logLine);
        public delegate void SessionDataRead(string line, bool error = false);

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
        public bool IsOpen { get { return this.Status != Statuses.Closed && this.Status != Statuses.Closing && this.Status != Statuses.New; } }
        public short _samplesPerSecond;
        public short SamplesPerSecond
        {
            get
            {
                return _samplesPerSecond;
            }
            private set
            {
                _samplesPerSecond = value;
                CurrentSamplesPerSecond = _samplesPerSecond;
            }
        }
        public short CurrentSamplesPerSecond { get; private set; }
        public DateTime LogStarted { get; private set; }
        public LoggerSessionStatusChanged StatusChanged;
        public LogLineReadDel LogLineRead;
        public SessionDataRead DataRead;
        public SessionTypes SessionType { get; private set; }
        public CommunicationInfo CommunicationInfo { get; private set; }
        public IdentificationInfo IdentificationInfo { get; private set; }
        public SessionVariables Variables { get; private set; }
        public ME7LoggerLog Log { get; private set; }
        public List<ExpressionVariable> ExpressionVariables { get; set; }

        public bool CanSetPlaybackSpeed { get { return this.SessionType != SessionTypes.RealTime; } }

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

        private string filePath;
        public ME7LoggerSession(string ME7LoggerDirectory, string filePath, SessionTypes sessionType = SessionTypes.LogFile)
        {
            this.Status = Statuses.New;
            this.SessionType = sessionType;
            this.ME7LoggerDirectory = ME7LoggerDirectory;
            this.filePath = filePath;
            if (this.SessionType == SessionTypes.LogFile)
            {
                this.Log = new ME7LoggerLog(this, this.filePath);
            }
            else if (this.SessionType == SessionTypes.SessionOutput)
            {
                this.Log = new ME7LoggerLog(this);
                this.options = new LoggerOptions("");
            }
            else
            {
                throw new ArgumentOutOfRangeException("sessionType");
            }
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

            if (this.SessionType == SessionTypes.LogFile)
            {
                this.IdentificationInfo = new IdentificationInfo();
                this.CommunicationInfo = new CommunicationInfo();
                this.Variables = new SessionVariables();
                this.Log.InitializeSession(IdentificationInfo, CommunicationInfo, Variables);
                this.AddExpressions();
                this.SamplesPerSecond = CommunicationInfo.SamplesPerSecond;
                this.Status = Statuses.Initialized;
                this.LogStarted = CommunicationInfo.LogStarted;
                this.Status = Statuses.Open;
                this.Log.Open();
            }
            else if (this.SessionType == SessionTypes.RealTime)
            {
                logReady = false;

                if (options.WriteLogFileWithVME7L)
                {
                    this.WriteOneSample();
                    this.logWriter = new LogWriter(options.LogFile);
                }

                if (options.WriteOutputFile)
                {
                    outputWriter = new StreamWriter(Path.Combine(ME7LoggerDirectory, "VisualME7LoggerOutput.txt"), false);
                    outputWriter.AutoFlush = true;
                }

                System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Highest;
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
            else
            {
                new System.Threading.Thread(new System.Threading.ThreadStart(OpenFromSessionOutput)).Start();
            }
        }

        private void WriteOneSample()
        {
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(
                Path.Combine(ME7LoggerDirectory, "bin", "ME7Logger.exe"),
                string.Format("{0} \"{1}\"", options.ToString(true), configFilePath));
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;

            if (p.Start())
            {
                p.WaitForExit();
                System.Threading.Thread.Sleep(500);
            }
        }

        private void AddExpressions()
        {
            if (ExpressionVariables != null)
            {
                foreach (ExpressionVariable ev in this.ExpressionVariables)
                {
                    this.Variables.Add(ev);
                }
            }
        }

        private void OpenFromSessionOutput()
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    this.ReadData(line);
                    System.Threading.Thread.Sleep(this.Status == Statuses.Open ? 50 : 1);
                }
            }
            this.Close();
        }

        public void ResetSpeed()
        {
            if (this.SessionType != SessionTypes.RealTime)
            {
                this.CurrentSamplesPerSecond = this.SamplesPerSecond;
                if (this.StatusChanged != null)
                {
                    StatusChanged(_status);
                }
            }
        }

        public void SetSpeed(int add)
        {
            if (this.SessionType != SessionTypes.RealTime)
            {
                if (this.CurrentSamplesPerSecond + add > 0)
                {
                    this.CurrentSamplesPerSecond += (short)add;
                }
                else
                {
                    this.CurrentSamplesPerSecond = 1;
                }

                if (this.StatusChanged != null)
                {
                    StatusChanged(_status);
                }
            }
        }

        public void Pause()
        {
            if (this.Status == Statuses.Open)
            {
                if (this.SessionType != SessionTypes.RealTime)
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

                if (this.DataRead != null)
                {
                    this.DataRead(e.Data, true);
                }
                errorTextBuilder.AppendFormat("{0}{1}", string.IsNullOrEmpty(ErrorText) ? string.Empty : Environment.NewLine, e.Data);
            }
        }

        void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            this.ReadData(e.Data);
        }

        private void ReadData(string data)
        {
            lock (this)
            {
                if (options.WriteOutputFile && outputWriter != null)
                {
                    outputWriter.WriteLine(data);
                }

                if (Debug)
                {
                    WriteDebug("line read: " + data);
                }

                try
                {
                    if (this.Status == Statuses.Opening)
                    {
                        if (Debug)
                        {
                            WriteDebug("Opening...before readline");
                        }
                        if (this.ReadLine(data))
                        {
                            if (Debug)
                            {
                                WriteDebug("Opening...readline true, var count:" + (this.Variables == null ? 0 : this.Variables.Count));
                            }

                            this.AddExpressions();

                            this.Status = Statuses.Initialized;
                            this.Status = Statuses.Open;
                            this.Log.Open();
                            this.LogStarted = DateTime.Now;
                        }
                    }
                    else if (this.Status == Statuses.Open)
                    {
                        if (Debug)
                        {
                            WriteDebug("Open... logready:" + logReady.ToString());
                        }

                        if (!logReady && data == "Press ^C to stop logging")
                        {
                            logReady = true;
                        }
                        else if (logReady)
                        {
                            if (Debug)
                            {
                                WriteDebug("Open... reading line");
                            }

                            if (options.WriteLogFileWithVME7L)
                            {
                                this.logWriter.Add(data);
                            }

                            LogLine logLine = this.Log.ReadLine(data);
                            this.LineRead(logLine);
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorTextBuilder.AppendFormat("Error when reading line {0}\r\n{1}", data, ex);
                }
            }
        }

        internal void LineRead(LogLine logLine)
        {
            foreach (Variable v in logLine.Variables.Where(v => v.SessionVariable.Type == SessionVariable.Types.Expression))
            {
                v.Value = ((ExpressionVariable)v.SessionVariable).Compute(Variables, logLine);
            }

            if (LogLineRead != null)
            {
                if (Debug)
                {
                    WriteDebug("Open.... Line Read!");
                }
                LogLineRead(logLine);
            }
        }

        private static object DebugLock = new object();
        public static bool Debug;
        public void WriteDebug(string line)
        {
            if (Debug)
            {
                lock (DebugLock)
                {
                    using (StreamWriter sw = new StreamWriter(System.IO.Path.Combine(ME7LoggerDirectory, "DEBUG1.TXT"), true))
                    {
                        sw.WriteLine("{0:HH:mm:ss.ffff}:  {1}", DateTime.Now, line);
                    }
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

                    if (this.logWriter != null)
                    {
                        this.logWriter.Close();
                        this.logWriter = null;
                    }

                    Status = Statuses.Closed;
                }
            }
        }

        private string logConfigFile;
        private string ecuCharacteristicsFile;
        private string ecuDef;
        private LogWriter logWriter;
        private bool ReadLine(string line)
        {
            if (line != null)
            {
                if (this.DataRead != null)
                {
                    this.DataRead(line);
                }

                if (string.IsNullOrEmpty(logConfigFile))
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
            }
            return false;
        }

        class LogWriter
        {
            Queue<string> queue;
            StreamWriter writer;
            public LogWriter(string path)
            {
                queue = new Queue<string>();
                writer = new StreamWriter(path, true);
                new System.Threading.Thread(this.Run).Start();
            }

            public bool stop = false;
            void Run()
            {
                while (!stop)
                {
                    int waits = 0;
                    while (waits++ < 100 && !stop)
                    {
                        System.Threading.Thread.Sleep(100);
                    }

                    if (queue.Count > 0)
                    {
                        while (queue.Count > 0)
                        {
                            writer.WriteLine(queue.Dequeue());
                        }
                        writer.Flush();
                    }
                }

                if (this.writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                    writer = null;
                }
            }

            public void Add(string data)
            {
                this.queue.Enqueue(data);
            }

            public void Close()
            {
                this.stop = true;
            }
        }
    }

    public class SessionVariable
    {
        public enum Types
        {
            Log,
            Expression
        }

        public Types Type { get; private set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string Alias { get; set; }

        internal SessionVariable(Types type, string name, string unit, string alias)
        {
            this.Type = type;
            this.Name = name;
            this.Unit = unit;
            this.Alias = alias;
        }
    }

    public class LogVariable : SessionVariable
    {
        public int Number { get; private set; }
        public string Address { get; private set; }
        public short Size { get; private set; }
        public string BitMask { get; private set; }
        public bool Signed { get; private set; }
        public bool Inverse { get; private set; }
        public decimal Factor { get; private set; }
        public decimal Offset { get; private set; }

        internal LogVariable(int number, string name, string unit, string alias)
            : base(Types.Log, name, unit, alias)
        {
            this.Number = number;
            this.Alias = alias;
        }

        internal LogVariable(string line)
            : base(Types.Log, null, null, null)
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

    public class ExpressionVariable : SessionVariable
    {
        private string _expression;
        public string Expression
        {
            get { return _expression; }
            set
            {
                _expression = value;
                _exp = null;
                Error = false;
            }
        }
        public bool Error { get; private set; }

        public ExpressionVariable() : base(Types.Expression, null, null, null) { }

        public ExpressionVariable(string name, string unit, string expression)
            : base(Types.Expression, name, unit, name)
        {
            Expression = expression;
        }

        NCalc.Expression _exp = null;
        LogLine curLL = null;
        public string Compute(SessionVariables variables, LogLine logLine)
        {
            if (_exp == null)
            {
                _exp = new NCalc.Expression(this.Expression);
                _exp.EvaluateParameter += delegate(string name, NCalc.ParameterArgs args)
                {
                    Variable var = curLL.GetVariableByName(name);
                    if (var != null)
                    {
                        args.Result = var.Value;
                    }
                    else
                    {
                        args.HasResult = false;
                    }
                };
            }
            object obj = "ERR";
            if (!Error)
            {
                curLL = logLine;
                _exp.Parameters.Clear();
                try
                {
                    obj = _exp.Evaluate().ToString();
                }
                catch
                {
                    Error = true;
                }
            }
            return obj.ToString();
        }

        public override string ToString()
        {
            return Name;
        }

        public void Read(XElement ele)
        {
            foreach (XAttribute att in ele.Attributes())
            {
                switch (att.Name.LocalName)
                {
                    case "Name":
                        this.Name = att.Value;
                        break;
                    case "Unit":
                        this.Unit = att.Value;
                        break;
                    case "Expression":
                        this.Expression = att.Value;
                        break;
                }
            }
        }

        public XElement Write()
        {
            XElement retval = new XElement("Expression");
            retval.Add(new XAttribute("Name", this.Name));
            retval.Add(new XAttribute("Unit", this.Unit));
            retval.Add(new XAttribute("Expression", this.Expression));
            return retval;
        }

        public ExpressionVariable Clone()
        {
            return new ExpressionVariable(this.Name, this.Unit, this.Expression);
        }
    }

    public class SessionVariables
    {
        public short LoggedDataSize { get; private set; }

        private List<SessionVariable> list = new List<SessionVariable>();
        private Dictionary<string, SessionVariable> byName = new Dictionary<string, SessionVariable>(StringComparer.InvariantCultureIgnoreCase);
        public SessionVariable this[string name]
        {
            get
            {
                if (this.byName.ContainsKey(name))
                    return this.byName[name];
                return null;
            }
        }

        internal SessionVariables() { }

        internal void Add(SessionVariable loggedVariable)
        {
            list.Add(loggedVariable);
            byName[loggedVariable.Name] = loggedVariable;
        }

        public IEnumerable<SessionVariable> Values
        {
            get { return this.list; }
        }

        public int Count
        {
            get { return this.list.Count; }
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
                            this.Add(new LogVariable(i, names[i].Trim(), units[i].Trim(), aliases[i].Replace("\"", "").Trim()));
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
                    this.Add(new LogVariable(line));
                }
                else if (line.StartsWith("Logged data size is"))
                {
                    try { LoggedDataSize = short.Parse(line.Replace("Logged data size is ", "").Replace(" bytes.", "").Trim()); }
                    catch { }
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

        public bool WriteLogFileWithVME7L { get; set; }

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
                        case "DisableRealTimeOutput":
                            this.RealTimeOutput = !bool.Parse(att.Value);
                            break;
                        case "WriteLogFileWithVME7L":
                            this.WriteLogFileWithVME7L = bool.Parse(att.Value);
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
            root.Add(new XAttribute("DisableRealTimeOutput", !this.RealTimeOutput));
            root.Add(new XAttribute("WriteLogFileWithVME7L", this.WriteLogFileWithVME7L));

            return root;
        }

        public LoggerOptions Clone()
        {
            LoggerOptions clone = new LoggerOptions(string.Empty);
            clone.ConnectionType = this.ConnectionType;
            clone.COMPort = this.COMPort;
            clone.FTDIInfo = this.FTDIInfo;
            clone.BaudRate = this.BaudRate;
            clone.OverrideBaudRate = this.OverrideBaudRate;
            clone.OverrideSampleRate = this.OverrideSampleRate;
            clone.SampleRate = this.SampleRate;
            clone.TimeSync = this.TimeSync;
            clone.WriteAbsoluteTimestamp = this.WriteAbsoluteTimestamp;
            clone.WriteMilliSecondTimestamps = this.WriteMilliSecondTimestamps;
            clone.SuppressHeaderInfoInLog = this.SuppressHeaderInfoInLog;
            clone.ReadSingleMeasurement = this.ReadSingleMeasurement;
            clone.WriteLogRealTime = this.WriteLogRealTime;
            clone.RealTimeOutput = this.RealTimeOutput;
            clone.WriteLogToFile = this.WriteLogToFile;
            clone.LogFile = LogFile;
            clone.WriteOutputFile = this.WriteOutputFile;
            clone.WriteLogFileWithVME7L = this.WriteLogFileWithVME7L;
            return clone;
        }

        public override string ToString()
        {
            return this.ToString(false);
        }

        public string ToString(bool writeOneSample)
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
            if (ReadSingleMeasurement || writeOneSample)
            {
                sb.Append(" -1");
            }
            if (WriteLogRealTime)
            {
                sb.Append(" -r");
            }
            if ((RealTimeOutput || WriteLogFileWithVME7L) && !writeOneSample)
            {
                sb.Append(" -R");
            }
            if ((WriteLogToFile && !WriteLogFileWithVME7L) || writeOneSample)
            {
                sb.AppendFormat(" -o \"{0}\"", LogFile);
            }
            return sb.ToString();
        }
    }
}