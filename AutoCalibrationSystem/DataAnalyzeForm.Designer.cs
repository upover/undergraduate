namespace AutoCalibrationSystem
{
    partial class DataAnalyzeForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.timePickerStart = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.dgvDividerHistory = new System.Windows.Forms.DataGridView();
            this.SaveTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceVolt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceFrequency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StandOut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestOut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Temperature = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Humidity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnSelectData = new System.Windows.Forms.Button();
            this.numDividerId = new System.Windows.Forms.NumericUpDown();
            this.labelDividerId = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDividerHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDividerId)).BeginInit();
            this.SuspendLayout();
            // 
            // timePickerStart
            // 
            this.timePickerStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.timePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timePickerStart.Location = new System.Drawing.Point(93, 8);
            this.timePickerStart.Name = "timePickerStart";
            this.timePickerStart.Size = new System.Drawing.Size(152, 21);
            this.timePickerStart.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(13, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "开始时间:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(249, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "结束时间:";
            // 
            // timePickerEnd
            // 
            this.timePickerEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.timePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timePickerEnd.Location = new System.Drawing.Point(329, 8);
            this.timePickerEnd.Name = "timePickerEnd";
            this.timePickerEnd.Size = new System.Drawing.Size(152, 21);
            this.timePickerEnd.TabIndex = 3;
            // 
            // dgvDividerHistory
            // 
            this.dgvDividerHistory.AllowUserToOrderColumns = true;
            this.dgvDividerHistory.AllowUserToResizeColumns = false;
            this.dgvDividerHistory.AllowUserToResizeRows = false;
            this.dgvDividerHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDividerHistory.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgvDividerHistory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDividerHistory.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvDividerHistory.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("幼圆", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDividerHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDividerHistory.ColumnHeadersHeight = 40;
            this.dgvDividerHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDividerHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SaveTime,
            this.SourceVolt,
            this.SourceFrequency,
            this.StandOut,
            this.TestOut,
            this.Temperature,
            this.Humidity});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDividerHistory.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDividerHistory.EnableHeadersVisualStyles = false;
            this.dgvDividerHistory.GridColor = System.Drawing.Color.Black;
            this.dgvDividerHistory.Location = new System.Drawing.Point(12, 55);
            this.dgvDividerHistory.MinimumSize = new System.Drawing.Size(0, 32);
            this.dgvDividerHistory.Name = "dgvDividerHistory";
            this.dgvDividerHistory.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dgvDividerHistory.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvDividerHistory.RowTemplate.Height = 30;
            this.dgvDividerHistory.Size = new System.Drawing.Size(721, 596);
            this.dgvDividerHistory.StandardTab = true;
            this.dgvDividerHistory.TabIndex = 93;
            // 
            // SaveTime
            // 
            this.SaveTime.DataPropertyName = "save_time";
            this.SaveTime.HeaderText = "测量时间";
            this.SaveTime.Name = "SaveTime";
            // 
            // SourceVolt
            // 
            this.SourceVolt.DataPropertyName = "source_volt";
            this.SourceVolt.HeaderText = "源电压";
            this.SourceVolt.Name = "SourceVolt";
            // 
            // SourceFrequency
            // 
            this.SourceFrequency.DataPropertyName = "source_freq";
            this.SourceFrequency.HeaderText = "源频率";
            this.SourceFrequency.Name = "SourceFrequency";
            // 
            // StandOut
            // 
            this.StandOut.DataPropertyName = "stand_divider_volt";
            this.StandOut.HeaderText = "标准分压器电压";
            this.StandOut.Name = "StandOut";
            // 
            // TestOut
            // 
            this.TestOut.DataPropertyName = "test_divider_volt";
            this.TestOut.HeaderText = "待测分压器电压";
            this.TestOut.Name = "TestOut";
            // 
            // Temperature
            // 
            this.Temperature.DataPropertyName = "temperature";
            this.Temperature.HeaderText = "温度";
            this.Temperature.Name = "Temperature";
            // 
            // Humidity
            // 
            this.Humidity.DataPropertyName = "humidity";
            this.Humidity.HeaderText = "湿度";
            this.Humidity.Name = "Humidity";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1001, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 94;
            this.button1.Text = "稳定性监测";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(781, 8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 23);
            this.button2.TabIndex = 95;
            this.button2.Text = "相关系数分析";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(892, 8);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(93, 23);
            this.button3.TabIndex = 96;
            this.button3.Text = "多元回归分析";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // btnSelectData
            // 
            this.btnSelectData.Location = new System.Drawing.Point(667, 6);
            this.btnSelectData.Name = "btnSelectData";
            this.btnSelectData.Size = new System.Drawing.Size(66, 23);
            this.btnSelectData.TabIndex = 97;
            this.btnSelectData.Text = "选择数据";
            this.btnSelectData.UseVisualStyleBackColor = true;
            this.btnSelectData.Click += new System.EventHandler(this.btnSelectData_Click);
            // 
            // numDividerId
            // 
            this.numDividerId.Location = new System.Drawing.Point(552, 8);
            this.numDividerId.Name = "numDividerId";
            this.numDividerId.Size = new System.Drawing.Size(74, 21);
            this.numDividerId.TabIndex = 98;
            // 
            // labelDividerId
            // 
            this.labelDividerId.AutoSize = true;
            this.labelDividerId.Location = new System.Drawing.Point(490, 12);
            this.labelDividerId.Name = "labelDividerId";
            this.labelDividerId.Size = new System.Drawing.Size(59, 12);
            this.labelDividerId.TabIndex = 99;
            this.labelDividerId.Text = "分压器ID:";
            // 
            // DataAnalyzeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1249, 663);
            this.Controls.Add(this.labelDividerId);
            this.Controls.Add(this.numDividerId);
            this.Controls.Add(this.btnSelectData);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvDividerHistory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.timePickerEnd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.timePickerStart);
            this.Name = "DataAnalyzeForm";
            this.Text = "统计分析";
            this.Load += new System.EventHandler(this.DataAnalyzeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDividerHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDividerId)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker timePickerStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker timePickerEnd;
        private System.Windows.Forms.DataGridView dgvDividerHistory;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnSelectData;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaveTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceVolt;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceFrequency;
        private System.Windows.Forms.DataGridViewTextBoxColumn StandOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn Temperature;
        private System.Windows.Forms.DataGridViewTextBoxColumn Humidity;
        private System.Windows.Forms.NumericUpDown numDividerId;
        private System.Windows.Forms.Label labelDividerId;
    }
}