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
        public event EventHandler<DataMiningTools.MainForm.LabelEventArgs> onLabelTextRecv;
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
        private void ShowLabelText(string text, string target)
        {
            if (onLabelTextRecv != null)
                onLabelTextRecv(this, new MainForm.LabelEventArgs { Text = text, Target = target });
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
            InformationEntropy();
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
            ShowProgress(1, 3);
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
            ShowProgress(2, 3);
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

        private void InformationEntropy()
        {
            LogMessage("Starting calculate information entropy...");

            int currentProcess = 0;
            int allProcess = 6;

            double all_data = int.Parse(sql.GetResult("SELECT Count(*) FROM Logs")[0][0].ToString());
            double source, country, city, proto, desport, tcpflag;
            List<EntropySortElement> sortList = new List<EntropySortElement>();

            // Attack Source.
            source = calcSubjectEntropy(all_data, "Source");
            LogMessage("Source: " + source);
            ShowSubProgress(++currentProcess, allProcess);

            country = calcConsiderEntropyWithSubject(all_data, "Country", "Source");
            LogMessage("Country: " + country);
            ShowSubProgress(++currentProcess, allProcess);

            city = calcConsiderEntropyWithSubject(all_data, "City", "Source");
            LogMessage("City: " + city);
            ShowSubProgress(++currentProcess, allProcess);

            sortList.Add(new EntropySortElement { Name = "Country", Value = (source - country) });
            sortList.Add(new EntropySortElement { Name = "City", Value = (source - city) });
            ShowLabelText(showSortingResult(sortList), "lb_src");

            // Attack Type.
            proto = calcSubjectEntropy(all_data, "Protocol");
            LogMessage("Protocol: " + proto);
            ShowSubProgress(++currentProcess, allProcess);

            desport = calcConsiderEntropyWithSubject(all_data, "DestinationPort", "Protocol");
            LogMessage("Destination Port: " + desport);
            ShowSubProgress(++currentProcess, allProcess);

            tcpflag = calcConsiderEntropyWithSubject(all_data, "TCPflags", "Protocol");
            LogMessage("TCP Flags: " + tcpflag);
            ShowSubProgress(++currentProcess, allProcess);

            sortList.Clear();
            sortList.Add(new EntropySortElement { Name = "Destination Port", Value = (proto - desport) });
            sortList.Add(new EntropySortElement { Name = "TCP Flags", Value = (proto - tcpflag) });
            ShowLabelText(showSortingResult(sortList), "lb_type");

            LogMessage("Starting calculate information entropy...OK!");
            ShowProgress(3, 3);
        }

        private double calcSubjectEntropy(double all_data, string subject_dimension)
        {
            int all_subject_data;
            double subject_sum = 0;

            DataView dt_subject = sql.GetResult(string.Format("SELECT {0} FROM Logs GROUP BY {0}", subject_dimension));
            all_subject_data = dt_subject.Table.Rows.Count;

            for (int i = 0; i < all_subject_data; i++)
            {
                string subject = dt_subject[i][subject_dimension].ToString();
                double count;

                DataView dt1 = sql.GetResult(string.Format("SELECT Count(*) FROM Logs WHERE ({0} = '{1}')", subject_dimension, subject));
                count = int.Parse(dt1[0][0].ToString());

                subject_sum -= (count / all_data) * Math.Log((count / all_data), 2);
            }

            return subject_sum;
        }

        private double calcConsiderEntropyWithSubject(double all_data, string consider_dimension, string subject_dimension)
        {
            DataView dt_consider = sql.GetResult(string.Format("SELECT {0} FROM Logs GROUP BY {0}", consider_dimension));
            double consider_sum = 0;

            for (int i = 0; i < dt_consider.Table.Rows.Count; i++)
            {
                string consider = dt_consider[i][consider_dimension].ToString();

                double all_subject_for_consider_count = int.Parse(sql.GetResult(string.Format("SELECT Count(*) FROM Logs WHERE ({0} = '{1}')", consider_dimension, consider))[0][0].ToString());
                DataView dt = sql.GetResult(string.Format("SELECT {0} FROM Logs WHERE ({1} = '{2}') GROUP BY {0}", subject_dimension, consider_dimension, consider));

                for (int j = 0; j < dt.Table.Rows.Count; j++)
                {
                    string subject = dt[j][subject_dimension].ToString();
                    double count = int.Parse(sql.GetResult(string.Format("SELECT Count(*) FROM Logs WHERE ({0} = '{1}') and ({2} = '{3}')", consider_dimension, consider, subject_dimension, subject))[0][0].ToString());

                    consider_sum -= (count / all_subject_for_consider_count) * Math.Log((count / all_subject_for_consider_count), 2);
                }
                consider_sum *= all_subject_for_consider_count / all_data;
            }
            return consider_sum;
        }

        private string showSortingResult(List<EntropySortElement> list)
        {
            if (list.Count > 0)
            {
                list.Sort();

                StringBuilder sb = new StringBuilder();

                sb.Append(string.Format("Result: ({0}, {1})", list[0].Name, list[0].Value));
                for (int i = 1; i < list.Count; i++)
                    sb.Append(string.Format(" > ({0}, {1})", list[i].Name, list[i].Value));

                LogMessage(sb.ToString());
                return sb.ToString();
            }
            return "";
        }

        private class EntropySortElement : IComparable<EntropySortElement>
        {
            public string Name { get; set; }
            public double Value { get; set; }

            public int CompareTo(EntropySortElement other)
            {
                if (this.Value < other.Value) return 1;
                else if (this.Value > other.Value) return -1;
                else return 0;
            }
        }
    }
}
