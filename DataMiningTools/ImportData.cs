using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMiningTools
{
    public class ImportData
    {
        public class Log
        {
            public string Time { get; set; }
            public string Source { get; set; }
            public string SourcePort { get; set; }
            public string Destination { get; set; }
            public string DestinationPort { get; set; }
            public string Protocol { get; set; }
            public string Length { get; set; }
            public string Info { get; set; }
            public string TCPflags { get; set; }
            public string AT { get; set; }
        }
        public class LogCompare : IEqualityComparer<Log>
        {
            public bool Equals(Log x, Log y) { return x.Time.Equals(y.Time); }
            public int GetHashCode(Log obj) { return obj.Time.GetHashCode(); }
        }

        public event EventHandler<DataMiningTools.MainForm.LogEventArgs> onLogMessageRecv;
        public event EventHandler<DataMiningTools.MainForm.ProgressEventArgs> onProgressRecv;
        public event EventHandler<DataMiningTools.MainForm.ProgressEventArgs> onSubProgressRecv;

        private string[] CSVFiles;
        private SQLUtility sql;
        private List<Log> logs;
        private int ALLProcess;

        public ImportData(string[] CSVFiles) 
        {
            this.CSVFiles = CSVFiles;
            this.logs = new List<Log>();
            sql = new SQLUtility();
            sql.onProgressRecv += new EventHandler<DataMiningTools.MainForm.ProgressEventArgs>(sql_onProgressRecv);
        }

        protected void dataProcess_onLogMessageRecv(object sender, MainForm.LogEventArgs e)
        {
            LogMessage(e.Message);
        }
        protected void sql_onProgressRecv(object sender, DataMiningTools.MainForm.ProgressEventArgs e)
        {
            ShowSubProgress(e.Progress, 100);
        }
        private void LogMessage(string message)
        {
            if (onLogMessageRecv != null)
                onLogMessageRecv(this, new DataMiningTools.MainForm.LogEventArgs { Message = message } );
        }
        private void ShowProgress(int progress, int total)
        {
            if (onProgressRecv != null)
                onProgressRecv(this, new DataMiningTools.MainForm.ProgressEventArgs 
                                         {
                                             Progress = Convert.ToInt32(Convert.ToDouble(progress) / Convert.ToDouble(total) * 100)
                                         });
        }
        private void ShowSubProgress(int progress, int total)
        {
            if (onSubProgressRecv != null)
                onSubProgressRecv(this, new DataMiningTools.MainForm.ProgressEventArgs
                                            {
                                                Progress = Convert.ToInt32(Convert.ToDouble(progress) / Convert.ToDouble(total) * 100)
                                            });
        }

        public void StartImport()
        {
            if (CSVFiles == null)
                throw new Exception("Please select the files!");

            // step 1. import data
            LogMessage("Starting to import data...");
            ALLProcess = CSVFiles.Length + 1; // 1 = number of process, such as bulkcopy.
            foreach (string fileName in CSVFiles)
            {
                LogMessage(string.Format("Process {0}...", fileName));

#region DEBUGING CODE
#if DEBUG
                int timeln = 0;
                int sourceln = 0;
                int sourceportln = 0;
                int desln = 0;
                int desportln = 0;
                int protocolln = 0;
                int lengthln = 0;
                int infoln = 0;
                int tcpflagsln = 0;
#endif
#endregion

                string[] lines = System.IO.File.ReadAllLines(fileName);
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] split_line = lines[i].Split(new string[] { "\"," }, StringSplitOptions.None);

#region DEBUGING CODE
#if DEBUG
                    timeln = split_line[0].Substring(1, split_line[0].Length - 1).Length > timeln ? split_line[0].Substring(1, split_line[0].Length - 1).Length : timeln;
                    sourceln = split_line[1].Substring(1, split_line[1].Length - 1).Length > sourceln ? split_line[1].Substring(1, split_line[1].Length - 1).Length : sourceln;
                    sourceportln = split_line[2].Substring(1, split_line[2].Length - 1).Length > sourceportln ? split_line[2].Substring(1, split_line[2].Length - 1).Length : sourceportln;
                    desln = split_line[3].Substring(1, split_line[3].Length - 1).Length > desln ? split_line[3].Substring(1, split_line[3].Length - 1).Length : desln;
                    desportln = split_line[4].Substring(1, split_line[4].Length - 1).Length > desportln ? split_line[4].Substring(1, split_line[4].Length - 1).Length : desportln;
                    protocolln = split_line[5].Substring(1, split_line[5].Length - 1).Length > protocolln ? split_line[5].Substring(1, split_line[5].Length - 1).Length : protocolln;
                    lengthln = split_line[6].Substring(1, split_line[6].Length - 1).Length > lengthln ? split_line[6].Substring(1, split_line[6].Length - 1).Length : lengthln;
                    infoln = split_line[7].Substring(1, split_line[7].Length - 1).Length > infoln ? split_line[7].Substring(1, split_line[7].Length - 1).Length : infoln;
                    tcpflagsln = split_line[8].Substring(1, split_line[8].Length - 2).Length > tcpflagsln ? split_line[8].Substring(1, split_line[8].Length - 2).Length : tcpflagsln;
                    if (split_line[8].Substring(1, split_line[8].Length - 2).Length > 6)
                        Console.WriteLine(split_line[8].Substring(1, split_line[8].Length - 2));
#endif
#endregion

                    logs.Add(new Log 
                                 {
                                     Time = split_line[0].Substring(1, split_line[0].Length - 1),
                                     Source = split_line[1].Substring(1, split_line[1].Length - 1),
                                     SourcePort = split_line[2].Substring(1, split_line[2].Length - 1),
                                     Destination = split_line[3].Substring(1, split_line[3].Length - 1),
                                     DestinationPort = split_line[4].Substring(1, split_line[4].Length - 1),
                                     Protocol = split_line[5].Substring(1, split_line[5].Length - 1),
                                     Length = split_line[6].Substring(1, split_line[6].Length - 1),
                                     Info = split_line[7].Substring(1, split_line[7].Length - 1),
                                     TCPflags = split_line[8].Substring(1, split_line[8].Length - 2)
                                 });
                    ShowSubProgress(i, lines.Length);
                }
                LogMessage(string.Format("Process {0}...OK!", fileName));
                ShowProgress(Array.IndexOf(CSVFiles, fileName) + 1, ALLProcess);
            }

            // step 2. insert to database
            BulkCopyToSQL();
        }

        protected void BulkCopyToSQL()
        {
            LogMessage("Starting insert to database...");
            sql.InsertLogToDB(logs);
            LogMessage("Starting insert to database...OK!");
            ShowSubProgress(100, 100);
            ShowProgress(CSVFiles.Length + 1, ALLProcess);
        }
    }
}
