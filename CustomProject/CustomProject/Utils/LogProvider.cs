using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CustomProject.Utils
{
    public enum LogType { Error, Debug, Warning, Info }
    public class LogProvider
    {
        private static LogProvider instance;

        public static LogProvider Instance()
        {
            if (instance == null)
            {
                instance = new LogProvider();
                instance.AddLog("Init log provider.");
                instance.InitTimer();
                instance.AddLog("Init log timer.");
            }
            lock (instance)
            {
                return instance;
            }
        }



        private List<string> logs = new List<string>();
        /*
        DateTime.Now.ToString("MM/dd/yyyy")	                            05/29/2015
        DateTime.Now.ToString("dddd, dd MMMM yyyy")	                    Friday, 29 May 2015
        DateTime.Now.ToString("dddd, dd MMMM yyyy")	                    Friday, 29 May 2015 05:50
        DateTime.Now.ToString("dddd, dd MMMM yyyy")	                    Friday, 29 May 2015 05:50 AM
        DateTime.Now.ToString("dddd, dd MMMM yyyy")                     Friday, 29 May 2015 5:50
        DateTime.Now.ToString("dddd, dd MMMM yyyy")	                    Friday, 29 May 2015 5:50 AM
        DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss")            Friday, 29 May 2015 05:50:06
        DateTime.Now.ToString("MM/dd/yyyy HH:mm")	                    05/29/2015 05:50
        DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")	                05/29/2015 05:50 AM
        DateTime.Now.ToString("MM/dd/yyyy H:mm")                        05/29/2015 5:50
        DateTime.Now.ToString("MM/dd/yyyy h:mm tt")	                    05/29/2015 5:50 AM
        DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")                    05/29/2015 05:50:06
        DateTime.Now.ToString("MMMM dd")	                            May 29
        DateTime.Now.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss.fffffffK")	2015-05-16T05:50:06.7199222-04:00
        DateTime.Now.ToString("ddd, dd MMM yyy HH’:’mm’:’ss ‘GMT’")	    Fri, 16 May 2015 05:50:06 GMT
        DateTime.Now.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss")          2015-05-16T05:50:06
        DateTime.Now.ToString("HH:mm")	                                05:50
        DateTime.Now.ToString("hh:mm tt")	                            05:50 AM
        DateTime.Now.ToString("H:mm")                                   5:50
        DateTime.Now.ToString("h:mm tt")	                            5:50 AM
        DateTime.Now.ToString("HH:mm:ss")                               05:50:06
        DateTime.Now.ToString("yyyy MMMM")                              2015 May
        */
        public string LogFormat = "MM/dd/yyyy HH:mm:ss";

        public delegate void LogHandler(string message, LogType type = LogType.Info);
        public event LogHandler AddLogHandler;

        public void AddLog(string message, LogType type = LogType.Info)
        {
            string log = $"[{DateTime.Now.ToString(LogFormat)}][{type}]:{message}";
            AddLogHandler?.Invoke(log, type);
            logs.Add(log);
        }

        public void AddError(string message)
        {
            string log = $"[{DateTime.Now.ToString(LogFormat)}][{LogType.Error}]:{message}";
            AddLogHandler?.Invoke(log, LogType.Error);
            logs.Add(log);
        }

        public void SaveLogs(string path = "logs.txt")
        {
            AddLog($"Save logs to file: {path}");
            using (StreamWriter writetext = new StreamWriter(path))
            {
                foreach (string message in logs)
                {
                    writetext.WriteLine(message);
                }
            }
        }

        public async void InitTimer()
        {
            var dueTime = TimeSpan.FromSeconds(5);
            var interval = TimeSpan.FromSeconds(120);
            await ThreadManager.RunPeriodicAsync(OnTick, dueTime, interval, CancellationToken.None);
        }

        private void OnTick()
        {
            SaveLogs();
            logs = new List<string>();
        }


    }
}
