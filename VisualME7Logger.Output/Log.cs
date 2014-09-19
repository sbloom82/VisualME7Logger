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
        internal static System.Globalization.CultureInfo CultureInfo = new System.Globalization.CultureInfo("en-US");
       
        public ME7LoggerSession Session { get; private set; }

        internal ME7LoggerLog(ME7LoggerSession session)
        {
            this.Session = session;
        }

        //For testing using text files
        public ME7LoggerLog(ME7LoggerSession session, string logFilePath)
            : this(session)
        {
            this.logFilePath = logFilePath;
        }

        private int lineNumber;
        private string logFilePath;
        internal void InitializeSession(IdentificationInfo identificationInfo, CommunicationInfo communicationInfo, SessionVariables variables)
        {
            bool idinfostarted = false,
                commInfoStarted = false,
                variablesStarted = false;
            using (StreamReader sr = new StreamReader(logFilePath, Encoding.UTF7))
            {
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    if (!identificationInfo.Complete)
                    {
                        if (!idinfostarted)
                        {
                            idinfostarted = line.StartsWith("ECU identified with following data:");
                        }
                        else
                        {
                            identificationInfo.ReadLine(line, true);
                        }
                    }
                    else if (!communicationInfo.Complete)
                    {
                        if (!commInfoStarted)
                        {
                            commInfoStarted = line == "";
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
                            variablesStarted = line == "";
                        }
                        else
                        {
                            variables.ReadLine(line, true);
                        }

                        if (variables.Complete)
                        {
                            break;
                        }
                    }
                }
            }
        }
        internal void Open()
        {
            lineNumber = 0;
            if (this.Session.SessionType == ME7LoggerSession.SessionTypes.LogFile)
            {
                stop = false;

                if (!File.Exists(logFilePath))
                {
                    throw new FileNotFoundException("Log File not found at {0}", logFilePath);
                }
                new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(OpenFromLogFile)).Start(logFilePath);
            }
        }

        private bool stop = false;
        public void Close() { stop = true; }

        private bool paused = false;
        public void Pause()
        {
            paused = true;
        }
        public void Resume()
        {
            paused = false;
        }

        private void OpenFromLogFile(object parameter)
        {
            string logFilePath = (string)parameter;
            using (StreamReader sr = new StreamReader(logFilePath, Encoding.UTF7))
            {
                bool ready = false;
                string line;
                DateTime time = DateTime.Now;
                while ((line = sr.ReadLine()) != null && !stop)
                {
                    if (!ready)
                    {
                        if (this.Session.DataRead != null)
                        {
                            this.Session.DataRead(line);
                        }

                        if (line.StartsWith("\"TIME"))
                        {
                            ready = true;
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
                            LogLine logLine = this.ReadLine(line);
                            Session.LineRead(logLine);

                            int waitTime =
                                (int)((1 / (double)Session.CurrentSamplesPerSecond) * 1000) -
                                (int)DateTime.Now.Subtract(time).TotalMilliseconds;

                            if (waitTime > 0)
                                System.Threading.Thread.Sleep(waitTime);
                        }
                        catch { }
                    }

                    while (paused && !stop)
                    {
                        System.Threading.Thread.Sleep(25);
                    }
                    time = DateTime.Now;
                }
            }
            Session.Close();
        }

        internal LogLine ReadLine(string line)
        {
            return new LogLine(this, line, ++lineNumber);
        }
    }

    public class LogLine
    {
        public ME7LoggerLog Log { get; private set; }
        public decimal TimeStamp { get; private set; }
        public int LineNumber { get; private set; }
        private Dictionary<string, Variable> variablesByName = new Dictionary<string, Variable>(StringComparer.InvariantCultureIgnoreCase);
        public Variable this[string name] { get { return variablesByName[name]; } }
        public IEnumerable<Variable> Variables { get { return this.variables; } }
        private List<Variable> variables = new List<Variable>();

        public LogLine(ME7LoggerLog log, string line, int lineNumber)
        {
            this.Log = log;
            this.LineNumber = lineNumber;
            this.Parse(line);
        }

        private const char COLUMN_SEP = ',';
        private void Parse(string line)
        {
            string[] values = line.Split(LogLine.COLUMN_SEP);
            TimeStamp = decimal.Parse(values[0], VisualME7Logger.Log.ME7LoggerLog.CultureInfo);
            int i = 1;
            foreach (SessionVariable sv in Log.Session.Variables.Values)
            {
                string value = string.Empty;
                if (values.Length > i)
                {
                    value = values[i].Trim();
                }
                Variable v = new Variable(this, sv, value);
                variables.Add(v);
                variablesByName.Add(v.SessionVariable.Name, v);
                i++;
            }
        }

        public Variable GetVariableByName(string name)
        {
            if (variablesByName.ContainsKey(name))
                return variablesByName[name];
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
