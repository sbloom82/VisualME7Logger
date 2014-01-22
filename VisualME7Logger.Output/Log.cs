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
        public ME7LoggerSession Session { get; private set; }
        public delegate void LogLineRead(LogLine logLine);
        public delegate void LogComplete();
        public LogLineRead LineRead;

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
            if (this.Session.SessionType == ME7LoggerSession.SessionTypes.File)
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
            int wait = (int)((1 / (double)Session.SamplesPerSecond) * 1000);
            using (StreamReader sr = new StreamReader(logFilePath, Encoding.UTF7))
            {
                bool ready = false;
                string line;
                DateTime time = DateTime.Now;
                while ((line = sr.ReadLine()) != null && !stop)
                {
                    if (!ready && line.StartsWith("\"TIME"))
                    {
                        ready = true;
                    }
                    else if (ready)
                    {
                        if (string.IsNullOrWhiteSpace(line) || line[0] == '#')
                        {
                            //handles multiple logs in the same file
                            ready = false;
                            continue;
                        }
                        this.ReadLine(line);
                        int waitTime = wait - (int)DateTime.Now.Subtract(time).TotalMilliseconds;
                        if (waitTime > 0)
                            System.Threading.Thread.Sleep(waitTime);
                    }                    
                    
                    while (paused)
                    {
                        System.Threading.Thread.Sleep(25);
                    }
                    time = DateTime.Now;
                }
            }
        }

        internal void ReadLine(string line)
        {
            LogLine logLine = new LogLine(this, line, ++lineNumber);
            if (LineRead != null)
            {
                LineRead(logLine);
            }
        }
    }

    public class LogLine
    {
        public ME7LoggerLog Log { get; private set; }
        public decimal TimeStamp { get; private set; }
        public int LineNumber { get; private set; }
        private Dictionary<int, Variable> variablesByNumber = new Dictionary<int, Variable>();
        public Variable this[int number] { get { return variablesByNumber[number]; } }
        private Dictionary<string, Variable> variablesByName = new Dictionary<string, Variable>(StringComparer.InvariantCultureIgnoreCase);
        public Variable this[string name] { get { return variablesByName[name]; } }
        public IEnumerable<Variable> Variables { get { return this.variablesByNumber.Values; } }

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
            TimeStamp = decimal.Parse(values[0]);
            for (int i = 1; i < Log.Session.Variables.Count + 1; ++i)
            {
                string value = string.Empty;
                if (values.Length > i)
                {
                    value = values[i].Trim();
                }
                Variable v = new Variable(Log.Session.Variables.GetByNumber(i), value);
                variablesByNumber.Add(i, v);
                variablesByName.Add(v.SessionVariable.Name, v);
            }
        }

        public Variable GetVariableByNumber(int number)
        {
            return variablesByNumber[number];
        }

        public Variable GetVariableByName(string name)
        {
            return variablesByName[name];
        }
    }

    public class Variable
    {
        public SessionVariable SessionVariable { get; private set; }
        public string Value { get; private set; }

        public Variable(SessionVariable sessionVariable, string value)
        {
            this.SessionVariable = sessionVariable;
            this.Value = value;
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
