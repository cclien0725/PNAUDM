namespace DataMiningTools
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.tbx_SqlCommand = new System.Windows.Forms.TextBox();
            this.btn_SendCommand = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.DataViewPage = new System.Windows.Forms.TabPage();
            this.ImportDataPage = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbx_LogBox = new System.Windows.Forms.RichTextBox();
            this.SubProgress = new System.Windows.Forms.ProgressBar();
            this.btn_Import = new System.Windows.Forms.Button();
            this.ALLProgress = new System.Windows.Forms.ProgressBar();
            this.btn_SelectFile = new System.Windows.Forms.Button();
            this.tbx_SelectedFiles = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.DataProcessPage = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbx_LogBox2 = new System.Windows.Forms.RichTextBox();
            this.btn_StartProcessData = new System.Windows.Forms.Button();
            this.cbx_ClearDNS = new System.Windows.Forms.CheckBox();
            this.cbx_ClearUserIP = new System.Windows.Forms.CheckBox();
            this.cbx_ClearRouterARP = new System.Windows.Forms.CheckBox();
            this.cbx_TransferIP = new System.Windows.Forms.CheckBox();
            this.cbx_FillNull = new System.Windows.Forms.CheckBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_info_start = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.DataViewPage.SuspendLayout();
            this.ImportDataPage.SuspendLayout();
            this.DataProcessPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "SQL Command:";
            // 
            // tbx_SqlCommand
            // 
            this.tbx_SqlCommand.Location = new System.Drawing.Point(90, 7);
            this.tbx_SqlCommand.Name = "tbx_SqlCommand";
            this.tbx_SqlCommand.Size = new System.Drawing.Size(894, 22);
            this.tbx_SqlCommand.TabIndex = 1;
            // 
            // btn_SendCommand
            // 
            this.btn_SendCommand.Location = new System.Drawing.Point(990, 6);
            this.btn_SendCommand.Name = "btn_SendCommand";
            this.btn_SendCommand.Size = new System.Drawing.Size(57, 23);
            this.btn_SendCommand.TabIndex = 2;
            this.btn_SendCommand.Text = "Send";
            this.btn_SendCommand.UseVisualStyleBackColor = true;
            this.btn_SendCommand.Click += new System.EventHandler(this.btn_SendCommand_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 35);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1041, 474);
            this.dataGridView1.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.DataViewPage);
            this.tabControl1.Controls.Add(this.ImportDataPage);
            this.tabControl1.Controls.Add(this.DataProcessPage);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1061, 541);
            this.tabControl1.TabIndex = 4;
            // 
            // DataViewPage
            // 
            this.DataViewPage.Controls.Add(this.btn_SendCommand);
            this.DataViewPage.Controls.Add(this.dataGridView1);
            this.DataViewPage.Controls.Add(this.label1);
            this.DataViewPage.Controls.Add(this.tbx_SqlCommand);
            this.DataViewPage.Location = new System.Drawing.Point(4, 22);
            this.DataViewPage.Name = "DataViewPage";
            this.DataViewPage.Padding = new System.Windows.Forms.Padding(3);
            this.DataViewPage.Size = new System.Drawing.Size(1053, 515);
            this.DataViewPage.TabIndex = 0;
            this.DataViewPage.Text = "Data view";
            this.DataViewPage.UseVisualStyleBackColor = true;
            // 
            // ImportDataPage
            // 
            this.ImportDataPage.Controls.Add(this.tbx_LogBox);
            this.ImportDataPage.Controls.Add(this.btn_Import);
            this.ImportDataPage.Controls.Add(this.btn_SelectFile);
            this.ImportDataPage.Controls.Add(this.tbx_SelectedFiles);
            this.ImportDataPage.Controls.Add(this.label2);
            this.ImportDataPage.Location = new System.Drawing.Point(4, 22);
            this.ImportDataPage.Name = "ImportDataPage";
            this.ImportDataPage.Padding = new System.Windows.Forms.Padding(3);
            this.ImportDataPage.Size = new System.Drawing.Size(1053, 515);
            this.ImportDataPage.TabIndex = 1;
            this.ImportDataPage.Text = "Import Data";
            this.ImportDataPage.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 593);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "Total Progress";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 564);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "Sub Progress";
            // 
            // tbx_LogBox
            // 
            this.tbx_LogBox.Location = new System.Drawing.Point(3, 34);
            this.tbx_LogBox.Name = "tbx_LogBox";
            this.tbx_LogBox.Size = new System.Drawing.Size(1044, 478);
            this.tbx_LogBox.TabIndex = 7;
            this.tbx_LogBox.Text = "";
            // 
            // SubProgress
            // 
            this.SubProgress.Location = new System.Drawing.Point(92, 559);
            this.SubProgress.Name = "SubProgress";
            this.SubProgress.Size = new System.Drawing.Size(981, 23);
            this.SubProgress.TabIndex = 6;
            // 
            // btn_Import
            // 
            this.btn_Import.Location = new System.Drawing.Point(975, 6);
            this.btn_Import.Name = "btn_Import";
            this.btn_Import.Size = new System.Drawing.Size(75, 23);
            this.btn_Import.TabIndex = 4;
            this.btn_Import.Text = "Import";
            this.btn_Import.UseVisualStyleBackColor = true;
            this.btn_Import.Click += new System.EventHandler(this.btn_Import_Click);
            // 
            // ALLProgress
            // 
            this.ALLProgress.Location = new System.Drawing.Point(92, 588);
            this.ALLProgress.Name = "ALLProgress";
            this.ALLProgress.Size = new System.Drawing.Size(981, 23);
            this.ALLProgress.TabIndex = 3;
            // 
            // btn_SelectFile
            // 
            this.btn_SelectFile.Location = new System.Drawing.Point(894, 6);
            this.btn_SelectFile.Name = "btn_SelectFile";
            this.btn_SelectFile.Size = new System.Drawing.Size(75, 23);
            this.btn_SelectFile.TabIndex = 2;
            this.btn_SelectFile.Text = "Select";
            this.btn_SelectFile.UseVisualStyleBackColor = true;
            this.btn_SelectFile.Click += new System.EventHandler(this.btn_SelectFile_Click);
            // 
            // tbx_SelectedFiles
            // 
            this.tbx_SelectedFiles.Location = new System.Drawing.Point(94, 6);
            this.tbx_SelectedFiles.Name = "tbx_SelectedFiles";
            this.tbx_SelectedFiles.Size = new System.Drawing.Size(794, 22);
            this.tbx_SelectedFiles.TabIndex = 1;
            this.tbx_SelectedFiles.Click += new System.EventHandler(this.btn_SelectFile_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Select CSV files:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "csv files| *.csv";
            this.openFileDialog1.Multiselect = true;
            // 
            // DataProcessPage
            // 
            this.DataProcessPage.Controls.Add(this.btn_StartProcessData);
            this.DataProcessPage.Controls.Add(this.tbx_LogBox2);
            this.DataProcessPage.Controls.Add(this.groupBox2);
            this.DataProcessPage.Controls.Add(this.groupBox1);
            this.DataProcessPage.Location = new System.Drawing.Point(4, 22);
            this.DataProcessPage.Name = "DataProcessPage";
            this.DataProcessPage.Padding = new System.Windows.Forms.Padding(3);
            this.DataProcessPage.Size = new System.Drawing.Size(1053, 515);
            this.DataProcessPage.TabIndex = 2;
            this.DataProcessPage.Text = "Data Process";
            this.DataProcessPage.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbx_ClearRouterARP);
            this.groupBox1.Controls.Add(this.cbx_ClearUserIP);
            this.groupBox1.Controls.Add(this.cbx_ClearDNS);
            this.groupBox1.Location = new System.Drawing.Point(3, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(519, 267);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Clean Options";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbx_FillNull);
            this.groupBox2.Controls.Add(this.cbx_TransferIP);
            this.groupBox2.Location = new System.Drawing.Point(528, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(519, 267);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data Transform Options";
            // 
            // tbx_LogBox2
            // 
            this.tbx_LogBox2.Location = new System.Drawing.Point(6, 279);
            this.tbx_LogBox2.Name = "tbx_LogBox2";
            this.tbx_LogBox2.Size = new System.Drawing.Size(1041, 204);
            this.tbx_LogBox2.TabIndex = 2;
            this.tbx_LogBox2.Text = "";
            // 
            // btn_StartProcessData
            // 
            this.btn_StartProcessData.Location = new System.Drawing.Point(972, 489);
            this.btn_StartProcessData.Name = "btn_StartProcessData";
            this.btn_StartProcessData.Size = new System.Drawing.Size(75, 23);
            this.btn_StartProcessData.TabIndex = 3;
            this.btn_StartProcessData.Text = "Start";
            this.btn_StartProcessData.UseVisualStyleBackColor = true;
            this.btn_StartProcessData.Click += new System.EventHandler(this.btn_StartProcessData_Click);
            // 
            // cbx_ClearDNS
            // 
            this.cbx_ClearDNS.AutoSize = true;
            this.cbx_ClearDNS.Checked = true;
            this.cbx_ClearDNS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_ClearDNS.Location = new System.Drawing.Point(6, 21);
            this.cbx_ClearDNS.Name = "cbx_ClearDNS";
            this.cbx_ClearDNS.Size = new System.Drawing.Size(87, 16);
            this.cbx_ClearDNS.TabIndex = 0;
            this.cbx_ClearDNS.Text = "Clear DNS IP";
            this.cbx_ClearDNS.UseVisualStyleBackColor = true;
            // 
            // cbx_ClearUserIP
            // 
            this.cbx_ClearUserIP.AutoSize = true;
            this.cbx_ClearUserIP.Checked = true;
            this.cbx_ClearUserIP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_ClearUserIP.Location = new System.Drawing.Point(6, 43);
            this.cbx_ClearUserIP.Name = "cbx_ClearUserIP";
            this.cbx_ClearUserIP.Size = new System.Drawing.Size(86, 16);
            this.cbx_ClearUserIP.TabIndex = 1;
            this.cbx_ClearUserIP.Text = "Clear User IP";
            this.cbx_ClearUserIP.UseVisualStyleBackColor = true;
            // 
            // cbx_ClearRouterARP
            // 
            this.cbx_ClearRouterARP.AutoSize = true;
            this.cbx_ClearRouterARP.Checked = true;
            this.cbx_ClearRouterARP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_ClearRouterARP.Location = new System.Drawing.Point(6, 65);
            this.cbx_ClearRouterARP.Name = "cbx_ClearRouterARP";
            this.cbx_ClearRouterARP.Size = new System.Drawing.Size(109, 16);
            this.cbx_ClearRouterARP.TabIndex = 2;
            this.cbx_ClearRouterARP.Text = "Clear Router ARP";
            this.cbx_ClearRouterARP.UseVisualStyleBackColor = true;
            // 
            // cbx_TransferIP
            // 
            this.cbx_TransferIP.AutoSize = true;
            this.cbx_TransferIP.Checked = true;
            this.cbx_TransferIP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_TransferIP.Location = new System.Drawing.Point(6, 21);
            this.cbx_TransferIP.Name = "cbx_TransferIP";
            this.cbx_TransferIP.Size = new System.Drawing.Size(192, 16);
            this.cbx_TransferIP.TabIndex = 3;
            this.cbx_TransferIP.Text = "Transform Source IP to Geolocation";
            this.cbx_TransferIP.UseVisualStyleBackColor = true;
            // 
            // cbx_FillNull
            // 
            this.cbx_FillNull.AutoSize = true;
            this.cbx_FillNull.Checked = true;
            this.cbx_FillNull.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_FillNull.Location = new System.Drawing.Point(6, 43);
            this.cbx_FillNull.Name = "cbx_FillNull";
            this.cbx_FillNull.Size = new System.Drawing.Size(170, 16);
            this.cbx_FillNull.TabIndex = 4;
            this.cbx_FillNull.Text = "Fill null cells to unknown value";
            this.cbx_FillNull.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.btn_info_start);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1053, 515);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Info";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "Source";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(198, 137);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Source";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "Source Port";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "Country";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 100);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "City";
            // 
            // btn_info_start
            // 
            this.btn_info_start.Location = new System.Drawing.Point(3, 167);
            this.btn_info_start.Name = "btn_info_start";
            this.btn_info_start.Size = new System.Drawing.Size(75, 23);
            this.btn_info_start.TabIndex = 2;
            this.btn_info_start.Text = "Start";
            this.btn_info_start.UseVisualStyleBackColor = true;
            this.btn_info_start.Click += new System.EventHandler(this.btn_info_start_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Location = new System.Drawing.Point(260, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(198, 137);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Attack Type";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 69);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 12);
            this.label10.TabIndex = 2;
            this.label10.Text = "TCP flag";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 42);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 12);
            this.label11.TabIndex = 1;
            this.label11.Text = "Destination Port";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 18);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(44, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "Protocol";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1085, 618);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SubProgress);
            this.Controls.Add(this.ALLProgress);
            this.Name = "MainForm";
            this.Text = "Data Mining Tools for Security Analysis V0.1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.DataViewPage.ResumeLayout(false);
            this.DataViewPage.PerformLayout();
            this.ImportDataPage.ResumeLayout(false);
            this.ImportDataPage.PerformLayout();
            this.DataProcessPage.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbx_SqlCommand;
        private System.Windows.Forms.Button btn_SendCommand;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage DataViewPage;
        private System.Windows.Forms.TabPage ImportDataPage;
        private System.Windows.Forms.Button btn_Import;
        private System.Windows.Forms.ProgressBar ALLProgress;
        private System.Windows.Forms.Button btn_SelectFile;
        private System.Windows.Forms.TextBox tbx_SelectedFiles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ProgressBar SubProgress;
        private System.Windows.Forms.RichTextBox tbx_LogBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage DataProcessPage;
        private System.Windows.Forms.Button btn_StartProcessData;
        private System.Windows.Forms.RichTextBox tbx_LogBox2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbx_FillNull;
        private System.Windows.Forms.CheckBox cbx_TransferIP;
        private System.Windows.Forms.CheckBox cbx_ClearRouterARP;
        private System.Windows.Forms.CheckBox cbx_ClearUserIP;
        private System.Windows.Forms.CheckBox cbx_ClearDNS;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btn_info_start;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
    }
}

