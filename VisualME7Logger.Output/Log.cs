using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using VisualME7Logger.Session;
using VisualME7Logger.Common;

namespace VisualME7Logger.Log
{
    public class ME7LoggerLog
    {
        public enum LogTypes
        {
            Unknown,
            ME7Logger,
            VCDS,
            Eurodyne,
            Normal
        }

        internal static System.Globalization.CultureInfo CultureInfo = new System.Globalization.CultureInfo("en-US");

        public ME7LoggerSession Session { get; private set; }

        internal ME7LoggerLog(ME7LoggerSession session)
        {
            this.Session = session;
        }

        //For testing using text files
        public ME7LoggerLog(ME7LoggerSession session, string logFilePath, bool noWait = false)
            : this(session)
        {
            this.LogFilePath = logFilePath;
            this.noWait = noWait;
        }

        internal bool NewVCDSFormat;
        private bool noWait;
        private int lineNumber;
        public LogTypes LogType;

        public string LogFilePath { get; private set; }
        public long TotalFileSize { get; private set; }
        public long CurrentPosition { get; private set; }
        internal void InitializeSession(IdentificationInfo identificationInfo, CommunicationInfo communicationInfo, SessionVariables variables)
        {
            bool idinfostarted = false,
                commInfoStarted = false,
                variablesStarted = false;
            using (StreamReader sr = new StreamReader(LogFilePath, Encoding.UTF7))
            {
                string line = null;

                while ((line = sr.ReadLine()) != null)
                {
                    if (this.LogType == LogTypes.Unknown)
                    {
                        if (line.Contains("VCDS") || line.Contains("VAG-COM"))
                        {
                            this.LogType = LogTypes.VCDS;
                        }
                        else if (line.StartsWith("Time,"))
                        {
                            this.LogType = LogTypes.Eurodyne;
                        }
                        else if (line.StartsWith("Time (sec),"))
                        {
                            this.LogType = LogTypes.Normal;
                        }
                    }

                    if (this.LogType == LogTypes.VCDS)
                    {
                        if (!variables.Complete)
                        {
                            if (!variablesStarted)
                            {
                                variablesStarted = line.StartsWith(",Group A:") || line.StartsWith("Marker,");
                                NewVCDSFormat = line.StartsWith("Marker,");
                            }

                            if (variablesStarted)
                            {
                                variables.ReadLine(line, this.LogType);
                            }

                            if (variables.Complete)
                            {
                                break;
                            }
                        }
                    }
                    else if (this.LogType == LogTypes.Eurodyne)
                    {
                        if (!variables.Complete)
                        {
                            if (!variablesStarted)
                            {
                                variablesStarted = line.StartsWith("Time,");
                            }

                            if (variablesStarted)
                            {
                                variables.ReadLine(line, this.LogType);
                            }

                            if (variables.Complete)
                            {
                                break;
                            }
                        }
                    }
                    else if (this.LogType == LogTypes.Normal)
                    {
                        if (!variablesStarted)
                        {
                            variablesStarted = line.StartsWith("Time (sec),");
                        }

                        if (variablesStarted)
                        {
                            variables.ReadLine(line, this.LogType);
                        }

                        if (variables.Complete)
                        {
                            break;
                        }
                    }

                    if (!identificationInfo.Complete)
                    {
                        if (!idinfostarted)
                        {
                            idinfostarted = line.StartsWith("ECU identified with following data:");
                        }
                        else
                        {
                            this.LogType = LogTypes.ME7Logger;
                            identificationInfo.ReadLine(line, true);
                        }
                    }
                    else if (!communicationInfo.Complete)
                    {
                        if (!commInfoStarted)
                        {
                            commInfoStarted = line == "" || line.All(c => c == ',');
                        }
                        else
                        {
                            communicationInfo.ReadLine(line, true);
                        }
                    }
                    else if (!variables.Complete)
                    {
                        if (!variablesStarted)
                        {
                            variablesStarted = line == "" || line.All(c => c == ',');
                        }
                        else
                        {
                            variables.ReadLine(line, this.LogType);
                        }

                        if (variables.Complete)
                        {
                            break;
                        }
                    }
                }
            }
        }
        internal void Open(bool tailFile)
        {
            lineNumber = 0;
            if (this.Session.SessionType == ME7LoggerSession.SessionTypes.LogFile || tailFile)
            {
                stop = false;

                if (!File.Exists(LogFilePath) && !tailFile)
                {
                    throw new FileNotFoundException("Log File not found at {0}", LogFilePath);
                }
                else if (tailFile)
                {
                    if (File.Exists(LogFilePath))
                    {
                        try
                        {
                            string copyToName = LogFilePath;
                            while (File.Exists(copyToName))
                            {
                                copyToName += ".bak";
                            }
                            File.Move(LogFilePath, copyToName);
                        }
                        catch { }
                    }
                }
                new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(OpenFromLogFile)).Start(new object[] { LogFilePath, tailFile });
            }
        }

