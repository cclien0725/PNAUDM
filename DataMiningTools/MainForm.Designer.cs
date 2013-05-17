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
            this.tbx_LogBox = new System.Windows.Forms.RichTextBox();
            this.btn_Import = new System.Windows.Forms.Button();
            this.btn_SelectFile = new System.Windows.Forms.Button();
            this.tbx_SelectedFiles = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DataProcessPage = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lb_type = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lb_src = new System.Windows.Forms.Label();
            this.btn_StartProcessData = new System.Windows.Forms.Button();
            this.tbx_LogBox2 = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbx_FillNull = new System.Windows.Forms.CheckBox();
            this.cbx_TransferIP = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbx_ClearRouterARP = new System.Windows.Forms.CheckBox();
            this.cbx_ClearUserIP = new System.Windows.Forms.CheckBox();
            this.cbx_ClearDNS = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SubProgress = new System.Windows.Forms.ProgressBar();
            this.ALLProgress = new System.Windows.Forms.ProgressBar();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cbx_ClassifyAT = new System.Windows.Forms.CheckBox();
            this.cbx_ClearIPv6 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.DataViewPage.SuspendLayout();
            this.ImportDataPage.SuspendLayout();
            this.DataProcessPage.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "SQL Command:";
            // 
            // tbx_SqlCommand
            // 
            this.tbx_SqlCommand.Location = new System.Drawing.Point(107, 8);
            this.tbx_SqlCommand.Name = "tbx_SqlCommand";
            this.tbx_SqlCommand.Size = new System.Drawing.Size(877, 20);
            this.tbx_SqlCommand.TabIndex = 1;
            // 
            // btn_SendCommand
            // 
            this.btn_SendCommand.Location = new System.Drawing.Point(990, 7);
            this.btn_SendCommand.Name = "btn_SendCommand";
            this.btn_SendCommand.Size = new System.Drawing.Size(57, 25);
            this.btn_SendCommand.TabIndex = 2;
            this.btn_SendCommand.Text = "Send";
            this.btn_SendCommand.UseVisualStyleBackColor = true;
            this.btn_SendCommand.Click += new System.EventHandler(this.btn_SendCommand_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 38);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1041, 514);
            this.dataGridView1.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.DataViewPage);
            this.tabControl1.Controls.Add(this.ImportDataPage);
            this.tabControl1.Controls.Add(this.DataProcessPage);
            this.tabControl1.Location = new System.Drawing.Point(12, 14);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1061, 586);
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
            this.DataViewPage.Size = new System.Drawing.Size(1053, 560);
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
            this.ImportDataPage.Size = new System.Drawing.Size(1053, 560);
            this.ImportDataPage.TabIndex = 1;
            this.ImportDataPage.Text = "Import Data";
            this.ImportDataPage.UseVisualStyleBackColor = true;
            // 
            // tbx_LogBox
            // 
            this.tbx_LogBox.Location = new System.Drawing.Point(3, 37);
            this.tbx_LogBox.Name = "tbx_LogBox";
            this.tbx_LogBox.Size = new System.Drawing.Size(1044, 518);
            this.tbx_LogBox.TabIndex = 7;
            this.tbx_LogBox.Text = "";
            // 
            // btn_Import
            // 
            this.btn_Import.Location = new System.Drawing.Point(975, 7);
            this.btn_Import.Name = "btn_Import";
            this.btn_Import.Size = new System.Drawing.Size(75, 25);
            this.btn_Import.TabIndex = 4;
            this.btn_Import.Text = "Import";
            this.btn_Import.UseVisualStyleBackColor = true;
            this.btn_Import.Click += new System.EventHandler(this.btn_Import_Click);
            // 
            // btn_SelectFile
            // 
            this.btn_SelectFile.Location = new System.Drawing.Point(894, 7);
            this.btn_SelectFile.Name = "btn_SelectFile";
            this.btn_SelectFile.Size = new System.Drawing.Size(75, 25);
            this.btn_SelectFile.TabIndex = 2;
            this.btn_SelectFile.Text = "Select";
            this.btn_SelectFile.UseVisualStyleBackColor = true;
            this.btn_SelectFile.Click += new System.EventHandler(this.btn_SelectFile_Click);
            // 
            // tbx_SelectedFiles
            // 
            this.tbx_SelectedFiles.Location = new System.Drawing.Point(107, 7);
            this.tbx_SelectedFiles.Name = "tbx_SelectedFiles";
            this.tbx_SelectedFiles.Size = new System.Drawing.Size(781, 20);
            this.tbx_SelectedFiles.TabIndex = 1;
            this.tbx_SelectedFiles.Click += new System.EventHandler(this.btn_SelectFile_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Select CSV files:";
            // 
            // DataProcessPage
            // 
            this.DataProcessPage.Controls.Add(this.groupBox5);
            this.DataProcessPage.Controls.Add(this.btn_StartProcessData);
            this.DataProcessPage.Controls.Add(this.tbx_LogBox2);
            this.DataProcessPage.Controls.Add(this.groupBox2);
            this.DataProcessPage.Controls.Add(this.groupBox1);
            this.DataProcessPage.Location = new System.Drawing.Point(4, 22);
            this.DataProcessPage.Name = "DataProcessPage";
            this.DataProcessPage.Padding = new System.Windows.Forms.Padding(3);
            this.DataProcessPage.Size = new System.Drawing.Size(1053, 560);
            this.DataProcessPage.TabIndex = 2;
            this.DataProcessPage.Text = "Data Process";
            this.DataProcessPage.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.groupBox4);
            this.groupBox5.Controls.Add(this.groupBox3);
            this.groupBox5.Location = new System.Drawing.Point(256, 7);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(794, 289);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Information Entropy";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lb_type);
            this.groupBox4.Location = new System.Drawing.Point(6, 149);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(782, 134);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Attack Type";
            // 
            // lb_type
            // 
            this.lb_type.AutoSize = true;
            this.lb_type.Location = new System.Drawing.Point(6, 60);
            this.lb_type.Name = "lb_type";
            this.lb_type.Size = new System.Drawing.Size(48, 15);
            this.lb_type.TabIndex = 1;
            this.lb_type.Text = "Result: ";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lb_src);
            this.groupBox3.Location = new System.Drawing.Point(6, 23);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(782, 120);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Attack Source";
            // 
            // lb_src
            // 
            this.lb_src.AutoSize = true;
            this.lb_src.Location = new System.Drawing.Point(6, 53);
            this.lb_src.Name = "lb_src";
            this.lb_src.Size = new System.Drawing.Size(48, 15);
            this.lb_src.TabIndex = 0;
            this.lb_src.Text = "Result: ";
            // 
            // btn_StartProcessData
            // 
            this.btn_StartProcessData.Location = new System.Drawing.Point(972, 530);
            this.btn_StartProcessData.Name = "btn_StartProcessData";
            this.btn_StartProcessData.Size = new System.Drawing.Size(75, 25);
            this.btn_StartProcessData.TabIndex = 3;
            this.btn_StartProcessData.Text = "Start";
            this.btn_StartProcessData.UseVisualStyleBackColor = true;
            this.btn_StartProcessData.Click += new System.EventHandler(this.btn_StartProcessData_Click);
            // 
            // tbx_LogBox2
            // 
            this.tbx_LogBox2.Location = new System.Drawing.Point(6, 302);
            this.tbx_LogBox2.Name = "tbx_LogBox2";
            this.tbx_LogBox2.Size = new System.Drawing.Size(1041, 221);
            this.tbx_LogBox2.TabIndex = 2;
            this.tbx_LogBox2.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbx_FillNull);
            this.groupBox2.Controls.Add(this.cbx_TransferIP);
            this.groupBox2.Location = new System.Drawing.Point(6, 156);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(241, 140);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data Transform Options";
            // 
            // cbx_FillNull
            // 
            this.cbx_FillNull.AutoSize = true;
            this.cbx_FillNull.Checked = true;
            this.cbx_FillNull.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_FillNull.Location = new System.Drawing.Point(6, 47);
            this.cbx_FillNull.Name = "cbx_FillNull";
            this.cbx_FillNull.Size = new System.Drawing.Size(191, 19);
            this.cbx_FillNull.TabIndex = 4;
            this.cbx_FillNull.Text = "Fill null cells to unknown value";
            this.cbx_FillNull.UseVisualStyleBackColor = true;
            // 
            // cbx_TransferIP
            // 
            this.cbx_TransferIP.AutoSize = true;
            this.cbx_TransferIP.Checked = true;
            this.cbx_TransferIP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_TransferIP.Location = new System.Drawing.Point(6, 23);
            this.cbx_TransferIP.Name = "cbx_TransferIP";
            this.cbx_TransferIP.Size = new System.Drawing.Size(220, 19);
            this.cbx_TransferIP.TabIndex = 3;
            this.cbx_TransferIP.Text = "Transform Source IP to Geolocation";
            this.cbx_TransferIP.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbx_ClearIPv6);
            this.groupBox1.Controls.Add(this.cbx_ClassifyAT);
            this.groupBox1.Controls.Add(this.cbx_ClearRouterARP);
            this.groupBox1.Controls.Add(this.cbx_ClearUserIP);
            this.groupBox1.Controls.Add(this.cbx_ClearDNS);
            this.groupBox1.Location = new System.Drawing.Point(6, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(241, 143);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Clean Options";
            // 
            // cbx_ClearRouterARP
            // 
            this.cbx_ClearRouterARP.AutoSize = true;
            this.cbx_ClearRouterARP.Checked = true;
            this.cbx_ClearRouterARP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_ClearRouterARP.Location = new System.Drawing.Point(6, 70);
            this.cbx_ClearRouterARP.Name = "cbx_ClearRouterARP";
            this.cbx_ClearRouterARP.Size = new System.Drawing.Size(122, 19);
            this.cbx_ClearRouterARP.TabIndex = 2;
            this.cbx_ClearRouterARP.Text = "Clear Router ARP";
            this.cbx_ClearRouterARP.UseVisualStyleBackColor = true;
            // 
            // cbx_ClearUserIP
            // 
            this.cbx_ClearUserIP.AutoSize = true;
            this.cbx_ClearUserIP.Checked = true;
            this.cbx_ClearUserIP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_ClearUserIP.Location = new System.Drawing.Point(6, 47);
            this.cbx_ClearUserIP.Name = "cbx_ClearUserIP";
            this.cbx_ClearUserIP.Size = new System.Drawing.Size(98, 19);
            this.cbx_ClearUserIP.TabIndex = 1;
            this.cbx_ClearUserIP.Text = "Clear User IP";
            this.cbx_ClearUserIP.UseVisualStyleBackColor = true;
            // 
            // cbx_ClearDNS
            // 
            this.cbx_ClearDNS.AutoSize = true;
            this.cbx_ClearDNS.Checked = true;
            this.cbx_ClearDNS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_ClearDNS.Location = new System.Drawing.Point(6, 23);
            this.cbx_ClearDNS.Name = "cbx_ClearDNS";
            this.cbx_ClearDNS.Size = new System.Drawing.Size(98, 19);
            this.cbx_ClearDNS.TabIndex = 0;
            this.cbx_ClearDNS.Text = "Clear DNS IP";
            this.cbx_ClearDNS.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 642);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Total Progress";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 611);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Sub Progress";
            // 
            // SubProgress
            // 
            this.SubProgress.Location = new System.Drawing.Point(108, 606);
            this.SubProgress.Name = "SubProgress";
            this.SubProgress.Size = new System.Drawing.Size(965, 25);
            this.SubProgress.TabIndex = 6;
            // 
            // ALLProgress
            // 
            this.ALLProgress.Location = new System.Drawing.Point(108, 637);
            this.ALLProgress.Name = "ALLProgress";
            this.ALLProgress.Size = new System.Drawing.Size(965, 25);
            this.ALLProgress.TabIndex = 3;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "csv files| *.csv";
            this.openFileDialog1.Multiselect = true;
            // 
            // cbx_ClassifyAT
            // 
            this.cbx_ClassifyAT.AutoSize = true;
            this.cbx_ClassifyAT.Checked = true;
            this.cbx_ClassifyAT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_ClassifyAT.Location = new System.Drawing.Point(6, 118);
            this.cbx_ClassifyAT.Name = "cbx_ClassifyAT";
            this.cbx_ClassifyAT.Size = new System.Drawing.Size(131, 19);
            this.cbx_ClassifyAT.TabIndex = 3;
            this.cbx_ClassifyAT.Text = "Classify Attack Type";
            this.cbx_ClassifyAT.UseVisualStyleBackColor = true;
            // 
            // cbx_ClearIPv6
            // 
            this.cbx_ClearIPv6.AutoSize = true;
            this.cbx_ClearIPv6.Checked = true;
            this.cbx_ClearIPv6.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_ClearIPv6.Location = new System.Drawing.Point(6, 95);
            this.cbx_ClearIPv6.Name = "cbx_ClearIPv6";
            this.cbx_ClearIPv6.Size = new System.Drawing.Size(81, 19);
            this.cbx_ClearIPv6.TabIndex = 4;
            this.cbx_ClearIPv6.Text = "Clear IPv6";
            this.cbx_ClearIPv6.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1085, 670);
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
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lb_type;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lb_src;
        private System.Windows.Forms.CheckBox cbx_ClassifyAT;
        private System.Windows.Forms.CheckBox cbx_ClearIPv6;
    }
}

