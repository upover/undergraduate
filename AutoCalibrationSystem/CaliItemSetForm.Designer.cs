namespace AutoCalibrationSystem
{
    partial class CaliItemSetForm
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
            this.lbRangeStart = new System.Windows.Forms.Label();
            this.udRangeStart = new System.Windows.Forms.NumericUpDown();
            this.udRangeEnd = new System.Windows.Forms.NumericUpDown();
            this.lbend = new System.Windows.Forms.Label();
            this.udPoints = new System.Windows.Forms.NumericUpDown();
            this.lbPoints = new System.Windows.Forms.Label();
            this.btnSure = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.udRangeStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udRangeEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPoints)).BeginInit();
            this.SuspendLayout();
            // 
            // lbRangeStart
            // 
            this.lbRangeStart.AutoSize = true;
            this.lbRangeStart.Location = new System.Drawing.Point(42, 44);
            this.lbRangeStart.Name = "lbRangeStart";
            this.lbRangeStart.Size = new System.Drawing.Size(65, 12);
            this.lbRangeStart.TabIndex = 2;
            this.lbRangeStart.Text = "范围上限：";
            // 
            // udRangeStart
            // 
            this.udRangeStart.Location = new System.Drawing.Point(142, 44);
            this.udRangeStart.Name = "udRangeStart";
            this.udRangeStart.Size = new System.Drawing.Size(120, 21);
            this.udRangeStart.TabIndex = 3;
            // 
            // udRangeEnd
            // 
            this.udRangeEnd.Location = new System.Drawing.Point(143, 86);
            this.udRangeEnd.Name = "udRangeEnd";
            this.udRangeEnd.Size = new System.Drawing.Size(120, 21);
            this.udRangeEnd.TabIndex = 5;
            // 
            // lbend
            // 
            this.lbend.AutoSize = true;
            this.lbend.Location = new System.Drawing.Point(42, 86);
            this.lbend.Name = "lbend";
            this.lbend.Size = new System.Drawing.Size(65, 12);
            this.lbend.TabIndex = 4;
            this.lbend.Text = "范围下限：";
            // 
            // udPoints
            // 
            this.udPoints.Location = new System.Drawing.Point(142, 129);
            this.udPoints.Name = "udPoints";
            this.udPoints.Size = new System.Drawing.Size(120, 21);
            this.udPoints.TabIndex = 7;
            // 
            // lbPoints
            // 
            this.lbPoints.AutoSize = true;
            this.lbPoints.Location = new System.Drawing.Point(42, 129);
            this.lbPoints.Name = "lbPoints";
            this.lbPoints.Size = new System.Drawing.Size(65, 12);
            this.lbPoints.TabIndex = 6;
            this.lbPoints.Text = "校准点数：";
            // 
            // btnSure
            // 
            this.btnSure.Location = new System.Drawing.Point(55, 185);
            this.btnSure.Name = "btnSure";
            this.btnSure.Size = new System.Drawing.Size(75, 23);
            this.btnSure.TabIndex = 8;
            this.btnSure.Text = "确定";
            this.btnSure.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(172, 185);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // CaliItemSetForm
            // 
            this.ClientSize = new System.Drawing.Size(314, 243);
            this.ControlBox = false;
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSure);
            this.Controls.Add(this.udPoints);
            this.Controls.Add(this.lbPoints);
            this.Controls.Add(this.udRangeEnd);
            this.Controls.Add(this.lbend);
            this.Controls.Add(this.udRangeStart);
            this.Controls.Add(this.lbRangeStart);
            this.Name = "CaliItemSetForm";
            this.Text = "校准项目配置";
            this.Load += new System.EventHandler(this.CaliItemSetForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.udRangeStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udRangeEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udPoints)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbRangeStart;
        private System.Windows.Forms.NumericUpDown udRangeStart;
        private System.Windows.Forms.NumericUpDown udRangeEnd;
        private System.Windows.Forms.Label lbend;
        private System.Windows.Forms.NumericUpDown udPoints;
        private System.Windows.Forms.Label lbPoints;
        private System.Windows.Forms.Button btnSure;
        private System.Windows.Forms.Button btnExit;
    }
}