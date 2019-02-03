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
        public short CurrentSamplesPerSecond { get; internal set; }
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
        public bool WriteLogToFileEnabled
        {
            get
            {
                if (this.options != null)
                {
                    return this.options.WriteLogToFile;
                }
                return false;
            }
            set
            {
                if (this.options != null)
                {
                    this.options.WriteLogToFile = value;
                }
            }
        }

        public bool CanSetPlaybackSpeed { get { return this.SessionType != SessionTypes.RealTime; } }

        private LoggerOptions options;
        private string configFilePath;
        private string ME7LoggerDirectory;

        private string filePath;
        public string FilePath
        {
            get { return filePath; }
            private set { filePath = value; }
        }
        public string FileName
        {
            get
            {
                return string.IsNullOrEmpty(this.FilePath) ? string.Empty : Path.GetFileName(this.FilePath);
            }
        }

        public ME7LoggerSession(string ME7LoggerDirectory, string logFilePath, LoggerOptions options, string configFilePath)
        {
            this.Status = Statuses.New;
            this.SessionType = SessionTypes.RealTime;
            this.FilePath = logFilePath;
            this.Log = new ME7LoggerLog(this, this.FilePath);
            this.ME7LoggerDirectory = ME7LoggerDirectory;
            this.options = options;
            this.configFilePath = configFilePath;
        }

        public ME7LoggerSession(string ME7LoggerDirectory, string filePath, SessionTypes sessionType = SessionTypes.LogFile, bool noWait=false)
        {
            this.Status = Statuses.New;
            this.SessionType = sessionType;
            this.ME7LoggerDirectory = ME7LoggerDirectory;
            this.FilePath = filePath;
            if (this.SessionType == SessionTypes.LogFile)
            {
                this.Log = new ME7LoggerLog(this, this.FilePath, noWait);
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
            bool tailFile = options != null && options.TailLogFileWithVME7L;

            if (this.SessionType == SessionTypes.LogFile || tailFile)
            {
                if (!tailFile)
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
                }
                this.Log.Open(tailFile);
            }

            if (this.SessionType == SessionTypes.RealTime)
            {
                logReady = false;

                if (options.WriteOutputFile)
                {
                    outputWriter = new StreamWriter(Path.Combine(ME7LoggerDirectory, "VisualME7LoggerOutput.txt"), false);
                    outputWriter.AutoFlush = true;
                }

                System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Highest;
                p = new Process();
                p.StartInfo = new ProcessStartInfo(
                    Path.Combine(ME7LoggerDirectory, "bin", "ME7Logger.exe"),
                    string.Format("{0} \"{1}\"", options.ToString(configFilePath), configFilePath));
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

            if (this.SessionType == SessionTypes.SessionOutput)
            {
                new System.Threading.Thread(new System.Threading.ThreadStart(OpenFromSessionOutput)).Start();
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
            using (StreamReader sr = new StreamReader(this.FilePath))
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

                try
                {
                    if (this.Status == Statuses.Opening)
                    {
                        if (this.ReadLine(data))
                        {
                            this.AddExpressions();

                            this.Status = Statuses.Initialized;
                            this.Status = Statuses.Open;

                            this.LogStarted = DateTime.Now;
                        }
                    }
                    else if (this.Status == Statuses.Open)
                    {
                        if (!logReady && data == "Press ^C to stop logging")
                        {
                            logReady = true;
                        }
                        else if (logReady)
                        {
                            LogLine logLine = this.Log.ReadLine(data, lastLogLine);
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

        LogLine lastLogLine = null;
        internal void LineRead(LogLine logLine)
        {
            foreach (Variable v in logLine.Variables)
            {
                if (v.SessionVariable.Type == SessionVariable.Types.Expression)
                {
                    v.Value = Convert.ToDecimal(((ExpressionVariable)v.SessionVariable).Compute(logLine), ME7LoggerLog.CultureInfo);
                }

                if (lastLogLine != null)
                {
                    Variable lastLogLineVar = lastLogLine.GetVariableByName(v.SessionVariable.Name);
                    if (lastLogLineVar != null)
                    {
                        v.CurrentMinValue = v.Value < lastLogLineVar.CurrentMinValue ? v.Value : lastLogLineVar.CurrentMinValue;
                        v.CurrentMaxValue = v.Value > lastLogLineVar.CurrentMaxValue ? v.Value : lastLogLineVar.CurrentMaxValue;
                    }
                }
            }

            if (LogLineRead != null)
            {
                LogLineRead(logLine);
            }

            lastLogLine = logLine;
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

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(this.Alias))
            {
                return string.Format("{0} ({1})", this.Alias, this.Name);
            }
            return this.Name;
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

        public string Group { get; private set; }

        internal LogVariable(int number, string name, string unit, string alias)
            : base(Types.Log, name, unit, alias)
        {
            this.Number = number;
            this.Alias = alias;
        }

        internal LogVariable(int number, string name, string unit, string alias, string group)
            : this(number, name, unit, alias)
        {
            this.Group = group;
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
            try { Factor = decimal.Parse(parts[7].Trim(), VisualME7Logger.Log.ME7LoggerLog.CultureInfo); }
            catch { }
            this.Offset = decimal.Parse(parts[8].Trim(), VisualME7Logger.Log.ME7LoggerLog.CultureInfo);
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
        int errorCount = 0;
        char splitChar = '|';
        public object Compute(LogLine logLine)
        {
            if (_exp == null)
            {
                _exp = new NCalc.Expression(this.Expression);
                _exp.EvaluateParameter += delegate (string name, NCalc.ParameterArgs args)
                {                   
                    if (!string.IsNullOrEmpty(name))
                    {
                        string[] names = name.Split(splitChar);
                        foreach (string n in names)
                        {
                            Variable var = curLL.GetVariableByName(n);
                            if (var != null)
                            {
                                args.Result = var.Value;
                                return;
                            }
                        }                        
                    }
                    args.HasResult = false;
                };
            }
            object obj = 0m;
            if (!Error)
            {
                curLL = logLine;
                _exp.Parameters.Clear();
                try
                {
                    obj = _exp.Evaluate();
                    errorCount = 0;
                }
                catch
                {
                    if (++errorCount > 5)
                    {
                        Error = true;
                    }
                }
            }
            return obj;
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
            if (loggedVariable is LogVariable)
            {
                LogVariablesCount++;
            }
            list.Add(loggedVariable);
            byName[loggedVariable.Name] = loggedVariable;
        }

        public IEnumerable<SessionVariable> Values
        {
            get { return this.list; }
        }

        public int LogVariablesCount;
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
        internal void ReadLine(string line, ME7LoggerLog.LogTypes logType = ME7LoggerLog.LogTypes.Unknown)
        {
            switch (logType)
            {
                case ME7LoggerLog.LogTypes.VCDS:
                    if (line.StartsWith(",Group A:") || line.StartsWith("Marker,"))
                    {
                        variableLines = new List<string>();
                        variableLines.Add(line);
                    }
                    else if (variableLines != null)
                    {
                        variableLines.Add(line);
                        if (variableLines.Count == 4)
                        {
                            string[] groups = variableLines[0].Split(',');
                            string[] names1 = variableLines[1].Split(',');
                            string[] names2 = variableLines[2].Split(',');
                            string[] units = variableLines[3].Split(',');
                            string currentGroup = "";

                            for (int i = 1; i < names1.Length; ++i)
                            {
                                if (string.IsNullOrEmpty(names1[i]))
                                {
                                    if (groups.Length > i)
                                    {
                                        currentGroup = groups[i].Substring(0, groups[i].Length - 1);
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(names1[i]))
                                    {
                                        string name = names1[i] + " " + names2[i];
                                        this.Add(new LogVariable(this.Count + 1, name, units[i], name, currentGroup));
                                    }
                                }
                            }

                            this.Complete = true;
                            break;
                        }
                        else if (variableLines.Count == 3 && variableLines[0].StartsWith("Marker,"))
                        {
                            string[] names1 = variableLines[0].Split(',');
                            string[] names2 = variableLines[1].Split(',');
                            string[] units = variableLines[2].Split(',');

                            for (int i = 1; i < names1.Length; ++i)
                            {
                                if (!string.IsNullOrEmpty(names1[i]) && names1[i] != "TIME")
                                {
                                    this.Add(new LogVariable(this.Count + 1, names1[i], units[i], names2[i]));
                                }
                            }

                            this.Complete = true;
                            break;
                        }
                    }
                    break;
                case ME7LoggerLog.LogTypes.ME7Logger:
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
                    break;
                case ME7LoggerLog.LogTypes.Standard:
                case ME7LoggerLog.LogTypes.Normal:
                    if (line.StartsWith("Time") || line.StartsWith("Time (sec),"))
                    {
                        bool parse = false;
                        if (line.StartsWith("Time (sec),"))
                        {
                            parse = true;
                        }

                        string[] names = line.Split(',');
                        for (int i = 1; i < names.Length; ++i)
                        {
                            if (!string.IsNullOrEmpty(names[i]))
                            {

                                if (parse)
                                {
                                    string value = names[i];
                                    int indexOpen = value.IndexOf("(");
                                    int indexClose = value.IndexOf(")");

                                    //example  Engine Speed(RPM) nmot_w, Vehicle Speed(MPH) vfzg_w,
                                    string alias = value.Substring(0, indexOpen);
                                    string unit = value.Substring(indexOpen + 1, indexClose - indexOpen - 1);
                                    string name = value.Substring(indexClose + 2, value.Length - indexClose - 2);
                                    this.Add(new LogVariable(i, name, unit, alias));
                                }
                                else
                                {
                                    this.Add(new LogVariable(i, names[i], string.Empty, names[i]));
                                }
                            }
                        }
                        this.Complete = true;
                    }
                    break;
                case ME7LoggerLog.LogTypes.Unknown:
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
                    break;
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
        public bool UseDefaultLogFile { get; set; }
        public bool WriteOutputFile { get; set; }

        public bool TailLogFileWithVME7L { get; set; }

        public LoggerOptions(string ME7LoggerDirectory)
        {
            ConnectionType = ConnectionTypes.Default;
            COMPort = "COM1";
            FTDIInfo = string.Empty;
            WriteLogToFile = true;
            LogFile = System.IO.Path.Combine(ME7LoggerDirectory, "logs", "VisualME7Logger.csv");
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
                        case "TailLogFileWithVME7L":
                            this.TailLogFileWithVME7L = bool.Parse(att.Value);
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
                            foreach (XAttribute att in ele.Attributes())
                            {
                                switch (att.Name.LocalName)
                                {
                                    case "WriteToFile":
                                        this.WriteLogToFile = bool.Parse(att.Value);
                                        break;
                                    case "UseDefaultLogFile":
                                        this.UseDefaultLogFile = bool.Parse(att.Value);
                                        break;
                                }
                            }
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
            logFile.Add(new XAttribute("UseDefaultLogFile", this.UseDefaultLogFile));
            root.Add(logFile);

            root.Add(new XAttribute("WriteLogRealTime", WriteLogRealTime));
            root.Add(new XAttribute("TimeSync", TimeSync));
            root.Add(new XAttribute("WriteAbsoluteTimestamp", WriteAbsoluteTimestamp));
            root.Add(new XAttribute("ReadSingleMeasurement", ReadSingleMeasurement));
            root.Add(new XAttribute("WriteOutputFile", WriteOutputFile));
            root.Add(new XAttribute("DisableRealTimeOutput", !this.RealTimeOutput));
            root.Add(new XAttribute("TailLogFileWithVME7L", this.TailLogFileWithVME7L));

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
            clone.UseDefaultLogFile = this.UseDefaultLogFile;
            clone.LogFile = LogFile;
            clone.WriteOutputFile = this.WriteOutputFile;
            clone.TailLogFileWithVME7L = this.TailLogFileWithVME7L;
            return clone;
        }

        public override string ToString()
        {
            return this.ToString(string.Empty);
        }

        public string ToString(string configFile)
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
            if (RealTimeOutput && !TailLogFileWithVME7L)
            {
                sb.Append(" -R");
            }
            if (WriteLogToFile || TailLogFileWithVME7L)
            {
                sb.AppendFormat(" -o");

                if (UseDefaultLogFile || string.IsNullOrEmpty(LogFile))
                {
                    sb.AppendFormat(" \"{0}_{1:yyyyMMdd_HHmmss}.csv\"",
                        System.IO.Path.GetFileNameWithoutExtension(configFile), DateTime.Now);
                }
                else
                {
                    sb.AppendFormat(" \"{0}\"", LogFile);
                }
            }
            return sb.ToString();
        }
    }
}