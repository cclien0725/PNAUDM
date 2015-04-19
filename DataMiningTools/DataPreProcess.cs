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
        public bool ClearIPv6s { get; set; }
        public bool TransferIP2Geo { get; set; }
        public bool FillNull { get; set; }
        public bool ClassifyAT { get; set; }

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
            if (ClearIPv6s) allProcess++;
            if (ClassifyAT) allProcess++;

            // Start processes
            if (ClearDNS)
            {
                Clear140_126_1_1();
                ShowSubProgress(currentProcess++, allProcess);
            }

            if (ClearUserIP)
            {
                ClearOurUsingIP();
                ShowSubProgress(currentProcess++, allProcess);
            }

            if (ClearARPs)
            {
                ClearARP();
                ShowSubProgress(currentProcess++, allProcess);
            }

            if (ClearIPv6s)
            {
                ClearIPv6();
                ShowSubProgress(currentProcess++, allProcess);
            }

            if (ClassifyAT)
            {
                ClassifyAttackType();
                ShowSubProgress(currentProcess++, allProcess);
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
            sql.RunCommand("DELETE FROM " + Properties.Settings.Default.TABLE + " WHERE source = '140.126.1.1'");
            sql.RunCommand("DELETE FROM " + Properties.Settings.Default.TABLE + " WHERE destination = '140.126.1.1'");
            LogMessage("Deleting DNS IP...OK!");
        }

        private void ClearOurUsingIP()
        {
            LogMessage("Updating user IP...");
            sql.RunCommand("UPDATE " + Properties.Settings.Default.TABLE + " SET AT = 'Normal' WHERE source = '140.126.130.74'");
            sql.RunCommand("UPDATE " + Properties.Settings.Default.TABLE + " SET AT = 'Normal' WHERE source = '140.126.130.38'");
            sql.RunCommand("UPDATE " + Properties.Settings.Default.TABLE + " SET AT = 'Normal' WHERE source = '140.126.130.39'");
            sql.RunCommand("UPDATE " + Properties.Settings.Default.TABLE + " SET AT = 'Normal' WHERE source = '140.126.130.73'");
            sql.RunCommand("UPDATE " + Properties.Settings.Default.TABLE + " SET AT = 'Normal' WHERE source = '140.126.130.42'");
            LogMessage("Updating user IP...OK!");
        }

        private void ClearARP()
        {
            LogMessage("Deleting router ARP...");
            sql.RunCommand("DELETE FROM " + Properties.Settings.Default.TABLE + " WHERE source = 'Cisco_b3:08:44'");
            sql.RunCommand("DELETE FROM " + Properties.Settings.Default.TABLE + " WHERE source = 'Elitegro_92:a8:fe'");
            sql.RunCommand("DELETE FROM " + Properties.Settings.Default.TABLE + " WHERE source = 'Giga-Byt_4f:90:25'");
            sql.RunCommand("DELETE FROM " + Properties.Settings.Default.TABLE + " WHERE source = 'ZyxelCom_d1:45:72'");
            sql.RunCommand("DELETE FROM " + Properties.Settings.Default.TABLE + " WHERE source = 'AsustekC_f6:cb:0a'");
            sql.RunCommand("DELETE FROM " + Properties.Settings.Default.TABLE + " WHERE source = 'PlanexCo_c2:c9:7b'");
            sql.RunCommand("DELETE FROM " + Properties.Settings.Default.TABLE + " WHERE source = '12:34:56:78:90:AB'");
            LogMessage("Deleting router ARP...OK!");
        }

        private void ClearIPv6()
        {
            LogMessage("Deleting IPv6...");
            sql.RunCommand("DELETE FROM " + Properties.Settings.Default.TABLE + " WHERE Protocol = 'IPv6'");
            LogMessage("Deleting IPv6...OK!");
        }

        private void ClassifyAttackType()
        {
            LogMessage("Classifying Attack Type...");
            int allProcess;
            int currentProcess;
            string sqlCountCmd;
            string sqlUpdateCmd;
            
            #region Claasify Not Listening port
            List<AllowItem> allowList = new List<AllowItem>();
            allowList.Add(new AllowItem { Protocol = "TCP", Port = 111 });
            allowList.Add(new AllowItem { Protocol = "TCP", Port = 21 });
            allowList.Add(new AllowItem { Protocol = "TCP", Port = 22 });
            allowList.Add(new AllowItem { Protocol = "TCP", Port = 54745 });
            allowList.Add(new AllowItem { Protocol = "TCP", Port = 3306 });
            allowList.Add(new AllowItem { Protocol = "TCP", Port = 80 });
            allowList.Add(new AllowItem { Protocol = "TCP", Port = 41058 });
            allowList.Add(new AllowItem { Protocol = "UDP", Port = 111 });
            allowList.Add(new AllowItem { Protocol = "UDP", Port = 46605 });
            allowList.Add(new AllowItem { Protocol = "UDP", Port = 937 });
            allowList.Add(new AllowItem { Protocol = "UDP", Port = 936 });
            allowList.Add(new AllowItem { Protocol = "UDP", Port = 48730 });

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("WHERE DestinationPort NOT IN (" + allowList[0].Port);
            for (int i = 1; i < allowList.Count; i++)
                sb.AppendFormat(", {0}", allowList[i].Port);
            sb.Append(") and SourcePort not in (80)");

            sql.RunCommand("UPDATE " + Properties.Settings.Default.TABLE + " SET AT = 'Port_Sweep' " + sb.ToString());
            // Normal for update.
            sql.RunCommand("UPDATE " + Properties.Settings.Default.TABLE + " SET AT = 'Normal' WHERE SourcePort = 80");
            #endregion

            #region Claasify Port 22
            sqlCountCmd = "SELECT COUNT(*) FROM " + Properties.Settings.Default.TABLE + " WHERE Source = '{0}' and SourcePort = {1} and DestinationPort = 22 and AT IS NULL";
            sqlUpdateCmd = "UPDATE " + Properties.Settings.Default.TABLE + " SET AT = '{0}' WHERE Source = '{1}' and SourcePort = {2} and DestinationPort = 22 and AT IS NULL";
            DataView dt_port_22 = sql.GetResult("SELECT Source, SourcePort FROM " + Properties.Settings.Default.TABLE + " WHERE DestinationPort = 22 and AT IS NULL GROUP BY Source, SourcePort, DestinationPort");
            allProcess = dt_port_22.Table.Rows.Count;
            currentProcess = 0;

            for (int i = 0; i < dt_port_22.Table.Rows.Count; i++)
            {
                string Source = dt_port_22[i]["Source"].ToString();
                int SourcePort = Convert.ToInt32(dt_port_22[i]["SourcePort"].ToString());

                int totalCount = Convert.ToInt32(sql.GetResult(string.Format(sqlCountCmd, Source, SourcePort))[0][0].ToString());
                // Port_Sweep
                if (totalCount <= 2)
                {
                    sql.RunCommand(string.Format(sqlUpdateCmd, "Port_Sweep", Source, SourcePort));
                    //LogMessage(Source + ", " + SourcePort + ", cout: " + totalCount + " > Port_Sweep");
                }
                else
                {
                    string likeCommand = sqlCountCmd + " and Convert(varchar(255), Info) LIKE '{2}'";
                    int syncCount = Convert.ToInt32(sql.GetResult(string.Format(likeCommand, Source, SourcePort, "%SYN%"))[0][0].ToString());
                    int rstCount = Convert.ToInt32(sql.GetResult(string.Format(likeCommand, Source, SourcePort, "%RST%"))[0][0].ToString());
                    int tcpreasCount = Convert.ToInt32(sql.GetResult(string.Format(likeCommand, Source, SourcePort, "[[]TCP segment of a reassembled PDU]"))[0][0].ToString());
                    int newKeyCount = Convert.ToInt32(sql.GetResult(string.Format(likeCommand, Source, SourcePort, "%New Keys%"))[0][0].ToString());
                    int encryptCount = Convert.ToInt32(sql.GetResult(string.Format(likeCommand, Source, SourcePort, "Encrypted request packet len%"))[0][0].ToString());

                    if (syncCount > 0 && rstCount > 0 && syncCount == rstCount)
                    {
                        sql.RunCommand(string.Format(sqlUpdateCmd, "Port_Sweep", Source, SourcePort));
                        //LogMessage(Source + ", " + SourcePort + ", cout: " + totalCount + " > Port_Sweep");
                    }
                    // Guess_Password
                    else if (tcpreasCount == 3)
                    {
                        sql.RunCommand(string.Format(sqlUpdateCmd, "Guess_Password", Source, SourcePort));
                        //LogMessage(Source + ", " + SourcePort + ", cout: " + totalCount + " > Guess_Password");
                    }
                    else if (encryptCount == 3)
                    {
                        sql.RunCommand(string.Format(sqlUpdateCmd, "Guess_Password", Source, SourcePort));
                        //LogMessage(Source + ", " + SourcePort + ", " + totalCount + "/" + newKeyCount + " = 3 > Guess_Password (encrypt)");
                    }
                    else if (tcpreasCount > 3)
                    {
                        // Guess_Password
                        if (newKeyCount != 0 && tcpreasCount / newKeyCount == 3)
                        {
                            sql.RunCommand(string.Format(sqlUpdateCmd, "Guess_Password", Source, SourcePort));
                            //LogMessage(Source + ", " + SourcePort + ", " + totalCount + "/" + newKeyCount + " = 3 > Guess_Password");
                        }
                        else if (tcpreasCount == 6)
                        {
                            sql.RunCommand(string.Format(sqlUpdateCmd, "Guess_Password", Source, SourcePort));
                            //LogMessage(Source + ", " + SourcePort + ", " + totalCount + "/" + newKeyCount + " = 3 > Guess_Password (tcp)");
                        }
                        else
                        {
                            sql.RunCommand(string.Format(sqlUpdateCmd, "Normal", Source, SourcePort));
                        }
                    }
                    else
                    {
                        sql.RunCommand(string.Format(sqlUpdateCmd, "Normal", Source, SourcePort));
                    }
                }

                ShowSubProgress(++currentProcess, allProcess);
            }
            #endregion
            
            #region Classify Port 80
            sqlCountCmd = "SELECT COUNT(*) FROM " + Properties.Settings.Default.TABLE + " WHERE Source = '{0}' and SourcePort = {1} and DestinationPort = 80 and AT IS NULL";
            sqlUpdateCmd = "UPDATE " + Properties.Settings.Default.TABLE + " SET AT = '{0}' WHERE Source = '{1}' and SourcePort = {2} and DestinationPort = 80 and AT IS NULL";
            DataView dt_port_80 = sql.GetResult("SELECT Source, SourcePort FROM " + Properties.Settings.Default.TABLE + " WHERE DestinationPort = 80 and AT IS NULL GROUP BY Source, SourcePort, DestinationPort");
            allProcess = dt_port_80.Table.Rows.Count;
            currentProcess = 0;
            
            for (int i = 0; i < dt_port_80.Table.Rows.Count; i++)
            {
                string Source = dt_port_80[i]["Source"].ToString();
                int SourcePort = Convert.ToInt32(dt_port_80[i]["SourcePort"].ToString());

                int totalCount = Convert.ToInt32(sql.GetResult(string.Format(sqlCountCmd, Source, SourcePort))[0][0].ToString());
                // Port_Sweep
                if (totalCount <= 2)
                {
                    sql.RunCommand(string.Format(sqlUpdateCmd, "Port_Sweep", Source, SourcePort));
                    //LogMessage(Source + ", " + SourcePort + ", cout: " + totalCount + " > Port_Sweep");
                }
                else
                {
                    string likeCommand = sqlCountCmd + " and Convert(varchar(255), Info) LIKE '{2}'";
                    int syncCount = Convert.ToInt32(sql.GetResult(string.Format(likeCommand, Source, SourcePort, "%SYN%"))[0][0].ToString());
                    int rstCount = Convert.ToInt32(sql.GetResult(string.Format(likeCommand, Source, SourcePort, "%RST%"))[0][0].ToString());
                    int headCount = Convert.ToInt32(sql.GetResult(string.Format(likeCommand, Source, SourcePort, "HEAD%"))[0][0].ToString());
                    int robotCount = Convert.ToInt32(sql.GetResult(string.Format(likeCommand, Source, SourcePort, "GET %robots%"))[0][0].ToString());
                    int readmeCount = Convert.ToInt32(sql.GetResult(string.Format(likeCommand, Source, SourcePort, "GET %README%"))[0][0].ToString());
                    int managerCount = Convert.ToInt32(sql.GetResult(string.Format(likeCommand, Source, SourcePort, "GET %manager%"))[0][0].ToString());
                    int mutihopCount = Convert.ToInt32(sql.GetResult(string.Format(likeCommand, Source, SourcePort, "GET http://%"))[0][0].ToString());

                    if (syncCount > 0 && rstCount > 0 && syncCount == rstCount)
                    {
                        sql.RunCommand(string.Format(sqlUpdateCmd, "Port_Sweep", Source, SourcePort));
                        //LogMessage(Source + ", " + SourcePort + ", count: " + headCount + " > Port_Sweep");
                    }
                    // Brute_Force
                    else if (headCount > 0)
                    {
                        sql.RunCommand(string.Format(sqlUpdateCmd, "Brute_Force", Source, SourcePort));
                        //LogMessage(Source + ", " + SourcePort + ", count: " + headCount + " > Brute_Force (HEAD)");
                    }
                    else if (robotCount > 0)
                    {
                        sql.RunCommand(string.Format(sqlUpdateCmd, "Brute_Force", Source, SourcePort));
                        //LogMessage(Source + ", " + SourcePort + ", count: " + robotCount + " > Brute_Force (GET %robots%)");
                    }
                    else if (readmeCount > 0)
                    {
                        sql.RunCommand(string.Format(sqlUpdateCmd, "Brute_Force", Source, SourcePort));
                        //LogMessage(Source + ", " + SourcePort + ", count: " + readmeCount + " > Brute_Force (GET %README%)");
                    }
                    else if (managerCount > 0)
                    {
                        sql.RunCommand(string.Format(sqlUpdateCmd, "Brute_Force", Source, SourcePort));
                        //LogMessage(Source + ", " + SourcePort + ", count: " + managerCount + " > Brute_Force (GET %manager%)");
                    }
                    else if (mutihopCount > 0)
                    {
                        sql.RunCommand(string.Format(sqlUpdateCmd, "Multihop", Source, SourcePort));
                        //LogMessage(Source + ", " + SourcePort + ", count: " + managerCount + " > Multihop (GET http://%)");
                    }
                    else
                    {
                        sql.RunCommand(string.Format(sqlUpdateCmd, "Normal", Source, SourcePort));
                        //LogMessage(Source + ", " + SourcePort + ", count: " + totalCount + " > Normal");
                    }
                }

                ShowSubProgress(++currentProcess, allProcess);
            }
            #endregion
            

            #region Classify Port 21
            sqlCountCmd = "SELECT COUNT(*) FROM " + Properties.Settings.Default.TABLE + " WHERE Source = '{0}' and SourcePort = {1} and DestinationPort = 21 and AT IS NULL";
            sqlUpdateCmd = "UPDATE " + Properties.Settings.Default.TABLE + " SET AT = '{0}' WHERE Source = '{1}' and SourcePort = {2} and DestinationPort = 21 and AT IS NULL";
            DataView dt_port_21 = sql.GetResult("SELECT Source, SourcePort FROM " + Properties.Settings.Default.TABLE + " WHERE DestinationPort = 21 and AT IS NULL GROUP BY Source, SourcePort, DestinationPort");
            allProcess = dt_port_21.Table.Rows.Count;
            currentProcess = 0;

            for (int i = 0; i < dt_port_21.Table.Rows.Count; i++)
            {
                string Source = dt_port_21[i]["Source"].ToString();
                int SourcePort = Convert.ToInt32(dt_port_21[i]["SourcePort"].ToString());

                int totalCount = Convert.ToInt32(sql.GetResult(string.Format(sqlCountCmd, Source, SourcePort))[0][0].ToString());
                // Port_Sweep
                if (totalCount <= 2)
                {
                    sql.RunCommand(string.Format(sqlUpdateCmd, "Port_Sweep", Source, SourcePort));
                    //LogMessage(Source + ", " + SourcePort + ", cout: " + totalCount + " > Port_Sweep");
                }
                else
                {
                    string likeCommand = sqlCountCmd + " and Convert(varchar(255), Info) LIKE '{2}'";
                    int syncCount = Convert.ToInt32(sql.GetResult(string.Format(likeCommand, Source, SourcePort, "%SYN%"))[0][0].ToString());
                    int rstCount = Convert.ToInt32(sql.GetResult(string.Format(likeCommand, Source, SourcePort, "%RST%"))[0][0].ToString());
                    int count = Convert.ToInt32(sql.GetResult(string.Format(likeCommand, Source, SourcePort, "%anonymous%"))[0][0].ToString());

                    // Port_Sweep
                    if (syncCount > 0 && rstCount > 0 && syncCount == rstCount)
                    {
                        sql.RunCommand(string.Format(sqlUpdateCmd, "Port_Sweep", Source, SourcePort));
                        //LogMessage(Source + ", " + SourcePort + ", cout: " + totalCount + " > Port_Sweep");
                    }
                    else if (count > 0)
                    {
                        sql.RunCommand(string.Format(sqlUpdateCmd, "Guess_Password", Source, SourcePort));
                       //LogMessage(Source + ", " + SourcePort + ", cout: " + totalCount + " > Guess_Password");
                    }
                    else
                    {
                        sql.RunCommand(string.Format(sqlUpdateCmd, "Normal", Source, SourcePort));
                        //LogMessage(Source + ", " + SourcePort + ", cout: " + totalCount + " > Normal");
                    }
                }

                ShowSubProgress(++currentProcess, allProcess);
            }

            #endregion

            #region Classify Port 3306
            sqlCountCmd = "SELECT COUNT(*) FROM " + Properties.Settings.Default.TABLE + " WHERE Source = '{0}' and SourcePort = {1} and DestinationPort = 3306 and AT IS NULL";
            sqlUpdateCmd = "UPDATE " + Properties.Settings.Default.TABLE + " SET AT = '{0}' WHERE Source = '{1}' and SourcePort = {2} and DestinationPort = 3306 and AT IS NULL";
            DataView dt_port_3306 = sql.GetResult("SELECT Source, SourcePort FROM " + Properties.Settings.Default.TABLE + " WHERE DestinationPort = 3306 and AT IS NULL GROUP BY Source, SourcePort, DestinationPort");
            allProcess = dt_port_3306.Table.Rows.Count;
            currentProcess = 0;

            for (int i = 0; i < dt_port_3306.Table.Rows.Count; i++)
            {
                string Source = dt_port_3306[i]["Source"].ToString();
                int SourcePort = Convert.ToInt32(dt_port_3306[i]["SourcePort"].ToString());

                int totalCount = Convert.ToInt32(sql.GetResult(string.Format(sqlCountCmd, Source, SourcePort))[0][0].ToString());
                // Port_Sweep
                if (totalCount <= 2)
                {
                    sql.RunCommand(string.Format(sqlUpdateCmd, "Port_Sweep", Source, SourcePort));
                    //LogMessage(Source + ", " + SourcePort + ", cout: " + totalCount + " > Port_Sweep");
                }
                else
                {
                    string likeCommand = sqlCountCmd + " and Convert(varchar(255), Info) LIKE '{2}'";
                    int syncCount = Convert.ToInt32(sql.GetResult(string.Format(likeCommand, Source, SourcePort, "%SYN%"))[0][0].ToString());
                    int rstCount = Convert.ToInt32(sql.GetResult(string.Format(likeCommand, Source, SourcePort, "%RST%"))[0][0].ToString());

                    // Port_Sweep
                    if (syncCount > 0 && rstCount > 0 && syncCount == rstCount)
                    {
                        sql.RunCommand(string.Format(sqlUpdateCmd, "Port_Sweep", Source, SourcePort));
                        //LogMessage(Source + ", " + SourcePort + ", cout: " + totalCount + " > Port_Sweep");
                    }
                    else
                    {
                        sql.RunCommand(string.Format(sqlUpdateCmd, "Guess_Password", Source, SourcePort));
                        //LogMessage(Source + ", " + SourcePort + ", cout: " + totalCount + " > Guess_Password");
                    }
                }

                ShowSubProgress(++currentProcess, allProcess);
            }

            #endregion
            
            LogMessage("Classifying Attack Type...OK!");
        }

        private void TransformIP2GeoLocation()
        {
            try
            {
                LogMessage("Transforming source IP to geolocation...");

                DataView dt = sql.GetResult("SELECT Source FROM " + Properties.Settings.Default.TABLE + " GROUP BY Source");
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
                            sql.RunCommand(string.Format("UPDATE " + Properties.Settings.Default.TABLE + " SET Country = '{0}', City = '{1}' WHERE Source = '{2}'", geoDt[0]["country"], geoDt[0]["city"], IP));
                        else
                            sql.RunCommand(string.Format("UPDATE " + Properties.Settings.Default.TABLE + " SET Country = '{0}', City = '{1}' WHERE Source = '{2}'", "unknown", "unknown", IP));
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
            sql.RunCommand("UPDATE " + Properties.Settings.Default.TABLE + " SET City = 'unknown' WHERE City = ''");
            sql.RunCommand("UPDATE " + Properties.Settings.Default.TABLE + " SET TCPflags = 'unknown' WHERE TCPflags = ''");
            LogMessage("Filling null cells with \"unknown\"...OK!");
        }

        private void InformationEntropy()
        {
            LogMessage("Starting calculate information entropy...");

            int currentProcess = 0;
            int allProcess = 6;

            double all_data = int.Parse(sql.GetResult("SELECT Count(*) FROM " + Properties.Settings.Default.TABLE + "")[0][0].ToString());
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

            DataView dt_subject = sql.GetResult(string.Format("SELECT {0} FROM " + Properties.Settings.Default.TABLE + " GROUP BY {0}", subject_dimension));
            all_subject_data = dt_subject.Table.Rows.Count;

            for (int i = 0; i < all_subject_data; i++)
            {
                string subject = dt_subject[i][subject_dimension].ToString();
                double count;

                DataView dt1 = sql.GetResult(string.Format("SELECT Count(*) FROM " + Properties.Settings.Default.TABLE + " WHERE ({0} = '{1}')", subject_dimension, subject));
                count = int.Parse(dt1[0][0].ToString());

                subject_sum -= (count / all_data) * Math.Log((count / all_data), 2);
            }

            return subject_sum;
        }

        private double calcConsiderEntropyWithSubject(double all_data, string consider_dimension, string subject_dimension)
        {
            DataView dt_consider = sql.GetResult(string.Format("SELECT {0} FROM " + Properties.Settings.Default.TABLE + " GROUP BY {0}", consider_dimension));
            double consider_sum = 0;

            for (int i = 0; i < dt_consider.Table.Rows.Count; i++)
            {
                string consider = dt_consider[i][consider_dimension].ToString();

                double all_subject_for_consider_count = int.Parse(sql.GetResult(string.Format("SELECT Count(*) FROM " + Properties.Settings.Default.TABLE + " WHERE ({0} = '{1}')", consider_dimension, consider))[0][0].ToString());
                DataView dt = sql.GetResult(string.Format("SELECT {0} FROM " + Properties.Settings.Default.TABLE + " WHERE ({1} = '{2}') GROUP BY {0}", subject_dimension, consider_dimension, consider));

                for (int j = 0; j < dt.Table.Rows.Count; j++)
                {
                    string subject = dt[j][subject_dimension].ToString();
                    double count = int.Parse(sql.GetResult(string.Format("SELECT Count(*) FROM " + Properties.Settings.Default.TABLE + " WHERE ({0} = '{1}') and ({2} = '{3}')", consider_dimension, consider, subject_dimension, subject))[0][0].ToString());

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

        private class AllowItem
        {
            public string Protocol { get; set; }
            public int Port { get; set; }
        }
    }
}
