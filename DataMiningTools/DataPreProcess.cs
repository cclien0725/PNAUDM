using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataMiningTools
{
    public class DataPreProcess
    {
        public event EventHandler<DataMiningTools.MainForm.LogEventArgs> onLogMessageRecv;
        public event EventHandler<DataMiningTools.MainForm.ProgressEventArgs> onProgressRecv;
        public event EventHandler<DataMiningTools.MainForm.ProgressEventArgs> onSubProgressRecv;
        private void LogMessage(string message)
        {
            if (onLogMessageRecv != null)
                onLogMessageRecv(this, new DataMiningTools.MainForm.LogEventArgs { Message = message });
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

        private SQLUtility sql;

        public bool ClearDNS { get; set; }
        public bool ClearUserIP { get; set; }
        public bool ClearARPs { get; set; }
        public bool TransferIP2Geo { get; set; }
        public bool FillNull { get; set; }

        public DataPreProcess()
        {
            sql = new SQLUtility();
        }
        public void StartDataProcess()
        {
            CleanIngnoreData();
            TransformData();
        }
        private void CleanIngnoreData()
        {
            LogMessage("Starting clean processes...");
            int currentProcess = 0;
            int allProcess = 0;

            // Count the processes
            if (ClearDNS) allProcess++;
            if (ClearUserIP) allProcess++;
            if (ClearARPs) allProcess++;

            // Start processes
            if (ClearDNS)
            {
                Clear140_126_1_1();
                ShowSubProgress(++currentProcess, allProcess);
            }

            if (ClearUserIP)
            {
                ClearOurUsingIP();
                ShowSubProgress(++currentProcess, allProcess);
            }

            if (ClearARPs)
            {
                ClearARP();
                ShowSubProgress(++currentProcess, allProcess);
            }

            LogMessage("Starting clean processes...OK!");
            ShowProgress(1, 2);
        }
        private void TransformData()
        {
            LogMessage("Starting transform data processes...");
            int currentProcess = 0;
            int allProcess = 0;

            // Count the processes
            if (TransferIP2Geo) allProcess++;
            if (FillNull) allProcess++;

            // Start processes
            if (TransferIP2Geo)
            {
                TransformIP2GeoLocation();
                ShowSubProgress(++currentProcess, allProcess);
            }

            if (FillNull)
            {
                FillNullCells();
                ShowSubProgress(++currentProcess, allProcess);
            }

            LogMessage("Starting transform data processes...OK!");
            ShowProgress(2, 2);
        }

        private void Clear140_126_1_1()
        {
            LogMessage("Deleting DNS IP...");
            sql.RunCommand("DELETE FROM Logs WHERE source = '140.126.1.1'");
            sql.RunCommand("DELETE FROM Logs WHERE destination = '140.126.1.1'");
            LogMessage("Deleting DNS IP...OK!");
        }

        private void ClearOurUsingIP()
        {
            LogMessage("Deleting user IP...");
            sql.RunCommand("DELETE FROM Logs WHERE source = '140.126.130.74'");
            sql.RunCommand("DELETE FROM Logs WHERE source = '140.126.130.38'");
            sql.RunCommand("DELETE FROM Logs WHERE source = '140.126.130.39'");
            sql.RunCommand("DELETE FROM Logs WHERE source = '140.126.130.73'");
            LogMessage("Deleting user IP...OK!");
        }

        private void ClearARP()
        {
            LogMessage("Deleting router ARP...");
            sql.RunCommand("DELETE FROM Logs WHERE source = 'Cisco_b3:08:44'");
            sql.RunCommand("DELETE FROM Logs WHERE source = 'Elitegro_92:a8:fe'");
            sql.RunCommand("DELETE FROM Logs WHERE source = 'Giga-Byt_4f:90:25'");
            sql.RunCommand("DELETE FROM Logs WHERE source = 'ZyxelCom_d1:45:72'");
            sql.RunCommand("DELETE FROM Logs WHERE source = 'AsustekC_f6:cb:0a'");
            sql.RunCommand("DELETE FROM Logs WHERE source = 'PlanexCo_c2:c9:7b'");
            LogMessage("Deleting router ARP...OK!");
        }

        private void TransformIP2GeoLocation()
        {
            try
            {
                LogMessage("Transforming source IP to geolocation...");

                DataView dt = sql.GetResult("SELECT Source FROM Logs GROUP BY Source");
                for (int i = 0; i < dt.Table.Rows.Count; i++)
                {
                    string IP = dt[i]["Source"].ToString();
                    if (!IP.Contains(":"))
                    {
                        string[] splitedIP = IP.Split('.');
                        long integer_ip = Convert.ToInt64(splitedIP[0]) * 16777216
                                            + Convert.ToInt64(splitedIP[1]) * 65536
                                            + Convert.ToInt64(splitedIP[2]) * 256
                                            + Convert.ToInt64(splitedIP[3]);
                        DataView geoDt = sql.GetResult(string.Format("SELECT city, country FROM GeoIPBlocks, GeoIPLocation WHERE {0} BETWEEN startIpNum AND endIpNum AND GeoIPBlocks.locId = GeoIPLocation.locId", integer_ip));
                        if (geoDt.Count > 0)
                            sql.RunCommand(string.Format("UPDATE Logs SET Country = '{0}', City = '{1}' WHERE Source = '{2}'", geoDt[0]["country"], geoDt[0]["city"], IP));
                        else
                            sql.RunCommand(string.Format("UPDATE Logs SET Country = '{0}', City = '{1}' WHERE Source = '{2}'", "unknown", "unknown", IP));
                    }
                }

                LogMessage("Transforming source IP to geolocation...OK!");
            }
            catch (Exception ex)
            {
            }
        }

        private void FillNullCells()
        {
            LogMessage("Filling null cells with \"unknown\"...");
            sql.RunCommand("UPDATE Logs SET City = 'unknown' WHERE City = ''");
            sql.RunCommand("UPDATE Logs SET TCPflags = 'unknown' WHERE TCPflags = ''");
            LogMessage("Filling null cells with \"unknown\"...OK!");
        }
    }
}
