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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dgvCaliItem = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Source = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StandOut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestOut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Frequency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Temperature = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Humidity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCaliItem)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStart.Location = new System.Drawing.Point(112, 8);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.Size = new System.Drawing.Size(152, 21);
            this.dateTimePickerStart.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(22, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "开始时间:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(291, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "结束时间:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(381, 8);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(152, 21);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // dgvCaliItem
            // 
            this.dgvCaliItem.AllowUserToOrderColumns = true;
            this.dgvCaliItem.AllowUserToResizeColumns = false;
            this.dgvCaliItem.AllowUserToResizeRows = false;
            this.dgvCaliItem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCaliItem.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgvCaliItem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvCaliItem.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvCaliItem.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("幼圆", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCaliItem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCaliItem.ColumnHeadersHeight = 40;
            this.dgvCaliItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCaliItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Time,
            this.Source,
            this.StandOut,
            this.TestOut,
            this.Frequency,
            this.Temperature,
            this.Humidity});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCaliItem.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvCaliItem.EnableHeadersVisualStyles = false;
            this.dgvCaliItem.GridColor = System.Drawing.Color.Black;
            this.dgvCaliItem.Location = new System.Drawing.Point(12, 55);
            this.dgvCaliItem.MinimumSize = new System.Drawing.Size(0, 32);
            this.dgvCaliItem.Name = "dgvCaliItem";
            this.dgvCaliItem.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dgvCaliItem.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvCaliItem.RowTemplate.Height = 30;
            this.dgvCaliItem.Size = new System.Drawing.Size(721, 596);
            this.dgvCaliItem.StandardTab = true;
            this.dgvCaliItem.TabIndex = 93;
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
            // Time
            // 
            this.Time.DataPropertyName = "Time";
            this.Time.HeaderText = "测量时间";
            this.Time.Name = "Time";
            // 
            // Source
            // 
            this.Source.DataPropertyName = "Source";
            this.Source.HeaderText = "源";
            this.Source.Name = "Source";
            // 
            // StandOut
            // 
            this.StandOut.DataPropertyName = "SourceOut";
            this.StandOut.HeaderText = "标准表";
            this.StandOut.Name = "StandOut";
            // 
            // TestOut
            // 
            this.TestOut.DataPropertyName = "TestOut";
            this.TestOut.HeaderText = "待校表";
            this.TestOut.Name = "TestOut";
            // 
            // Frequency
            // 
            this.Frequency.DataPropertyName = "Mode";
            this.Frequency.HeaderText = "频率";
            this.Frequency.Name = "Frequency";
            // 
            // Temperature
            // 
            this.Temperature.HeaderText = "温度";
            this.Temperature.Name = "Temperature";
            // 
            // Humidity
            // 
            this.Humidity.HeaderText = "湿度";
            this.Humidity.Name = "Humidity";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(640, 6);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(93, 23);
            this.button4.TabIndex = 97;
            this.button4.Text = "选择数据";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // ResultManageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1249, 663);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvCaliItem);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePickerStart);
            this.Name = "ResultManageForm";
            this.Text = "统计分析";
            this.Load += new System.EventHandler(this.DataAnalyzeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCaliItem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DataGridView dgvCaliItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Source;
        private System.Windows.Forms.DataGridViewTextBoxColumn StandOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn Frequency;
        private System.Windows.Forms.DataGridViewTextBoxColumn Temperature;
        private System.Windows.Forms.DataGridViewTextBoxColumn Humidity;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}