namespace AutoCalibrationSystem
{
    partial class ResultManageForm
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
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvCaliItem = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Source = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StandOut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestOut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mode = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(112, 40);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(42, 16);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "VDC";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(160, 40);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(54, 16);
            this.checkBox2.TabIndex = 6;
            this.checkBox2.Text = "VAC V";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(220, 40);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(54, 16);
            this.checkBox3.TabIndex = 7;
            this.checkBox3.Text = "VAC F";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(395, 40);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(54, 16);
            this.checkBox4.TabIndex = 10;
            this.checkBox4.Text = "IAC F";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(335, 40);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(54, 16);
            this.checkBox5.TabIndex = 9;
            this.checkBox5.Text = "IAC V";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(287, 40);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(42, 16);
            this.checkBox6.TabIndex = 8;
            this.checkBox6.Text = "IDC";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(112, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(337, 36);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(22, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "选择模式:";
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("幼圆", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCaliItem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCaliItem.ColumnHeadersHeight = 40;
            this.dgvCaliItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCaliItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Time,
            this.Source,
            this.StandOut,
            this.TestOut,
            this.Mode});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCaliItem.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCaliItem.EnableHeadersVisualStyles = false;
            this.dgvCaliItem.GridColor = System.Drawing.Color.Black;
            this.dgvCaliItem.Location = new System.Drawing.Point(12, 81);
            this.dgvCaliItem.MinimumSize = new System.Drawing.Size(0, 32);
            this.dgvCaliItem.Name = "dgvCaliItem";
            this.dgvCaliItem.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dgvCaliItem.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvCaliItem.RowTemplate.Height = 30;
            this.dgvCaliItem.Size = new System.Drawing.Size(864, 411);
            this.dgvCaliItem.StandardTab = true;
            this.dgvCaliItem.TabIndex = 93;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(801, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 94;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(801, 42);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 95;
            this.button2.Text = "删除数据";
            this.button2.UseVisualStyleBackColor = true;
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
            // Mode
            // 
            this.Mode.DataPropertyName = "Mode";
            this.Mode.HeaderText = "模式";
            this.Mode.Name = "Mode";
            // 
            // ResultManageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 493);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvCaliItem);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.checkBox5);
            this.Controls.Add(this.checkBox6);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePickerStart);
            this.Controls.Add(this.groupBox1);
            this.Name = "ResultManageForm";
            this.Text = "结果管理";
            this.Load += new System.EventHandler(this.ResultManageForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCaliItem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvCaliItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Source;
        private System.Windows.Forms.DataGridViewTextBoxColumn StandOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mode;
    }
}