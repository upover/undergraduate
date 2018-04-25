namespace AutoCalibrationSystem
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Button btnDeleteAllPoint;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btLoginout = new System.Windows.Forms.Button();
            this.btncomset = new System.Windows.Forms.Button();
            this.btnresult = new System.Windows.Forms.Button();
            this.btnuser = new System.Windows.Forms.Button();
            this.btncaliset = new System.Windows.Forms.Button();
            this.btnstartcali = new System.Windows.Forms.Button();
            this.btnsavecali = new System.Windows.Forms.Button();
            this.dgvCaliItem = new System.Windows.Forms.DataGridView();
            this.Mode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Source = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StandOut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestOut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddPoint = new System.Windows.Forms.Button();
            this.btnInsertPoint = new System.Windows.Forms.Button();
            this.btnDeletePoint = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.rbVDCL = new System.Windows.Forms.RadioButton();
            this.rbVDCHP = new System.Windows.Forms.RadioButton();
            this.rbVACVL = new System.Windows.Forms.RadioButton();
            this.rbVACVH = new System.Windows.Forms.RadioButton();
            this.rbVACF = new System.Windows.Forms.RadioButton();
            this.rbIDC = new System.Windows.Forms.RadioButton();
            this.rbIACI = new System.Windows.Forms.RadioButton();
            this.rbIACF = new System.Windows.Forms.RadioButton();
            this.rbUnitSmall = new System.Windows.Forms.RadioButton();
            this.rbUnitBig = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbVDCHN = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textSend = new System.Windows.Forms.TextBox();
            this.textReceive = new System.Windows.Forms.TextBox();
            this.btnPrePage = new System.Windows.Forms.Button();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.labelCurPage = new System.Windows.Forms.Label();
            this.labelTotalRecord = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btncancelcali = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbTypeAll = new System.Windows.Forms.RadioButton();
            this.rbTypeCur = new System.Windows.Forms.RadioButton();
            this.btnsendcali = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnanalyze = new System.Windows.Forms.Button();
            this.progressBarCali = new System.Windows.Forms.ProgressBar();
            this.btnpausecali = new System.Windows.Forms.Button();
            this.labelprogress = new System.Windows.Forms.Label();
            this.labelcompleted = new System.Windows.Forms.Label();
            btnDeleteAllPoint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCaliItem)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDeleteAllPoint
            // 
            btnDeleteAllPoint.Location = new System.Drawing.Point(182, 509);
            btnDeleteAllPoint.Name = "btnDeleteAllPoint";
            btnDeleteAllPoint.Size = new System.Drawing.Size(75, 23);
            btnDeleteAllPoint.TabIndex = 96;
            btnDeleteAllPoint.Text = "全部删除";
            btnDeleteAllPoint.UseVisualStyleBackColor = true;
            // 
            // btLoginout
            // 
            this.btLoginout.Image = ((System.Drawing.Image)(resources.GetObject("btLoginout.Image")));
            this.btLoginout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btLoginout.Location = new System.Drawing.Point(1156, 620);
            this.btLoginout.Name = "btLoginout";
            this.btLoginout.Size = new System.Drawing.Size(58, 23);
            this.btLoginout.TabIndex = 83;
            this.btLoginout.Text = "退出";
            this.btLoginout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btLoginout.UseVisualStyleBackColor = true;
            this.btLoginout.Click += new System.EventHandler(this.btLoginout_Click);
            // 
            // btncomset
            // 
            this.btncomset.Location = new System.Drawing.Point(104, 2);
            this.btncomset.Name = "btncomset";
            this.btncomset.Size = new System.Drawing.Size(75, 23);
            this.btncomset.TabIndex = 84;
            this.btncomset.Text = "通信配置";
            this.btncomset.UseVisualStyleBackColor = true;
            this.btncomset.Click += new System.EventHandler(this.btncomset_Click);
            // 
            // btnresult
            // 
            this.btnresult.Location = new System.Drawing.Point(194, 2);
            this.btnresult.Name = "btnresult";
            this.btnresult.Size = new System.Drawing.Size(75, 23);
            this.btnresult.TabIndex = 85;
            this.btnresult.Text = "结果管理";
            this.btnresult.UseVisualStyleBackColor = true;
            this.btnresult.Click += new System.EventHandler(this.btnresult_Click);
            // 
            // btnuser
            // 
            this.btnuser.Location = new System.Drawing.Point(370, 2);
            this.btnuser.Name = "btnuser";
            this.btnuser.Size = new System.Drawing.Size(75, 23);
            this.btnuser.TabIndex = 86;
            this.btnuser.Text = "用户管理";
            this.btnuser.UseVisualStyleBackColor = true;
            // 
            // btncaliset
            // 
            this.btncaliset.Location = new System.Drawing.Point(12, 2);
            this.btncaliset.Name = "btncaliset";
            this.btncaliset.Size = new System.Drawing.Size(75, 23);
            this.btncaliset.TabIndex = 87;
            this.btncaliset.Text = "校准项配置";
            this.btncaliset.UseVisualStyleBackColor = true;
            this.btncaliset.Click += new System.EventHandler(this.btncaliset_Click);
            // 
            // btnstartcali
            // 
            this.btnstartcali.Location = new System.Drawing.Point(12, 590);
            this.btnstartcali.Name = "btnstartcali";
            this.btnstartcali.Size = new System.Drawing.Size(75, 23);
            this.btnstartcali.TabIndex = 88;
            this.btnstartcali.Text = "开始校准";
            this.btnstartcali.UseVisualStyleBackColor = true;
            this.btnstartcali.Click += new System.EventHandler(this.btnstartcali_Click);
            // 
            // btnsavecali
            // 
            this.btnsavecali.Location = new System.Drawing.Point(272, 590);
            this.btnsavecali.Name = "btnsavecali";
            this.btnsavecali.Size = new System.Drawing.Size(75, 23);
            this.btnsavecali.TabIndex = 89;
            this.btnsavecali.Text = "保存结果";
            this.btnsavecali.UseVisualStyleBackColor = true;
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
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("幼圆", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCaliItem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvCaliItem.ColumnHeadersHeight = 48;
            this.dgvCaliItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCaliItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Mode,
            this.Source,
            this.StandOut,
            this.TestOut,
            this.State});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCaliItem.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvCaliItem.EnableHeadersVisualStyles = false;
            this.dgvCaliItem.GridColor = System.Drawing.Color.Black;
            this.dgvCaliItem.Location = new System.Drawing.Point(11, 98);
            this.dgvCaliItem.MinimumSize = new System.Drawing.Size(0, 32);
            this.dgvCaliItem.Name = "dgvCaliItem";
            this.dgvCaliItem.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dgvCaliItem.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvCaliItem.RowTemplate.Height = 30;
            this.dgvCaliItem.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvCaliItem.Size = new System.Drawing.Size(916, 370);
            this.dgvCaliItem.StandardTab = true;
            this.dgvCaliItem.TabIndex = 92;
            this.dgvCaliItem.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCaliItem_CellContentClick);
            // 
            // Mode
            // 
            this.Mode.DataPropertyName = "Mode";
            this.Mode.HeaderText = "模式";
            this.Mode.Name = "Mode";
            // 
            // Source
            // 
            this.Source.DataPropertyName = "Source";
            this.Source.HeaderText = "源";
            this.Source.Name = "Source";
            // 
            // StandOut
            // 
            this.StandOut.DataPropertyName = "StandOut";
            this.StandOut.HeaderText = "标准表";
            this.StandOut.Name = "StandOut";
            // 
            // TestOut
            // 
            this.TestOut.DataPropertyName = "TestOut";
            this.TestOut.HeaderText = "待校表";
            this.TestOut.Name = "TestOut";
            // 
            // State
            // 
            this.State.DataPropertyName = "State";
            this.State.HeaderText = "状态";
            this.State.Name = "State";
            // 
            // btnAddPoint
            // 
            this.btnAddPoint.Location = new System.Drawing.Point(12, 480);
            this.btnAddPoint.Name = "btnAddPoint";
            this.btnAddPoint.Size = new System.Drawing.Size(75, 23);
            this.btnAddPoint.TabIndex = 93;
            this.btnAddPoint.Text = "增加点";
            this.btnAddPoint.UseVisualStyleBackColor = true;
            this.btnAddPoint.Click += new System.EventHandler(this.btnAddPoint_Click);
            // 
            // btnInsertPoint
            // 
            this.btnInsertPoint.Location = new System.Drawing.Point(11, 509);
            this.btnInsertPoint.Name = "btnInsertPoint";
            this.btnInsertPoint.Size = new System.Drawing.Size(75, 23);
            this.btnInsertPoint.TabIndex = 94;
            this.btnInsertPoint.Text = "插入点";
            this.btnInsertPoint.UseVisualStyleBackColor = true;
            this.btnInsertPoint.Click += new System.EventHandler(this.btnInsertPoint_Click);
            // 
            // btnDeletePoint
            // 
            this.btnDeletePoint.Location = new System.Drawing.Point(182, 480);
            this.btnDeletePoint.Name = "btnDeletePoint";
            this.btnDeletePoint.Size = new System.Drawing.Size(75, 23);
            this.btnDeletePoint.TabIndex = 95;
            this.btnDeletePoint.Text = "删除点";
            this.btnDeletePoint.UseVisualStyleBackColor = true;
            this.btnDeletePoint.Click += new System.EventHandler(this.btnDeletePoint_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(272, 480);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 97;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(970, 183);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 98;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(970, 412);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 99;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // rbVDCL
            // 
            this.rbVDCL.AutoSize = true;
            this.rbVDCL.Location = new System.Drawing.Point(323, 20);
            this.rbVDCL.Name = "rbVDCL";
            this.rbVDCL.Size = new System.Drawing.Size(65, 16);
            this.rbVDCL.TabIndex = 100;
            this.rbVDCL.TabStop = true;
            this.rbVDCL.Text = "VDC Low";
            this.rbVDCL.UseVisualStyleBackColor = true;
            this.rbVDCL.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // rbVDCHP
            // 
            this.rbVDCHP.AutoSize = true;
            this.rbVDCHP.Location = new System.Drawing.Point(487, 20);
            this.rbVDCHP.Name = "rbVDCHP";
            this.rbVDCHP.Size = new System.Drawing.Size(77, 16);
            this.rbVDCHP.TabIndex = 101;
            this.rbVDCHP.TabStop = true;
            this.rbVDCHP.Text = "VDC H Pos";
            this.rbVDCHP.UseVisualStyleBackColor = true;
            this.rbVDCHP.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // rbVACVL
            // 
            this.rbVACVL.AutoSize = true;
            this.rbVACVL.Location = new System.Drawing.Point(238, 20);
            this.rbVACVL.Name = "rbVACVL";
            this.rbVACVL.Size = new System.Drawing.Size(77, 16);
            this.rbVACVL.TabIndex = 102;
            this.rbVACVL.TabStop = true;
            this.rbVACVL.Text = "VAC V Low";
            this.rbVACVL.UseVisualStyleBackColor = true;
            this.rbVACVL.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // rbVACVH
            // 
            this.rbVACVH.AutoSize = true;
            this.rbVACVH.Location = new System.Drawing.Point(396, 20);
            this.rbVACVH.Name = "rbVACVH";
            this.rbVACVH.Size = new System.Drawing.Size(83, 16);
            this.rbVACVH.TabIndex = 103;
            this.rbVACVH.TabStop = true;
            this.rbVACVH.Text = "VAC V High";
            this.rbVACVH.UseVisualStyleBackColor = true;
            this.rbVACVH.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // rbVACF
            // 
            this.rbVACF.AutoSize = true;
            this.rbVACF.Location = new System.Drawing.Point(177, 20);
            this.rbVACF.Name = "rbVACF";
            this.rbVACF.Size = new System.Drawing.Size(53, 16);
            this.rbVACF.TabIndex = 104;
            this.rbVACF.TabStop = true;
            this.rbVACF.Text = "VAC F";
            this.rbVACF.UseVisualStyleBackColor = true;
            this.rbVACF.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // rbIDC
            // 
            this.rbIDC.AutoSize = true;
            this.rbIDC.Location = new System.Drawing.Point(128, 20);
            this.rbIDC.Name = "rbIDC";
            this.rbIDC.Size = new System.Drawing.Size(41, 16);
            this.rbIDC.TabIndex = 105;
            this.rbIDC.TabStop = true;
            this.rbIDC.Text = "IDC";
            this.rbIDC.UseVisualStyleBackColor = true;
            this.rbIDC.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // rbIACI
            // 
            this.rbIACI.AutoSize = true;
            this.rbIACI.Location = new System.Drawing.Point(6, 20);
            this.rbIACI.Name = "rbIACI";
            this.rbIACI.Size = new System.Drawing.Size(53, 16);
            this.rbIACI.TabIndex = 106;
            this.rbIACI.TabStop = true;
            this.rbIACI.Text = "IAC I";
            this.rbIACI.UseVisualStyleBackColor = true;
            this.rbIACI.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // rbIACF
            // 
            this.rbIACF.AutoSize = true;
            this.rbIACF.Location = new System.Drawing.Point(67, 20);
            this.rbIACF.Name = "rbIACF";
            this.rbIACF.Size = new System.Drawing.Size(53, 16);
            this.rbIACF.TabIndex = 107;
            this.rbIACF.TabStop = true;
            this.rbIACF.Text = "IAC F";
            this.rbIACF.UseVisualStyleBackColor = true;
            this.rbIACF.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // rbUnitSmall
            // 
            this.rbUnitSmall.AutoSize = true;
            this.rbUnitSmall.Location = new System.Drawing.Point(15, 20);
            this.rbUnitSmall.Name = "rbUnitSmall";
            this.rbUnitSmall.Size = new System.Drawing.Size(29, 16);
            this.rbUnitSmall.TabIndex = 109;
            this.rbUnitSmall.TabStop = true;
            this.rbUnitSmall.Text = "V";
            this.rbUnitSmall.UseVisualStyleBackColor = true;
            // 
            // rbUnitBig
            // 
            this.rbUnitBig.AutoSize = true;
            this.rbUnitBig.Location = new System.Drawing.Point(66, 20);
            this.rbUnitBig.Name = "rbUnitBig";
            this.rbUnitBig.Size = new System.Drawing.Size(35, 16);
            this.rbUnitBig.TabIndex = 110;
            this.rbUnitBig.TabStop = true;
            this.rbUnitBig.Text = "kV";
            this.rbUnitBig.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbVDCHN);
            this.groupBox1.Controls.Add(this.rbVDCL);
            this.groupBox1.Controls.Add(this.rbVDCHP);
            this.groupBox1.Controls.Add(this.rbIACF);
            this.groupBox1.Controls.Add(this.rbVACVL);
            this.groupBox1.Controls.Add(this.rbIACI);
            this.groupBox1.Controls.Add(this.rbVACVH);
            this.groupBox1.Controls.Add(this.rbIDC);
            this.groupBox1.Controls.Add(this.rbVACF);
            this.groupBox1.Location = new System.Drawing.Point(12, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(656, 51);
            this.groupBox1.TabIndex = 111;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "当前校准项";
            // 
            // rbVDCHN
            // 
            this.rbVDCHN.AutoSize = true;
            this.rbVDCHN.Location = new System.Drawing.Point(572, 20);
            this.rbVDCHN.Name = "rbVDCHN";
            this.rbVDCHN.Size = new System.Drawing.Size(77, 16);
            this.rbVDCHN.TabIndex = 108;
            this.rbVDCHN.TabStop = true;
            this.rbVDCHN.Text = "VDC H Neg";
            this.rbVDCHN.UseVisualStyleBackColor = true;
            this.rbVDCHN.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbUnitSmall);
            this.groupBox2.Controls.Add(this.rbUnitBig);
            this.groupBox2.Location = new System.Drawing.Point(681, 41);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(120, 51);
            this.groupBox2.TabIndex = 112;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "单位";
            // 
            // textSend
            // 
            this.textSend.Location = new System.Drawing.Point(970, 52);
            this.textSend.Multiline = true;
            this.textSend.Name = "textSend";
            this.textSend.Size = new System.Drawing.Size(244, 113);
            this.textSend.TabIndex = 113;
            // 
            // textReceive
            // 
            this.textReceive.Location = new System.Drawing.Point(970, 216);
            this.textReceive.Multiline = true;
            this.textReceive.Name = "textReceive";
            this.textReceive.Size = new System.Drawing.Size(244, 179);
            this.textReceive.TabIndex = 114;
            this.textReceive.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnPrePage
            // 
            this.btnPrePage.Location = new System.Drawing.Point(93, 480);
            this.btnPrePage.Name = "btnPrePage";
            this.btnPrePage.Size = new System.Drawing.Size(75, 23);
            this.btnPrePage.TabIndex = 115;
            this.btnPrePage.Text = "上一页";
            this.btnPrePage.UseVisualStyleBackColor = true;
            this.btnPrePage.Click += new System.EventHandler(this.btnPrePage_Click);
            // 
            // btnNextPage
            // 
            this.btnNextPage.Location = new System.Drawing.Point(92, 509);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(75, 23);
            this.btnNextPage.TabIndex = 116;
            this.btnNextPage.Text = "下一页";
            this.btnNextPage.UseVisualStyleBackColor = true;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // labelCurPage
            // 
            this.labelCurPage.AutoSize = true;
            this.labelCurPage.Location = new System.Drawing.Point(730, 480);
            this.labelCurPage.Name = "labelCurPage";
            this.labelCurPage.Size = new System.Drawing.Size(41, 12);
            this.labelCurPage.TabIndex = 117;
            this.labelCurPage.Text = "当前页";
            // 
            // labelTotalRecord
            // 
            this.labelTotalRecord.AutoSize = true;
            this.labelTotalRecord.Location = new System.Drawing.Point(839, 480);
            this.labelTotalRecord.Name = "labelTotalRecord";
            this.labelTotalRecord.Size = new System.Drawing.Size(41, 12);
            this.labelTotalRecord.TabIndex = 118;
            this.labelTotalRecord.Text = "总记录";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(738, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 16);
            this.label1.TabIndex = 119;
            this.label1.Text = "提示：请先进行内部校准";
            // 
            // btncancelcali
            // 
            this.btncancelcali.Location = new System.Drawing.Point(182, 590);
            this.btncancelcali.Name = "btncancelcali";
            this.btncancelcali.Size = new System.Drawing.Size(75, 23);
            this.btncancelcali.TabIndex = 120;
            this.btncancelcali.Text = "取消校准";
            this.btncancelcali.UseVisualStyleBackColor = true;
            this.btncancelcali.Click += new System.EventHandler(this.btncancelcali_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbTypeAll);
            this.groupBox3.Controls.Add(this.rbTypeCur);
            this.groupBox3.Location = new System.Drawing.Point(807, 41);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(120, 51);
            this.groupBox3.TabIndex = 121;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "方式";
            // 
            // rbTypeAll
            // 
            this.rbTypeAll.AutoSize = true;
            this.rbTypeAll.Location = new System.Drawing.Point(15, 20);
            this.rbTypeAll.Name = "rbTypeAll";
            this.rbTypeAll.Size = new System.Drawing.Size(47, 16);
            this.rbTypeAll.TabIndex = 109;
            this.rbTypeAll.TabStop = true;
            this.rbTypeAll.Text = "全部";
            this.rbTypeAll.UseVisualStyleBackColor = true;
            this.rbTypeAll.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // rbTypeCur
            // 
            this.rbTypeCur.AutoSize = true;
            this.rbTypeCur.Location = new System.Drawing.Point(66, 20);
            this.rbTypeCur.Name = "rbTypeCur";
            this.rbTypeCur.Size = new System.Drawing.Size(47, 16);
            this.rbTypeCur.TabIndex = 110;
            this.rbTypeCur.TabStop = true;
            this.rbTypeCur.Text = "当前";
            this.rbTypeCur.UseVisualStyleBackColor = true;
            this.rbTypeCur.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // btnsendcali
            // 
            this.btnsendcali.Location = new System.Drawing.Point(370, 590);
            this.btnsendcali.Name = "btnsendcali";
            this.btnsendcali.Size = new System.Drawing.Size(75, 23);
            this.btnsendcali.TabIndex = 122;
            this.btnsendcali.Text = "下传结果";
            this.btnsendcali.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(467, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 123;
            this.label2.Text = "当前温度:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(593, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 124;
            this.label3.Text = "当前湿度:";
            // 
            // btnanalyze
            // 
            this.btnanalyze.Location = new System.Drawing.Point(282, 2);
            this.btnanalyze.Name = "btnanalyze";
            this.btnanalyze.Size = new System.Drawing.Size(75, 23);
            this.btnanalyze.TabIndex = 125;
            this.btnanalyze.Text = "统计分析";
            this.btnanalyze.UseVisualStyleBackColor = true;
            this.btnanalyze.Click += new System.EventHandler(this.btnanalyze_Click);
            // 
            // progressBarCali
            // 
            this.progressBarCali.Location = new System.Drawing.Point(675, 590);
            this.progressBarCali.Name = "progressBarCali";
            this.progressBarCali.Size = new System.Drawing.Size(252, 23);
            this.progressBarCali.TabIndex = 126;
            // 
            // btnpausecali
            // 
            this.btnpausecali.Location = new System.Drawing.Point(92, 590);
            this.btnpausecali.Name = "btnpausecali";
            this.btnpausecali.Size = new System.Drawing.Size(75, 23);
            this.btnpausecali.TabIndex = 127;
            this.btnpausecali.Text = "暂停校准";
            this.btnpausecali.UseVisualStyleBackColor = true;
            this.btnpausecali.Click += new System.EventHandler(this.btnpausecali_Click);
            // 
            // labelprogress
            // 
            this.labelprogress.AutoSize = true;
            this.labelprogress.Location = new System.Drawing.Point(616, 595);
            this.labelprogress.Name = "labelprogress";
            this.labelprogress.Size = new System.Drawing.Size(53, 12);
            this.labelprogress.TabIndex = 128;
            this.labelprogress.Text = "当前进度";
            // 
            // labelcompleted
            // 
            this.labelcompleted.AutoSize = true;
            this.labelcompleted.Location = new System.Drawing.Point(819, 616);
            this.labelcompleted.Name = "labelcompleted";
            this.labelcompleted.Size = new System.Drawing.Size(59, 12);
            this.labelcompleted.TabIndex = 129;
            this.labelcompleted.Text = "已完成0/0";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 655);
            this.Controls.Add(this.labelcompleted);
            this.Controls.Add(this.labelprogress);
            this.Controls.Add(this.btnpausecali);
            this.Controls.Add(this.progressBarCali);
            this.Controls.Add(this.btnanalyze);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnsendcali);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btncancelcali);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelTotalRecord);
            this.Controls.Add(this.labelCurPage);
            this.Controls.Add(this.btnNextPage);
            this.Controls.Add(this.btnPrePage);
            this.Controls.Add(this.textReceive);
            this.Controls.Add(this.textSend);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(btnDeleteAllPoint);
            this.Controls.Add(this.btnDeletePoint);
            this.Controls.Add(this.btnInsertPoint);
            this.Controls.Add(this.btnAddPoint);
            this.Controls.Add(this.dgvCaliItem);
            this.Controls.Add(this.btnsavecali);
            this.Controls.Add(this.btnstartcali);
            this.Controls.Add(this.btncaliset);
            this.Controls.Add(this.btnuser);
            this.Controls.Add(this.btnresult);
            this.Controls.Add(this.btncomset);
            this.Controls.Add(this.btLoginout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "自动校准平台";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCaliItem)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btLoginout;
        private System.Windows.Forms.Button btncomset;
        private System.Windows.Forms.Button btnresult;
        private System.Windows.Forms.Button btnuser;
        private System.Windows.Forms.Button btncaliset;
        private System.Windows.Forms.Button btnstartcali;
        private System.Windows.Forms.Button btnsavecali;
        private System.Windows.Forms.DataGridView dgvCaliItem;
        private System.Windows.Forms.Button btnAddPoint;
        private System.Windows.Forms.Button btnInsertPoint;
        private System.Windows.Forms.Button btnDeletePoint;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.RadioButton rbVDCL;
        private System.Windows.Forms.RadioButton rbVDCHP;
        private System.Windows.Forms.RadioButton rbVACVL;
        private System.Windows.Forms.RadioButton rbVACVH;
        private System.Windows.Forms.RadioButton rbVACF;
        private System.Windows.Forms.RadioButton rbIDC;
        private System.Windows.Forms.RadioButton rbIACI;
        private System.Windows.Forms.RadioButton rbIACF;
        private System.Windows.Forms.RadioButton rbUnitSmall;
        private System.Windows.Forms.RadioButton rbUnitBig;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textSend;
        private System.Windows.Forms.TextBox textReceive;
        private System.Windows.Forms.Button btnPrePage;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Label labelCurPage;
        private System.Windows.Forms.Label labelTotalRecord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btncancelcali;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbTypeAll;
        private System.Windows.Forms.RadioButton rbTypeCur;
        private System.Windows.Forms.Button btnsendcali;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnanalyze;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Source;
        private System.Windows.Forms.DataGridViewTextBoxColumn StandOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn State;
        private System.Windows.Forms.RadioButton rbVDCHN;
        private System.Windows.Forms.ProgressBar progressBarCali;
        private System.Windows.Forms.Button btnpausecali;
        private System.Windows.Forms.Label labelprogress;
        private System.Windows.Forms.Label labelcompleted;
    }
}