        private bool stop = false;
        public void Close() { stop = true; }

        public void Pause()
        {
            controlQueue.Enqueue("pause");
        }
        public void Resume()
        {
            controlQueue.Enqueue("resume");
        }
        public void ForwardLarge()
        {
            controlQueue.Enqueue("forwardLarge");
        }

        public void Forward()
        {
            controlQueue.Enqueue("forward");
        }

        public void ReverseLarge()
        {
            controlQueue.Enqueue("reverseLarge");
        }

        public void Reverse()
        {
            controlQueue.Enqueue("reverse");
        }

        public void SetPostion(long position)
        {
            controlQueue.Enqueue("setPosition:" + position);
        }

        public Queue<string> controlQueue = new Queue<string>();
        private void OpenFromLogFile(object parameter)
        {
            string logFilePath = (string)((object[])parameter)[0];
            bool tailFile = (bool)((object[])parameter)[1];

            if (tailFile)
            {
                while (true)
                {
                    if (File.Exists(logFilePath))
                    {
                        break;
                    }
                    if (stop)
                    {
                        return;
                    }
                    System.Threading.Thread.Sleep(250);
                }
            }

            using (StreamReader sr = new StreamReader(
                new FileStream(logFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite),
                Encoding.UTF7))
            {
                bool ready = false;
                bool newVCDSReady = false;
                string line;
                DateTime time = DateTime.Now;

                this.CurrentPosition = 0;
                this.TotalFileSize = sr.BaseStream.Length;

                if (tailFile && this.TotalFileSize > 0)
                {
                    this.CurrentPosition = this.TotalFileSize;
                    sr.BaseStream.Position = this.TotalFileSize;
                    ready = true;
                }

                LogLine last = null;
                while (!stop)
                {
                    while ((line = sr.ReadLine()) != null && !stop)
                    {
                        if (tailFile && ready && Session.Variables != null &&
                            line.Split(',').Length != Session.Variables.LogVariablesCount + 1)
                        {
                            break;
                        }
                        this.CurrentPosition = sr.BaseStream.Position;

                        if (!ready)
                        {
                            if (this.Session.DataRead != null && !tailFile)
                            {
                                this.Session.DataRead(line);
                            }

                            if (newVCDSReady)
                            {
                                ready = line.StartsWith(",,");
                            }
                            else if (line.StartsWith("\"TIME") ||
                                line.StartsWith("TIME") ||
                                line.StartsWith("Marker,") ||
                                line.StartsWith("Time,") ||
                                line.StartsWith("Time (sec),"))
                            {
                                ready = true;
                                if (NewVCDSFormat)
                                {
                                    newVCDSReady = true;
                                    ready = false;
                                }
                            }
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(line) || line[0] == '#')
                            {
                                //handles multiple logs in the same file
                                ready = false;
                                continue;
                            }
                            try
                            {
                                if (this.LogType == LogTypes.VCDS && !NewVCDSFormat)
                                {
                                    foreach (LogLine logLine in this.ReadVCDSLine(line, last))
                                    {
                                        Session.LineRead(logLine);
                                        if(Session.CurrentSamplesPerSecond == 0)
                                            Session.CurrentSamplesPerSecond = (short)(1 / ((logLine.TimeStamp - (last == null ? 0 : last.TimeStamp))));
                                        last = logLine;
                                    }
                                }
                                else
                                {
                                    LogLine logLine = this.ReadLine(line, last);
                                    Session.LineRead(logLine);
                                    if (this.LogType == LogTypes.Eurodyne ||
                                        this.LogType == LogTypes.Normal ||
                                        this.LogType == LogTypes.VCDS)
                                    {
                                        if(Session.CurrentSamplesPerSecond == 0)
                                            Session.CurrentSamplesPerSecond = (short)(1 / ((logLine.TimeStamp - (last == null ? 0 : last.TimeStamp))));
                                    }
                                    last = logLine;
                                }

                                if (!tailFile)
                                {
                                    if (!noWait)
                                    {
                                        int waitTime =
                                          (int)((1 / (double)Session.CurrentSamplesPerSecond) * 1000) -
                                          (int)DateTime.Now.Subtract(time).TotalMilliseconds;

                                        if (waitTime > 0 && waitTime < 2500)
                                            System.Threading.Thread.Sleep(waitTime);
                                    }
                                }
                            }
                            catch { }
                        }

                        #region Control
                        if (!tailFile)
                        {
                            while (this.controlQueue.Count > 0)
                            {
                                string control = controlQueue.Dequeue();
                                try
                                {
                                    switch (control)
                                    {
                                        case "pause":
                                            while (!stop && controlQueue.Count == 0)
                                            {
                                                System.Threading.Thread.Sleep(25);
                                            }
                                            break;
                                        case "reverse":
                                            sr.BaseStream.Seek(-SmallStep, SeekOrigin.Current);
                                            break;
                                        case "reverseLarge":
                                            sr.BaseStream.Seek(-LargeStep, SeekOrigin.Current);
                                            break;
                                        case "forward":
                                            sr.BaseStream.Seek(SmallStep, SeekOrigin.Current);
                                            break;
                                        case "forwardLarge":
                                            sr.BaseStream.Seek(LargeStep, SeekOrigin.Current);
                                            break;
                                        default:
                                            if (control.StartsWith("setPosition:"))
                                            {
                                                sr.BaseStream.Seek(long.Parse(control.Split(':')[1]), SeekOrigin.Begin);
                                            }
                                            break;
                                    }
                                }
                                catch { }
                            }
                        }
                        #endregion

                        time = DateTime.Now;
                    }

                    if (tailFile)
                    {
                        if (sr.BaseStream.Length == this.CurrentPosition)
                        {
                            System.Threading.Thread.Sleep(25);
                        }
                    }
                    else if (line == null)
                    {
                        stop = true;
                    }
                }
            }
            Session.Close();
        }
        private long LargeStep { get { return this.TotalFileSize / 50; } }
        private long SmallStep { get { return this.TotalFileSize / 250; } }


        internal LogLine ReadLine(string line, LogLine last)
        {
            return new LogLine(this, line, ++lineNumber, last);
        }

        private IEnumerable<LogLine> ReadVCDSLine(string line, LogLine last)
        {
            string currentGroup = null;
            List<LogLine> lines = new List<LogLine>();
            string[] split = line.Split(',');
            for (int i = 0; i < split.Length; ++i)
            {
                if ((i - 1) % 5 == 0)
                {
                    if (currentGroup == null)
                    {
                        currentGroup = "Group A";
                    }
                    else if (currentGroup == "Group A")
                    {
                        currentGroup = "Group B";
                    }
                    else if (currentGroup == "Group B")
                    {
                        currentGroup = "Group C";
                    }

                    List<string> values = new List<string>();
                    for (int j = i; j < i + 5; ++j)
                    {
                        values.Add(split[j]);
                    }
                    if (!string.IsNullOrEmpty(values[0]))
                    {
                        LogLine groupLogLine = new LogLine(this, currentGroup, lineNumber++, last, values.ToArray());
                        lines.Add(groupLogLine);
                        last = groupLogLine;
                    }
                }
            }
            return lines;
        }
    }

    public class LogLine
    {
        public ME7LoggerLog Log { get; private set; }
        public decimal TimeStamp { get; private set; }
        private DateTime? TimeStampDateTime { get; set; }
        public int LineNumber { get; private set; }
        private Dictionary<string, Variable> variablesByName = new Dictionary<string, Variable>(StringComparer.InvariantCultureIgnoreCase);
        public Variable this[string name] { get { return variablesByName[name]; } }
        public IEnumerable<Variable> Variables { get { return this.variables; } }
        private List<Variable> variables = new List<Variable>();
        private short timestampIndex = 0;

        public LogLine(ME7LoggerLog log, string line, int lineNumber, LogLine last)
        {
            this.Log = log;
            if (this.Log.NewVCDSFormat)
                this.timestampIndex = 1;

            this.LineNumber = lineNumber;
            this.Parse(line, last);
        }

        public LogLine(ME7LoggerLog log, string group, int lineNumber, LogLine last, params string[] values)
        {
            this.Log = log;
            this.LineNumber = lineNumber;
            this.TimeStamp = decimal.Parse(values[0], VisualME7Logger.Log.ME7LoggerLog.CultureInfo);

            int i = 1;
            foreach (SessionVariable sv in Log.Session.Variables.Values)
            {
                string value = "";
                LogVariable lv = sv as LogVariable;

                if (lv != null && lv.Group == group)
                {
                    //set value from log
                    value = values[i++].Trim();
                }
                else if (last != null)
                {
                    value = last.variablesByName[sv.Name].Value.ToString();
                    //set value from last log line
                }
                Variable v = new Variable(this, sv, value);
                variables.Add(v);
                if (!variablesByName.ContainsKey(v.SessionVariable.Name))
                {
                    variablesByName.Add(v.SessionVariable.Name, v);
                }
                else if (lv.Group == group)
                {
                    variablesByName[v.SessionVariable.Name] = v;
                }
            }
        }


        private bool timeOnly = false;
        private const char COLUMN_SEP = ',';
        private void Parse(string line, LogLine last)
        {
            string[] values = line.Split(LogLine.COLUMN_SEP);

            if (last == null)
            {
                try
                {
                    TimeStamp = decimal.Parse(values[timestampIndex], VisualME7Logger.Log.ME7LoggerLog.CultureInfo);
                }
                catch
                {
                    try
                    {
                        TimeStampDateTime = DateTime.Parse(values[timestampIndex]);
                        TimeStamp = 0;
                    }
                    catch
                    {
                        TimeStampDateTime = DateTime.ParseExact(values[timestampIndex], "HH:mm:ss:ffff", System.Globalization.CultureInfo.CurrentCulture);
                        TimeStamp = 0;
                        timeOnly = true;
                    }
                }
            }
            else
            {
                if (!last.TimeStampDateTime.HasValue)
                {
                    TimeStamp = decimal.Parse(values[timestampIndex], VisualME7Logger.Log.ME7LoggerLog.CultureInfo);
                }
                else
                {
                    if (!last.timeOnly)
                    {
                        TimeStampDateTime = DateTime.Parse(values[timestampIndex]);
                    }
                    else
                    {
                        TimeStampDateTime = DateTime.ParseExact(values[timestampIndex], "HH:mm:ss:ffff", System.Globalization.CultureInfo.CurrentCulture);
                        timeOnly = true;
                    }
                    TimeStamp = last.TimeStamp + (decimal)TimeStampDateTime.Value.Subtract(last.TimeStampDateTime.Value).TotalSeconds;
                }
            }

            int i = timestampIndex + 1;

            foreach (SessionVariable sv in Log.Session.Variables.Values)
            {
                string value = string.Empty;
                if (values.Length > i)
                {
                    value = values[i].Trim();
                }
                Variable v = new Variable(this, sv, value);
                variables.Add(v);
                if (!variablesByName.ContainsKey(v.SessionVariable.Name))
                {
                    variablesByName.Add(v.SessionVariable.Name, v);
                }

                i += Log.NewVCDSFormat ? 2 : 1;
            }
        }

        public Variable GetVariableByName(params string[] names)
        {
            foreach (string name in names)
            {
                if (variablesByName.ContainsKey(name))
                    return variablesByName[name];
            }
            return null;
        }
    }

    public class Variable
    {
        public LogLine LogLine { get; private set; }
        public SessionVariable SessionVariable { get; private set; }
        public decimal Value { get; internal set; }
        public decimal CurrentMinValue { get; internal set; }
        public decimal CurrentMaxValue { get; internal set; }

        public Variable(LogLine logLine, SessionVariable sessionVariable, string value)
        {
            this.LogLine = logLine;
            this.SessionVariable = sessionVariable;
            this.CurrentMinValue = decimal.MaxValue;
            this.CurrentMaxValue = decimal.MinValue;

            decimal tryParse;
            if (decimal.TryParse(value, System.Globalization.NumberStyles.Any, ME7LoggerLog.CultureInfo, out tryParse))
                this.Value = tryParse;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1} {2}",
                SessionVariable.Name,
                this.Value,
                SessionVariable.Unit);
        }
    }
}
