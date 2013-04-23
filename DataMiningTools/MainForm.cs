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
        public class LabelEventArgs : EventArgs
        {
            public string Text { get; set; }
            public string Target { get; set; }
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
                dataProcess.onLabelTextRecv += new EventHandler<LabelEventArgs>(dataProcess_onLabelTextRecv);
                dataProcess.StartDataProcess();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void dataProcess_onLabelTextRecv(object sender, MainForm.LabelEventArgs e)
        {
            if (InvokeRequired)
                Invoke((MethodInvoker)delegate { dataProcess_onLabelTextRecv(sender, e); });
            else
            {
                Label lb = GetType().GetField(e.Target, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(this) as Label;
                if (lb != null)
                    lb.Text = e.Text;
            }
        }

        protected void dataProcess_onLogMessageRecv(object sender, MainForm.LogEventArgs e)
        {
            if (InvokeRequired)
                Invoke((MethodInvoker)delegate { dataProcess_onLogMessageRecv(sender, e); });
            else
                showMessageOnBox(e.Message, tbx_LogBox2);
        }
    }
}
