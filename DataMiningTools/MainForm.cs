using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace DataMiningTools
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        public class ProgressEventArgs : EventArgs
        {
            public int Progress { get; set; }
        }
        public class LogEventArgs : EventArgs
        {
            public string Message { get; set; }
        }
        private SQLUtility sql;
        private string[] CSVFiles;
        private ImportData import;
        private DataPreProcess dataProcess;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btn_SelectFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbx_SelectedFiles.Text = string.Empty;
                CSVFiles = openFileDialog1.FileNames;
                foreach (string fileName in CSVFiles)
                    tbx_SelectedFiles.Text += string.Format("{0};", fileName);
                showMessageOnBox("Selected files.", tbx_LogBox);
            }
        }

        private void btn_Import_Click(object sender, EventArgs e)
        {
            try
            {
                ParameterizedThreadStart t = new ParameterizedThreadStart(StartImportData);
                StartThread(t, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void onSubProgressRecv(object sender, ProgressEventArgs e)
        {
            //SubProgress.Value = e.Progress;
            MainFormUICallBack("Value", e.Progress, typeof(ProgressBar), SubProgress);
        }
        protected void onProgressRecv(object sender, ProgressEventArgs e)
        {
            //ALLProgress.Value = e.Progress;
            MainFormUICallBack("Value", e.Progress, typeof(ProgressBar), ALLProgress);
        }
        protected void import_onLogMessageRecv(object sender, LogEventArgs e)
        {
            if (InvokeRequired)
                Invoke((MethodInvoker)delegate { import_onLogMessageRecv(sender, e); });
            else
                showMessageOnBox(e.Message, tbx_LogBox);
        }
        protected void showMessageOnBox(string message, RichTextBox LogBox)
        {
            LogBox.AppendText(string.Format("[{0}] {1}" + Environment.NewLine, DateTime.Now.ToLongTimeString(), message));
            if (LogBox.Lines.Length >= 100)
            {
                int totalLen = LogBox.Lines.Length;
                int copyLen = totalLen / 2;
                string[] str_arr = new string[totalLen - copyLen];

                Array.Copy(LogBox.Lines, copyLen, str_arr, 0, str_arr.Length);
                LogBox.Clear();

                foreach (string s in str_arr)
                    if (s != string.Empty)
                        LogBox.AppendText(s + Environment.NewLine);
            }
            LogBox.ScrollToCaret();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            tbx_SqlCommand.Text = "SELECT TOP(100) * FROM Logs";
            sql = new SQLUtility();
            ParameterizedThreadStart t = new ParameterizedThreadStart(SendCommand);
            StartThread(t, "");
            //dataGridView1.DataSource = sql.GetResult(tbx_SqlCommand.Text);
        }
        private void btn_SendCommand_Click(object sender, EventArgs e)
        {
            ParameterizedThreadStart t = new ParameterizedThreadStart(SendCommand);
            StartThread(t, "");
            //dataGridView1.DataSource = sql.GetResult(tbx_SqlCommand.Text);
        }

        private void StartThread(ParameterizedThreadStart t, object parameter)
        {
            Thread thread = new Thread(t);
            thread.Start(parameter);
        }
        private void SendCommand(object para)
        {
            //dataGridView1.DataSource = sql.GetResult(tbx_SqlCommand.Text);
            try
            {
                MainFormUICallBack("DataSource", sql.GetResult(tbx_SqlCommand.Text), typeof(DataGridView), dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void StartImportData(object para)
        {
            import = new ImportData(CSVFiles);
            import.onLogMessageRecv += new EventHandler<LogEventArgs>(import_onLogMessageRecv);
            import.onProgressRecv += new EventHandler<ProgressEventArgs>(onProgressRecv);
            import.onSubProgressRecv += new EventHandler<ProgressEventArgs>(onSubProgressRecv);
            import.StartImport();
        }

        // Cross UI Thread
        private delegate void UICallBack(string propertyName, object obj, Type ctlType, object ctl);
        private void MainFormUICallBack(string propertyName, object obj, Type ctlType, object ctl)
        {
            if (InvokeRequired)
            {
                UICallBack update = new UICallBack(MainFormUICallBack);
                Invoke(update, propertyName, obj, ctlType, ctl);
            }
            else 
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(ctlType);
                object _obj = Convert.ChangeType(ctl, ctlType);
                properties.Find(propertyName, true).SetValue(_obj, obj);
            }
        }

        private void btn_StartProcessData_Click(object sender, EventArgs e)
        {
            ParameterizedThreadStart t = new ParameterizedThreadStart(StartProcessData);
            StartThread(t, "");
        }
        private void StartProcessData(object para)
        {
            try
            {
                dataProcess = new DataPreProcess
                                  {
                                      ClearARPs = cbx_ClearRouterARP.Checked,
                                      ClearUserIP = cbx_ClearUserIP.Checked,
                                      ClearDNS = cbx_ClearDNS.Checked,
                                      TransferIP2Geo = cbx_TransferIP.Checked,
                                      FillNull = cbx_FillNull.Checked
                                  };
                dataProcess.onLogMessageRecv += new EventHandler<LogEventArgs>(dataProcess_onLogMessageRecv);
                dataProcess.onProgressRecv += new EventHandler<ProgressEventArgs>(onProgressRecv);
                dataProcess.onSubProgressRecv += new EventHandler<ProgressEventArgs>(onSubProgressRecv);
                dataProcess.StartDataProcess();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void dataProcess_onLogMessageRecv(object sender, MainForm.LogEventArgs e)
        {
            if (InvokeRequired)
                Invoke((MethodInvoker)delegate { dataProcess_onLogMessageRecv(sender, e); });
            else
                showMessageOnBox(e.Message, tbx_LogBox2);
        }

        private void btn_info_start_Click(object sender, EventArgs e)
        {
            int all_source_data;
            double all_data, sum = 0;

            DataView dt = sql.GetResult("SELECT Count(*) FROM Logs");
            all_data = int.Parse(dt[0][0].ToString());

            dt = sql.GetResult("SELECT Source From Logs GROUP BY Source");
            all_source_data = dt.Table.Rows.Count;
            
            for (int i = 0; i < all_source_data; i++)
            {
                string ip = dt[i]["Source"].ToString();
                double count;

                DataView dt1 = sql.GetResult(string.Format("SELECT Count(*) FROM Logs WHERE (Source = '{0}')", ip));
                count = int.Parse(dt1[0][0].ToString());

                sum -= (count / all_data) * Math.Log((count / all_data), 2);
                File.AppendAllText(Environment.CurrentDirectory + "cal.txt", string.Format("{0}\n", count));
                //if (sum > 1)
                //{

                //}
            }
            label5.Text += ": " + sum; 

        }
    }
}
