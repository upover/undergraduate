using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.IO;
using System.Collections;


namespace AutoCalibrationSystem
{
    public partial class MainForm : Form
    {

        public delegate void ProcessDelegate(bool nextpage);
        //进度条
        private delegate void ProgressBarShow(int i);
        Thread progressbarThread = null;
        //新建窗体
        public static CaliItemSetForm caliItemSetForm = new CaliItemSetForm();
        public static ComSetForm comSetForm = new ComSetForm();
        public static ResultManageForm resultManageForm = new ResultManageForm();
        public static DataAnalyzeForm dataAnalyzeForm = new DataAnalyzeForm();

        //串口类
        public SerialPort comTest = new SerialPort();
        public SerialPort comStand = new SerialPort();
        public SerialPort com5520A = new SerialPort();
        public SerialPort com9920A = new SerialPort();
        public SerialPort com9920B = new SerialPort();



        private EnumCaliState caliState = EnumCaliState.INITI;
        //网口通信相关
        public Socket socketStand;
        Thread threadReceive;
        //定义接收服务端发送消息的回调
        private delegate void ReceiveMsgCallBack(string strMsg);
        //声明
        private ReceiveMsgCallBack receiveCallBack;

        //与串口接收相关的变量定义
        private StringBuilder builder = new StringBuilder();
        List<byte> buffer = new List<byte>(4096);
        public List<byte> testBuffer = new List<byte>(4096);
        
        //发送校准命令定时器
        System.Timers.Timer CS9010Timer;
        System.Timers.Timer Agilent34410ATimer;
        System.Timers.Timer Fluke5520ATimer;
        System.Timers.Timer CS9920ATimer;
        System.Timers.Timer CS9920BTimer;
        System.Timers.Timer SocketTimer;
        //发送命令计数
        private int sendcount9010 = 0;
        private int sendcount34410A = 0;
        private int sendcount5520A = 0;
        private int sendcount9920A = 0;
        private int sendcount9920B = 0;
        
        //
        public enum StateCS9920 { STOP, OUT_START, OUT_STABLE, SET_VALUE, SET_TIME};

        public StateCS9920 stateCS9920B = StateCS9920.STOP;
        public StateCS9920 stateCS9920A = StateCS9920.STOP;

        //仪表状态
        InstrumentsState instrumentsState = new InstrumentsState();

        //表格DataGridView相关数据
        GridViewModel gridViewModel = new GridViewModel();
        static CaliData caliData = new CaliData();

        //当前校准状态
        CaliProcess caliProcess = new CaliProcess(EnumMode.IACI,caliData);
        public MainForm()
        {
 
            InitializeComponent();
        }

        //校准项设置
        private void btncaliset_Click(object sender, EventArgs e)
        {
            caliItemSetForm.ShowDialog(this);
            if (caliItemSetForm.DialogResult == DialogResult.OK){
                
            }
        }

        public void IntiCaliItemGridView()
        {
            /*
            for (int i = 0; i < 21; i++){
                CaliItem item = new CaliItem(i,i*100,1,1);
                this.gridViewModel.items.Add(item);
            }
            */
            //电流
            //iac
            //for (int i = 0; i < caliData.iaciData.Count; i++)
            //{
            //    this.gridViewModel.items.Add(caliData.iaciData[i]);
            //}
            ////iacf
            //for (int i = 0; i < caliData.iacfData.Count; i++)
            //{
            //    this.gridViewModel.items.Add(caliData.iacfData[i]);
            //}
            ////idc
            //for (int i = 0; i < caliData.idcData.Count; i++)
            //{
            //    this.gridViewModel.items.Add(caliData.idcData[i]);
            //}
            ////电压
            ////vacf
            //for (int i = 0; i < caliData.vacfData.Count; i++)
            //{
            //    this.gridViewModel.items.Add(caliData.vacfData[i]);
            //}
            ////vacvl
            //for (int i = 0; i < CaliData.VLOWNUM; i++)
            //{
            //    this.gridViewModel.items.Add(caliData.vacvData[i]);
            //}
            ////vdclp
            //for (int i = 0; i < CaliData.VLOWNUM; i++)
            //{
            //    this.gridViewModel.items.Add(caliData.vdcData[i]);
            //}
            ////vdcln
            //for (int i = 0; i < CaliData.VLOWNUM; i++)
            //{
            //    this.gridViewModel.items.Add(caliData.vdcData[i+CaliData.VDCPNUM]);
            //}
            ////vacvh
            //for (int i = CaliData.VLOWNUM; i < caliData.vacvData.Count; i++)
            //{
            //    this.gridViewModel.items.Add(caliData.vacvData[i]);
            //}
            ////vdchp
            //for (int i = CaliData.VLOWNUM; i < CaliData.VDCPNUM; i++)
            //{
            //    this.gridViewModel.items.Add(caliData.vdcData[i]);
            //}
            ////vdchn
            //for (int i = CaliData.VLOWNUM; i < CaliData.VDCPNUM; i++)
            //{
            //    this.gridViewModel.items.Add(caliData.vdcData[i+CaliData.VDCPNUM]);
            //}
            //this.gridViewModel.recordCount = this.gridViewModel.items.Count;
            //this.gridViewModel.pageCount = this.gridViewModel.recordCount % 10 == 0 ? this.gridViewModel.recordCount / 10 : (this.gridViewModel.recordCount / 10 + 1);
            //this.gridViewModel.currentPage = 0;
        }
        private int getPageByCount(int count)
        {
            return (count % 10 == 0) ? count / 10 : count / 10 + 1;
        }
        private void IntiGridView(EnumMode mode) 
        {
            this.gridViewModel.curMode = mode;
            this.gridViewModel.curModePage = 0;
            switch (mode) 
            { 
                case EnumMode.IACI:
                    this.gridViewModel.currentPage = 0;
                    break;
                case EnumMode.IACF:
                    this.gridViewModel.currentPage = getPageByCount(caliData.iaciData.Count) ;
                    break;
                case EnumMode.IDC:
                    this.gridViewModel.currentPage = getPageByCount(caliData.iaciData.Count) + getPageByCount(caliData.iacfData.Count);
                    break;
                case EnumMode.VACF:
                    this.gridViewModel.currentPage = getPageByCount(caliData.iaciData.Count) + getPageByCount(caliData.iacfData.Count) + getPageByCount(caliData.idcData.Count);
                    break;
                case EnumMode.VACVL:
                    this.gridViewModel.currentPage = getPageByCount(caliData.iaciData.Count) + getPageByCount(caliData.iacfData.Count) + getPageByCount(caliData.idcData.Count)
                        + getPageByCount(caliData.vacfData.Count);
                    break;
                case EnumMode.VDCL:
                    this.gridViewModel.currentPage = getPageByCount(caliData.iaciData.Count) + getPageByCount(caliData.iacfData.Count) + getPageByCount(caliData.idcData.Count)
                        + getPageByCount(caliData.vacfData.Count) + getPageByCount(CaliData.VLOWNUM);
                    break;
                case EnumMode.VACVH:
                    this.gridViewModel.currentPage = getPageByCount(caliData.iaciData.Count) + getPageByCount(caliData.iacfData.Count) + getPageByCount(caliData.idcData.Count)
                        + getPageByCount(caliData.vacfData.Count) + getPageByCount(CaliData.VLOWNUM) + getPageByCount(CaliData.VLOWNUM*2);
                    break;
                case EnumMode.VDCHP:
                    this.gridViewModel.currentPage = getPageByCount(caliData.iaciData.Count) + getPageByCount(caliData.iacfData.Count) + getPageByCount(caliData.idcData.Count)
                        + getPageByCount(caliData.vacfData.Count) + getPageByCount(CaliData.VLOWNUM) + getPageByCount(CaliData.VLOWNUM * 2) + getPageByCount(CaliData.VACNUM - CaliData.VLOWNUM);
                    break;
                case EnumMode.VDCHN:
                    this.gridViewModel.currentPage = getPageByCount(caliData.iaciData.Count) + getPageByCount(caliData.iacfData.Count) + getPageByCount(caliData.idcData.Count)
                        + getPageByCount(caliData.vacfData.Count) + getPageByCount(CaliData.VLOWNUM) + getPageByCount(CaliData.VLOWNUM * 2) + getPageByCount(CaliData.VACNUM - CaliData.VLOWNUM)
                        + getPageByCount(CaliData.VDCPNUM-CaliData.VLOWNUM);
                    break;
            }                       
            this.gridViewModel.recordCount = MainForm.caliData.iacfData.Count + MainForm.caliData.iaciData.Count + MainForm.caliData.idcData.Count +
                                             MainForm.caliData.vacfData.Count + MainForm.caliData.vacvData.Count + MainForm.caliData.vdcData.Count;
            this.gridViewModel.pageCount = getPageByCount(caliData.iaciData.Count) + getPageByCount(caliData.iacfData.Count) + getPageByCount(caliData.idcData.Count)
                        + getPageByCount(caliData.vacfData.Count) + getPageByCount(CaliData.VLOWNUM) + getPageByCount(CaliData.VLOWNUM * 2) + getPageByCount(CaliData.VACNUM - CaliData.VLOWNUM)
                        + getPageByCount(CaliData.VDCPNUM - CaliData.VLOWNUM) + getPageByCount(CaliData.VDCPNUM - CaliData.VLOWNUM); 
        }
        //窗体加载初始化函数
        private void MainForm_Load(object sender, EventArgs e)
        {
            SocketTimer = new System.Timers.Timer();

            CS9010Timer = new System.Timers.Timer();
            CS9010Timer.Elapsed += CS9010Timer_Elapsed;
            
            Agilent34410ATimer = new System.Timers.Timer();
            Agilent34410ATimer.Elapsed += Agilent34410ATimer_Elapsed;

            Fluke5520ATimer = new System.Timers.Timer();
            Fluke5520ATimer.Elapsed += Fluke5520ATimer_Elapsed;

            CS9920ATimer = new System.Timers.Timer();
            CS9920ATimer.Elapsed += CS9920ATimer_Elapsed;

            CS9920BTimer = new System.Timers.Timer();
            CS9920BTimer.Elapsed += CS9920BTimer_Elapsed;

            comSetForm.comOpenHandler += comSetForm_ComOpenHandler;
            comSetForm.comCloseHandler += comSetForm_ComCloseHandler;
            comSetForm.lanOpenHandler += comSetForm_LanOpenHandler;
            comSetForm.lanCloseHandler += comSetForm_LanCloseHandler;
            this.rbIACI.Checked = true;
            this.rbTypeCur.Checked = true;
            
            this.rbUnitBig.Checked = true;
            //暂时提供统一的一个单位
            this.rbUnitSmall.Visible = false;
            //改变行的高度; 
            dgvCaliItem.RowTemplate.MinimumHeight = 32;
            dgvCaliItem.AutoGenerateColumns = false;
            //显示列表
            //IntiCaliItemGridView();
            IntiGridView(EnumMode.IACI);
            Load_Page(false);

        }

        //网口关闭事件
        bool comSetForm_LanCloseHandler(object sender, EventArgs e)
        {
            instrumentsState.Agilent34410AState = EnumInstrumentState.INIT;
            //关闭socket
            socketStand.Close();
            return true;
        }
        //网口打开事件
        bool comSetForm_LanOpenHandler(object sender, EventArgs e)
        {
            ComSetForm tempSetForm = (ComSetForm)sender;
            try
            {
                socketStand = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Parse(tempSetForm.TextIPStandValue);
                socketStand.Connect(ip, Convert.ToInt32(tempSetForm.TextPortStandValue));
                //实例化回调
                receiveCallBack = new ReceiveMsgCallBack(SetValue);
                SetValue("连接成功");
                instrumentsState.Agilent34410AState = EnumInstrumentState.REMOTE;
                //开启一个新的线程不停的接收服务器发送消息的线程
                threadReceive = new Thread(ReceiveTimer);
                //设置为后台线程
                threadReceive.IsBackground = true;
                threadReceive.Start();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接服务端出错:" + ex.ToString());
                return false;
            }

        }

         private void SetValue(string strValue)
         {
             this.textReceive.AppendText(strValue + "\r \n");
         }

        //串口关闭事件
        private bool comSetForm_ComCloseHandler(object sender, EventArgs e)
        {
            ComSetForm tempSetForm = (ComSetForm)sender;
            UtilEventArgs util = (UtilEventArgs)e;
            switch (util.Parmater)
            {
                case "comTest":
                    if (comTest.IsOpen)
                    {
                        comTest.Close();
                    }
                    else
                        MessageBox.Show("串口已关闭！");
                    break;
                case "comStand":
                    if (comStand.IsOpen)
                    {
                        comStand.Close();
                    }
                    else
                        MessageBox.Show("串口已关闭！");
                    break;
                case "com5520A":
                    if (com5520A.IsOpen)
                    {
                        com5520A.Close();
                    }
                    else
                        MessageBox.Show("串口已关闭！");
                    break;
                case "com9920A":
                    if (com9920A.IsOpen)
                    {
                        com9920A.Close();
                    }
                    else
                        MessageBox.Show("串口已关闭！");
                    break;
                case "com9920B":
                    if (com9920B.IsOpen)
                    {
                        com9920B.Close();
                    }
                    else
                        MessageBox.Show("串口已关闭！");
                    break;
            }
            return true;
        }



        bool comSetForm_ComOpenHandler(object sender, EventArgs e)
        {
            ComSetForm tempSetForm = (ComSetForm)sender;
            UtilEventArgs util = (UtilEventArgs)e;       
            switch (util.Parmater)
            {
                case "comTest":
                    if (!comTest.IsOpen)
                    {
                        comTest.PortName = tempSetForm.boxComTestValue;
                        comTest.BaudRate = int.Parse(tempSetForm.boxBaudrateTestValue);
                        comTest.DataReceived += comTest_DataReceived;
                        try
                        {
                            comTest.Open();
                        }
                        catch (UnauthorizedAccessException)
                        {
                            MessageBox.Show("串口已占用！");
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("串口已打开！");
                    }
                    break;
                case "comStand":
                    if (!comStand.IsOpen)
                    {
                        comStand.PortName = tempSetForm.boxComStandValue;
                        comStand.BaudRate = int.Parse(tempSetForm.boxBaudrateStandValue);
                        comStand.DataReceived += comStand_DataReceived;
                        try
                        {
                            comStand.Open();
                        }
                        catch (UnauthorizedAccessException)
                        {
                            MessageBox.Show("串口已占用！");
                            return false;
                        }
                        instrumentsState.Agilent34410AState = EnumInstrumentState.INIT;
                    }
                    else
                        MessageBox.Show("串口已打开！");
                    break;
                case "com5520A":
                    if (!com5520A.IsOpen)
                    {
                        com5520A.PortName = tempSetForm.boxCom5520AValue;
                        com5520A.BaudRate = int.Parse(tempSetForm.boxBaudrate5520AValue);
                        com5520A.DataReceived += com5520A_DataReceived;                        
                        try
                        {
                            com5520A.Open();
                        }
                        catch (UnauthorizedAccessException)
                        {
                            MessageBox.Show("串口已占用！");
                            return false;
                        }
                    }
                    else
                        MessageBox.Show("串口已打开！");
                    break;
                case "com9920A":
                    if (!com9920A.IsOpen)
                    {
                        com9920A.PortName = tempSetForm.boxCom9920AValue;
                        com9920A.BaudRate = int.Parse(tempSetForm.boxBaudrate9920AValue);
                        com9920A.DataReceived += com9920A_DataReceived;
                        try
                        {
                            com9920A.Open();
                        }
                        catch (UnauthorizedAccessException)
                        {
                            MessageBox.Show("串口已占用！");
                            return false;
                        }
                    }
                    else
                        MessageBox.Show("串口已打开！");
                    break;
                case "com9920B":
                    if (!com9920B.IsOpen)
                    {
                        com9920B.PortName = tempSetForm.boxCom9920BValue;
                        com9920B.BaudRate = int.Parse(tempSetForm.boxBaudrate9920BValue);
                        com9920B.DataReceived += com9920B_DataReceived;
                        try
                        {
                            com9920B.Open();
                        }
                        catch (UnauthorizedAccessException)
                        {
                            MessageBox.Show("串口已占用！");
                            return false;
                        }
                    }
                    else
                        MessageBox.Show("串口已打开！");
                    break;
            }
            return true;
        }

        //通信设置
        private void btncomset_Click(object sender, EventArgs e)
        {
            comSetForm.ShowDialog(this);
        }
        //结果管理
        private void btnresult_Click(object sender, EventArgs e)
        {
            resultManageForm.ShowDialog(this);
        }
        //统计分析
        private void btnanalyze_Click(object sender, EventArgs e)
        {
            dataAnalyzeForm.ShowDialog(this);
        }
        
        //通过输入框向网口发送消息
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                string strMsg = this.textSend.Text.TrimStart();
                byte[] buffer = new byte[2048];
                buffer = Encoding.Default.GetBytes(strMsg);
                int receive = socketStand.Send(buffer);
            }
            catch (Exception ex)
            {
                MessageBox.Show("发送消息出错:" + ex.Message);
            }
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.textReceive.Text = "";
        }

        private void dgvCaliItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btLoginout_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //加载页面
        private void Load_Page(bool nextpage)
        {
            if (caliProcess.complete == EnumCaliState.START)
            {
                if (caliProcess.curTotalNum % 10 == 0 && nextpage)
                {
                    this.gridViewModel.currentPage++;
                    if (gridViewModel.currentPage >= gridViewModel.pageCount)
                        gridViewModel.currentPage = gridViewModel.pageCount - 1;
                    this.gridViewModel.curModePage++;
                }
            }
            BindingList<CaliItem> tempItems = new BindingList<CaliItem>(); //数据源
            this.gridViewModel.getItemsList(caliData, tempItems);
            this.dgvCaliItem.DataSource = tempItems;  //绑定数据源
            //清除默认选中项，选中项为上次修改项
            this.dgvCaliItem.ClearSelection();
            //判断是否是最后一页，索引是否大于最后一项
//            if (this.gridViewModel.currentPage == this.gridViewModel.pageCount - 1 && this.gridViewModel.selectIndex > i % 10)
//            {
//                this.gridViewModel.selectIndex = i % 10;
//            }
//            this.dgvCaliItem.Rows[this.gridViewModel.selectIndex].Selected = true;
//            this.dgvCaliItem.FirstDisplayedScrollingRowIndex = this.gridViewModel.selectIndex;
            labelCurPage.Text = "当前页："+(this.gridViewModel.currentPage+1).ToString()+"/" + (this.gridViewModel.pageCount).ToString();//当前页
            labelTotalRecord.Text = "总记录：" + this.gridViewModel.recordCount.ToString();//总记录数 
        }
        //更新记录数
        private void refreshRecord()
        {
            this.gridViewModel.recordCount = this.gridViewModel.items.Count;
            this.gridViewModel.pageCount = this.gridViewModel.recordCount % 10 == 0 ? this.gridViewModel.recordCount / 10 : (this.gridViewModel.recordCount / 10 + 1);
        }
        //上一页
        private void btnPrePage_Click(object sender, EventArgs e)
        {
            gridViewModel.currentPage -= 1;
            if (gridViewModel.currentPage < 0)
                gridViewModel.currentPage = 0;
            gridViewModel.curModePage -=1;
            Load_Page(false);
        }
        //下一页
        private void btnNextPage_Click(object sender, EventArgs e)
        {
            gridViewModel.currentPage += 1;
            if (gridViewModel.currentPage >= gridViewModel.pageCount)
                gridViewModel.currentPage = gridViewModel.pageCount - 1;
            gridViewModel.curModePage += 1;
            Load_Page(false);
        }
        //增加校准点
        private void btnAddPoint_Click(object sender, EventArgs e)
        {
            this.gridViewModel.recordCount += 1;
            CaliItem item = new CaliItem("");
            this.gridViewModel.items.Add(item);
            refreshRecord();
            Load_Page(false);
        }
        //删除选中点
        private void btnDeletePoint_Click(object sender, EventArgs e)
        {
            this.gridViewModel.recordCount -= 1;
            if (gridViewModel.recordCount < 0)
            {
                gridViewModel.recordCount = 0;
                return;
            }
            if (this.dgvCaliItem.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选中行然后再删除！");
                return;
            }
            foreach (DataGridViewRow r in this.dgvCaliItem.SelectedRows)
            {
                if (!r.IsNewRow)
                {
                    int rowNum = r.Index+this.gridViewModel.currentPage * GridViewModel.pageSize;
                    this.gridViewModel.items.RemoveAt(rowNum);
                    this.gridViewModel.SetSelectedIndex(r.Index);
                    dgvCaliItem.Rows.Remove(r);

                }
            }
            refreshRecord();
            Load_Page(false);
        }
        //插入校准点
        private void btnInsertPoint_Click(object sender, EventArgs e)
        {
            if (this.dgvCaliItem.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选中一行然后再插入！");
                return;
            }
            foreach (DataGridViewRow r in this.dgvCaliItem.SelectedRows)
            {
                if (!r.IsNewRow)
                {
                    int rowNum = r.Index + this.gridViewModel.currentPage * GridViewModel.pageSize;
                    CaliItem tempItem = new CaliItem(0,0,0);
                    this.gridViewModel.items.Insert(rowNum+1,tempItem);
                    this.gridViewModel.SetSelectedIndex(r.Index + 1);
                }
            }
            refreshRecord();
            Load_Page(false);
        }
        //重置
        private void btnReset_Click(object sender, EventArgs e)
        {

        }
        //根据当前校准选项，变换单位，点数限制等
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            this.rbUnitBig.Checked = true;
            EnumMode mode = EnumMode.IACI;
            switch (rb.Text){
                case "VDC Low":
                    //this.rbUnitSmall.Text = "V";
                    this.rbUnitBig.Text = "kV";
                    mode = EnumMode.VDCL;
                    this.gridViewModel.maxNum = GridViewModel.MAXNUM_V;
                    break;
                case "VDC H Pos":
                    //this.rbUnitSmall.Text = "V";
                    this.rbUnitBig.Text = "kV";
                    this.gridViewModel.maxNum = GridViewModel.MAXNUM_V;
                    mode = EnumMode.VDCHP;
                    break;
                case "VDC H Neg":
                    //this.rbUnitSmall.Text = "V";
                    this.rbUnitBig.Text = "kV";
                    this.gridViewModel.maxNum = GridViewModel.MAXNUM_V;
                    mode = EnumMode.VDCHN;
                    break;
                case "VAC V Low":
                    //this.rbUnitSmall.Text = "V";
                    this.rbUnitBig.Text = "kV";
                    this.gridViewModel.maxNum = GridViewModel.MAXNUM_V;
                    mode = EnumMode.VACVL;
                    break;
                case "VAC V High":
                    //this.rbUnitSmall.Text = "V";
                    this.rbUnitBig.Text = "kV";
                    this.gridViewModel.maxNum = GridViewModel.MAXNUM_V;
                    mode = EnumMode.VACVH;
                    break;
                case "VAC F":
                    this.rbUnitBig.Text = "Hz";
                    this.gridViewModel.maxNum = GridViewModel.MAXNUM_F;
                    mode = EnumMode.VACF;
                    break;
                case "IDC":
                    //this.rbUnitSmall.Text = "uA";
                    this.rbUnitBig.Text = "mA";
                    this.gridViewModel.maxNum = GridViewModel.MAXNUM_I;
                    mode = EnumMode.IDC;
                    break;
                case "IAC I":
                    //this.rbUnitSmall.Text = "uA";
                    this.rbUnitBig.Text = "mA";
                    this.gridViewModel.maxNum = GridViewModel.MAXNUM_I;
                    mode = EnumMode.IACI;
                    break;
                case "IAC F":
                    this.rbUnitBig.Text = "Hz";
                    this.gridViewModel.maxNum = GridViewModel.MAXNUM_F;
                    mode = EnumMode.IACF;
                    break;
            }
            /*
            if (rb.Text == "VAC F" || rb.Text == "IAC F"){
                this.rbUnitBig.Visible = false;
            }
            else {
                this.rbUnitBig.Visible = true ;
            }
            */
            caliProcess = new CaliProcess(mode, caliData);
            IntiGridView(mode); 
            Load_Page(false);
        }
        //开始校准
        private void btnstartcali_Click(object sender, EventArgs e)
        {
            if (caliState == EnumCaliState.INITI)
            {
                //设置定时器定时时间
                Agilent34410ATimer.Interval = 3000;
                CS9010Timer.Interval = 3000;
            }                
            //从取消校准，开始校准
            else if (caliState == EnumCaliState.CANCEL)
            {
                //重置caliData,caliProcess
                caliData.Reset(caliProcess);
                EnumMode mode = caliProcess.initiMode;
                caliProcess = new CaliProcess(mode,caliData);
            }
            //判断是否连接
            if (!comTest.IsOpen)
            {
                MessageBox.Show("待校准表未连接");
                return;
            }
            if ((socketStand == null || !socketStand.Connected) && !comStand.IsOpen)
            {
                MessageBox.Show("标准表未连接");
                return;
            }
            //根据模式判断源是否已连接，打开对应定时器
            switch (caliProcess.curMode) 
            {
                case EnumMode.IACF:
                case EnumMode.IACI:
                case EnumMode.IDC:
                case EnumMode.VACF:
//                case EnumMode.VACVL:
                case EnumMode.VDCL:
                    if (!com5520A.IsOpen)
                    {
                        MessageBox.Show("源Fluke5520A未连接");
                        return;
                    }
                    else
                    {
                        Fluke5520ATimer.Interval = 3000;
                        Fluke5520ATimer.Enabled = true;
                        instrumentsState.Fluke5520AState = EnumInstrumentState.REMOTE;
                    }
                    break;
                case EnumMode.VACVH:
                case EnumMode.VACVL:
                    if (!com9920A.IsOpen)
                    {
                        MessageBox.Show("源CS9920A未连接");
                        return;
                    }
                    else
                    {
                        CS9920ATimer.Interval = 3000;
                        instrumentsState.CS9920AState = EnumInstrumentState.INIT;
                        CS9920ATimer.Enabled = true;
                    }
                    break;
                case EnumMode.VDCHP:
                case EnumMode.VDCHN:
                    if (!com9920B.IsOpen)
                    {
                        MessageBox.Show("源CS9920B未连接");
                        return;
                    }
                    else
                    {
                        CS9920BTimer.Interval = 3000;
                        CS9920BTimer.Enabled = true;
                        instrumentsState.CS9920BState = EnumInstrumentState.INIT;
                    }
                    break;
            }
            Agilent34410ATimer.Enabled = true;
            CS9010Timer.Enabled = true;

            if (caliState != EnumCaliState.PAUSE)
            {
                InitialProgressBar(caliProcess.totalNum);
            }
            instrumentsState.CS9010State = EnumInstrumentState.INIT;
            caliProcess.complete = EnumCaliState.START; 
            caliState = EnumCaliState.START;
 
        }
        //暂停校准
        private void btnpausecali_Click(object sender, EventArgs e)
        {
            //设置暂停标志
            caliState = EnumCaliState.PAUSE;
            //停止各定时函数
            Agilent34410ATimer.Enabled = false;
            CS9010Timer.Enabled = false ;
            //根据模式判断源，关闭对应定时器
            switch (caliProcess.curMode)
            {
                case EnumMode.IACF:
                case EnumMode.IACI:
                case EnumMode.IDC:
                case EnumMode.VACF:
                case EnumMode.VACVL:
                case EnumMode.VDCL:
                        Fluke5520ATimer.Enabled = false;                    
                    break;
                case EnumMode.VACVH:
                        CS9920ATimer.Enabled = false;                    
                    break;
                case EnumMode.VDCHP:
                case EnumMode.VDCHN:
                        CS9920BTimer.Enabled = false;                    
                    break;
            }
        }
        //取消校准
        private void btncancelcali_Click(object sender, EventArgs e)
        {
            //设置取消标志
            caliState = EnumCaliState.CANCEL;
            //停止各定时函数
            Agilent34410ATimer.Enabled = false;
            CS9010Timer.Enabled = false;
            //根据模式判断源，关闭对应定时器
            switch (caliProcess.curMode)
            {
                case EnumMode.IACF:
                case EnumMode.IACI:
                case EnumMode.IDC:
                case EnumMode.VACF:
                case EnumMode.VACVL:
                case EnumMode.VDCL:
                    Fluke5520ATimer.Enabled = false;
                    break;
                case EnumMode.VACVH:
                    CS9920ATimer.Enabled = false;
                    break;
                case EnumMode.VDCHP:
                case EnumMode.VDCHN:
                    CS9920BTimer.Enabled = false;
                    break;
            }
        }
        //进度条控件更新值
        private void ShowProgress(int current)
        {
            //说明，progressbar在主线程中定义，进度条线程访问progressbar时需要使用invoke，invokeRequired属性为true
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ProgressBarShow(ShowProgress),current);
            }
            //progressbar在主线程中定义，主线程直接访问progressbar
            else
            {
                this.progressBarCali.Value = current;
                this.labelcompleted.Text = "已完成:" + current.ToString() +"/"+this.progressBarCali.Maximum;
            }

        }
        //进度条线程更新函数
        private void updateProgress(object end)
        {
            int current = 0;
            ShowProgress(current);
            int max = (int)end;
            while (current < max)
            {
                current = caliProcess.curTotalNum;
                //current += 3;
                ShowProgress(current>max?max:current);
                Thread.Sleep(1000);
            }
            progressbarThread.Abort();
            progressbarThread.Join();
        }
        //开始进度条线程
        private void InitialProgressBar(int end)
        {
            this.progressBarCali.Maximum = end;
            progressbarThread = new Thread(new ParameterizedThreadStart(updateProgress));
            progressbarThread.Start(end);
        }
        //待校准表串口接收函数
        void comTest_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int n = comTest.BytesToRead;
            if (n <= 0)
                return;
            //收到数据，已发送命令数减1
            if ( sendcount9010 > 0)
            {
                sendcount9010--;
            }
            byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据
            string result = "";

            int received_count = 0;

            received_count += n;//增加计数器

            comTest.Read(buf, 0, n);
            result += System.Text.Encoding.Default.GetString(buf);
            Console.WriteLine("CS9010Recieved:" + result);
            //接收结果处理
            if (instrumentsState.CS9010State == EnumInstrumentState.INIT)
            {
                if (result.Equals("Address\n")) {
                    instrumentsState.CS9010State = EnumInstrumentState.SADD;
                    return;
                }
            }
            else if (instrumentsState.CS9010State == EnumInstrumentState.SADD)
            {
                if (result.Equals("Remote\n"))
                {
                    instrumentsState.CS9010State = EnumInstrumentState.REMOTE;
                    return;
                }
                else if (result.Equals("Local"))
                {
                    instrumentsState.CS9010State = EnumInstrumentState.INIT;
                }
                else if (result.Equals("Address")) {
                    instrumentsState.CS9010State = EnumInstrumentState.SADD;
                }
            }
            else if (instrumentsState.CS9010State == EnumInstrumentState.REMOTE)
            {
                //处理查询到的数据
                string[] datas = result.Split(':');
                datas = datas[1].Split('k');
                float data = float.Parse(datas[0]);
                //保存到caliData
                SaveTestToCaliData(data,caliProcess);
            }
        }
        //待校准表CS9010保存测量值
        public void SaveTestToCaliData(float data,CaliProcess thisCaliProcess) {
            switch (thisCaliProcess.curMode) { 
                case EnumMode.IACI:
                    caliData.iaciData[thisCaliProcess.curNum].TestOut = data;
                    break;
                case EnumMode.IACF:
                    caliData.iacfData[thisCaliProcess.curNum].TestOut = data;
                    break;
                case EnumMode.IDC:
                    caliData.idcData[thisCaliProcess.curNum].TestOut = data;
                    break;
                case EnumMode.VACF:
                    caliData.vacfData[thisCaliProcess.curNum].TestOut = data;
                    break;
                case EnumMode.VACVL:
                    if (stateCS9920A != StateCS9920.OUT_STABLE)
                    {
                        return;
                    }
                    caliData.vacvData[thisCaliProcess.curNum].TestOut = data;
                    break;
                case EnumMode.VACVH:
                    if (stateCS9920A != StateCS9920.OUT_STABLE)
                    {
                        return;
                    }
                    caliData.vacvData[CaliData.VLOWNUM + thisCaliProcess.curNum].TestOut = data;
                    break;
                case EnumMode.VDCL:
                    if (thisCaliProcess.curNum < CaliData.VLOWNUM)
                    {
                        caliData.vdcData[thisCaliProcess.curNum].TestOut = data;
                    }
                    else 
                    {
                        caliData.vdcData[CaliData.VDCPNUM + thisCaliProcess.curNum -CaliData.VLOWNUM].TestOut = data;
                    }
                    break;
                case EnumMode.VDCHP:
                    if (stateCS9920B != StateCS9920.OUT_STABLE)
                    {
                        return;
                    }
                    caliData.vdcData[CaliData.VLOWNUM + thisCaliProcess.curNum].TestOut = data;                   
                    break;
                case EnumMode.VDCHN:
                    if (stateCS9920B != StateCS9920.OUT_STABLE)
                    {
                        return;
                    }
                    caliData.vdcData[CaliData.VDCPNUM + CaliData.VLOWNUM + thisCaliProcess.curNum].TestOut = data;
                    break;
            }
            //已读
            if (thisCaliProcess.curNumTest == thisCaliProcess.curNum-1)
            {
                thisCaliProcess.curNumTest++;
            }
        }
        //cs9010定时发送指令函数
        private void CS9010Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (caliState == EnumCaliState.COMPLETE)
            {
                CS9010Timer.Enabled = false;
                return; 
            }
            if (sendcount9010 != 0)
                return;
            string command = "";
            //判断是否已处于远控状态
            if (instrumentsState.CS9010State == EnumInstrumentState.INIT)
            {
                //寻址
                command += CommandCS9010.Communication[(int)CommandCS9010.cmdCommunication.SADD];
                command += comSetForm.numSaddTestValue;
            }
            else if (instrumentsState.CS9010State == EnumInstrumentState.SADD)
            {
                //远控
                command += CommandCS9010.Communication[(int)CommandCS9010.cmdCommunication.REMOTE];               
                //command += "#";
                //comTest.Write(command);
                //远控指令发送后,高压表没有返回，需要询问一次状态
                //instrumentsState.CS9010State = EnumInstrumentState.QUERYREMOTE;
                //return;
            }
            else if (instrumentsState.CS9010State == EnumInstrumentState.QUERYREMOTE)
            {
                //询问是否已远控
                command += CommandCS9010.Communication[(int)CommandCS9010.cmdCommunication.QUERY];
            }
            else {
                //发送查询指令
                //判断是否需要切换模式
                if(caliProcess.changeMode && !caliProcess.changeMode9010){
                    switch(caliProcess.curMode)
                    {
                        case EnumMode.IACI:
                            command += CommandCS9010.Configure[(int)CommandCS9010.cmdConfigure.IAC];
                            break;
                        case EnumMode.IACF:
                            command += CommandCS9010.Configure[(int)CommandCS9010.cmdConfigure.IAC];                            
                            break;
                        case EnumMode.IDC:
                            command += CommandCS9010.Configure[(int)CommandCS9010.cmdConfigure.IDC];                               
                            break;
                        case EnumMode.VACVL:
                        case EnumMode.VACVH:
                        case EnumMode.VACF:
                            command += CommandCS9010.Configure[(int)CommandCS9010.cmdConfigure.VAC];                              
                            break;
                        case EnumMode.VDCL:
                        case EnumMode.VDCHP:
                        case EnumMode.VDCHN:
                            command += CommandCS9010.Configure[(int)CommandCS9010.cmdConfigure.VDC];                              
                            break;
                    }
                    command += "#";
                    comTest.Write(command);
                    Console.WriteLine("发送到cs9010:" + command);
                    caliProcess.changeMode9010 = true;
                    return;
                }
                else
                {
                    //该模式校准完毕
                    if( caliProcess.curNumTest == caliProcess.curModeTotal - 1 )
                    {
                        return;
                    }
                    if (caliProcess.curMode == EnumMode.IACF || caliProcess.curMode == EnumMode.VACF)
                    {
                        command += CommandCS9010.Measure[(int)CommandCS9010.cmdMeasure.FREQ];
                    }
                    else {
                        command += CommandCS9010.Measure[(int)CommandCS9010.cmdMeasure.VALUE];
                    }
                }

            }
            command += "#";//结束符
            //已发送命令数加1
            sendcount9010++;
            comTest.Write(command);
            Console.WriteLine("发送到CS9010:"+command);
        }
        //标准表串口接收函数
        private void comStand_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int n = comStand.BytesToRead;
                if (n <= 0)
                    return;
                byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据
                string result = "";

                int received_count = 0;

                received_count += n;//增加计数器

                comStand.Read(buf, 0, n);
                result += System.Text.Encoding.Default.GetString(buf);
                Console.WriteLine("Recieved:" + result);
                String[] strs;
                //对收到的字符进行转换
                strs = result.Split('E');
                int sign = strs[0].StartsWith("+") == true ? 1 : -1;
                float digit = float.Parse(strs[0]);
                int exponent = int.Parse(strs[1]);
                float data = digit * (float)Math.Pow(10, exponent);
                SaveStandToCaliData(data, caliProcess);
                if (sendcount34410A > 0)
                {
                    sendcount34410A--;
                }
                this.textReceive.Invoke(receiveCallBack, "接收34410A:"+ result);             
            }
            catch (Exception ex)
            {
                MessageBox.Show("接收服务端发送的消息出错:" + ex.ToString());
            }
        }
        //34410A网口socket的定时器
        private void ReceiveTimer() 
        {
            SocketTimer.Elapsed += ReceiveHandler;    //定时事件的方法
            SocketTimer.Interval = 1000;
            SocketTimer.Enabled = true;
        }
        //接到Agilent34410A返回的消息
        private void ReceiveHandler(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                byte[] buffer = new byte[100];
                int len = 0;
                string str = "";
                String[] strs;
                if (socketStand.Available > 0)
                {
                    //实际接收到的字节数
                    len = socketStand.Receive(buffer, SocketFlags.None);
                }
                if (len != 0)              
                {
                    str = Encoding.Default.GetString(buffer, 0, len);
                    //对收到的字符进行转换
                    strs = str.Split('E');
                    int sign = strs[0].StartsWith("+") == true ? 1 : -1;
                    float digit = float.Parse(strs[0]);
                    int exponent = int.Parse(strs[1]);
                    float data = digit * (float)Math.Pow(10, exponent)/1000;
                    SaveStandToCaliData(data, caliProcess);
                    if (sendcount34410A > 0)
                    {
                        sendcount34410A--;
                    }
                    this.textReceive.Invoke(receiveCallBack, "接收远程服务器:" + socketStand.RemoteEndPoint + "发送的消息:" + str);
                    Console.WriteLine("接收到34410A;" + str);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("接收服务端发送的消息出错:" + ex.ToString());
            }
        }
        //标准表34410A保存测量值
        public void SaveStandToCaliData(float data,CaliProcess thisCaliProcess) 
        {
            switch (thisCaliProcess.curMode)
            {
                case EnumMode.IACI:
                    caliData.iaciData[thisCaliProcess.curNum].StandOut = data;
                    break;
                case EnumMode.IACF:
                    caliData.iacfData[thisCaliProcess.curNum].StandOut = data;
                    break;
                case EnumMode.IDC:
                    caliData.idcData[thisCaliProcess.curNum].StandOut = data;
                    break;
                case EnumMode.VACF:
                    caliData.vacfData[thisCaliProcess.curNum].StandOut = data;
                    break;
                case EnumMode.VACVL:
                    if (stateCS9920A != StateCS9920.OUT_STABLE)
                    {
                        return;
                    }
                    caliData.vacvData[thisCaliProcess.curNum].StandOut = data;
                    break;
                case EnumMode.VACVH:
                    if (stateCS9920A != StateCS9920.OUT_STABLE)
                    {
                        return;
                    }
                    caliData.vacvData[CaliData.VLOWNUM + thisCaliProcess.curNum].StandOut = data;
                    break;
                case EnumMode.VDCL:
                    if (thisCaliProcess.curNum < CaliData.VLOWNUM)
                    {
                        caliData.vdcData[thisCaliProcess.curNum].StandOut = data;
                    }
                    else
                    {
                        caliData.vdcData[CaliData.VDCPNUM + thisCaliProcess.curNum - CaliData.VLOWNUM].StandOut = data;
                    }
                    break;
                case EnumMode.VDCHP:
                    if (stateCS9920B != StateCS9920.OUT_STABLE)
                    {
                        return;
                    }
                    caliData.vdcData[CaliData.VLOWNUM + thisCaliProcess.curNum].StandOut = data;
                    break;
                case EnumMode.VDCHN:
                    if (stateCS9920B != StateCS9920.OUT_STABLE)
                    {
                        return;
                    }
                    caliData.vdcData[CaliData.VDCPNUM + CaliData.VLOWNUM + thisCaliProcess.curNum].StandOut = data;
                    break;
            }
            //已读
            if (thisCaliProcess.curNumStand == thisCaliProcess.curNum-1)
            {
                thisCaliProcess.curNumStand++;
            }
        }
        //34410A发送消息函数
        private void StandSendMessage(string command)
        {
            try
            {
                if (!comStand.IsOpen)
                {
                    byte[] buffer = new byte[100];
                    buffer = Encoding.Default.GetBytes(command);
                    int receive = socketStand.Send(buffer);
                }
                else
                {
                    //用串口
                    comStand.Write(command);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("发送消息出错:" + ex.Message);
            }
        }
        //34410A定时函数
        private void Agilent34410ATimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (caliState == EnumCaliState.COMPLETE)
            {
                Agilent34410ATimer.Enabled = false;
                return;
            }
            if (sendcount34410A != 0)
                return;
            string command = "";
            //判断是否已处于远控状态
            if (instrumentsState.Agilent34410AState != EnumInstrumentState.REMOTE)
            {
                //MessageBox.Show("Agilent34410A连接未成功");
                command += CommandAgilent34410A.System[(int)CommandAgilent34410A.cmdSystem.Remote]; 
            }
            else
            {                
                //根据校准模式发送查询指令                
                switch (caliProcess.curMode)
                {
                    case EnumMode.IACI:
                        command += CommandAgilent34410A.Measure[(int)CommandAgilent34410A.cmdMeasure.IAC];
                        break;
                    case EnumMode.IACF:
                    case EnumMode.VACF:
                        command += CommandAgilent34410A.Measure[(int)CommandAgilent34410A.cmdMeasure.FREQ];
                        break;
                    case EnumMode.IDC:
                        command += CommandAgilent34410A.Measure[(int)CommandAgilent34410A.cmdMeasure.IDC];
                        break;
                    case EnumMode.VACVL:
                    case EnumMode.VACVH:
                        command += CommandAgilent34410A.Measure[(int)CommandAgilent34410A.cmdMeasure.VAC];
                        break;
                    case EnumMode.VDCL:
                    case EnumMode.VDCHP:
                    case EnumMode.VDCHN:
                        command += CommandAgilent34410A.Measure[(int)CommandAgilent34410A.cmdMeasure.VDC];
                        break;
                }
                if (caliProcess.curNumStand == caliProcess.curModeTotal - 1)
                {
                    return;
                }
                sendcount34410A++;
            }
            StandSendMessage(command+"\n");

            Console.WriteLine("发送到34410A:" + command);
        }
        //5520A定时函数
        private void Fluke5520ATimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (caliState == EnumCaliState.COMPLETE)
            {
                Fluke5520ATimer.Enabled = false;
                return;
            }
            ProcessDelegate showProcess = new ProcessDelegate(Load_Page);
            string command="";
            //判断是否已远控
            if (instrumentsState.Fluke5520AState == EnumInstrumentState.LOCAL)
            {
                command += "REMOTE\r\n";
                instrumentsState.Fluke5520AState = EnumInstrumentState.REMOTE;
            }
            else
            {
                //源和表都已读完数据
                if (caliProcess.curNumTest != -1 && caliProcess.curNumTest == caliProcess.curNumStand)
                {
                    if (caliProcess.curNum == caliProcess.curNumTest)
                    {
                        //校准点更新为已校准
                        this.gridViewModel.updateItem(caliData, caliProcess);
                        //刷新列表
                        dgvCaliItem.Invoke(showProcess, false);
                        caliProcess.curTotalNum++;
                        caliProcess.curNum++;
                        //是否要切换模式
                        caliProcess.getCurMode(caliData);
                        dgvCaliItem.Invoke(showProcess, true);
                        sendcount5520A--;
                    }
                }
                if (sendcount5520A == 0)
                {
                    command += this.caliProcess.getFlukeSourceString(caliData);
                    com5520A.Write(command);
                    Console.WriteLine("发送到5520A:" + command);
                    sendcount5520A++;
                }
            }

        }
        //Fluke5520A的串口接收函数        
        private void com5520A_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

        }
        //CS9920B定时发送函数
        private void CS9920BTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (caliState == EnumCaliState.COMPLETE)
            {
                CS9920BTimer.Enabled = false;
                return;
            }
            ProcessDelegate showProcess = new ProcessDelegate(Load_Page);            
            if (sendcount9920B != 0)
                return;
            string command = "";
            //判断是否已处于远控状态
            if (instrumentsState.CS9920BState == EnumInstrumentState.INIT)
            {
                //寻址
                command += CommandCS9920.Communication[(int)CommandCS9920.cmdCommunication.SADD];
                command += comSetForm.numSadd9920BValue;
            }
            else if (instrumentsState.CS9920BState == EnumInstrumentState.SADD)
            {
                //远控
                command += CommandCS9920.Communication[(int)CommandCS9920.cmdCommunication.REMOTE];                
            }
            //已是远控状态，开始校准，设置电压并输出
            else
            {
                //源和表都已读完数据
                if (caliProcess.curNumTest != -1 && caliProcess.curNumTest == caliProcess.curNumStand)
                {
                    if (caliProcess.curNum == caliProcess.curNumTest)
                    {
                        if(stateCS9920B == StateCS9920.OUT_STABLE)
                        {
                            //校准点更新为已校准
                            this.gridViewModel.updateItem(caliData, caliProcess);
                            //刷新列表
                            dgvCaliItem.Invoke(showProcess, false);
                            caliProcess.curTotalNum++;
                            caliProcess.curNum++;
                            //是否要切换模式
                            caliProcess.getCurMode(caliData);
                            dgvCaliItem.Invoke(showProcess, true);
                            //停止源输出
                            command += CommandCS9920.SourceControl[(int)CommandCS9920.cmdSourceControl.STOP];
                            stateCS9920B = StateCS9920.STOP;
                            command += "#";//结束符
                            com9920B.Write(command);
                            //已发送命令数加1
                            sendcount9920B++;
                            Console.WriteLine("发送到CS9920B:" + command);
                            return;
                        }
                    }
                }
                //先设置电压
                if (stateCS9920B == StateCS9920.STOP)
                {
                    command += CommandCS9920.SetVoltage[(int)CommandCS9920.cmdSetVoltage.DC];
                    command += this.caliProcess.getCS9920SourceString(caliData, "CS9920B");
                    stateCS9920B = StateCS9920.SET_VALUE;
                }
                //再开始输出电压
                else if (stateCS9920B == StateCS9920.SET_VALUE)
                {
                    command += CommandCS9920.SourceControl[(int)CommandCS9920.cmdSourceControl.STAR];
                    stateCS9920B = StateCS9920.OUT_START;
                }
                else if (stateCS9920B == StateCS9920.OUT_START)
                {
                    stateCS9920B = StateCS9920.OUT_STABLE;
                }
            }
            if (command.Equals(""))
                return;
            command += "#";//结束符
            com9920B.Write(command);
            //已发送命令数加1
            sendcount9920B++;
            Console.WriteLine("发送到CS9920B:" + command);
        }
        //CS9920B的串口接收函数
        private void com9920B_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int n = com9920B.BytesToRead;
            if (n <= 0)
                return;
            //收到数据，已发送命令数减1
            if (sendcount9920B > 0)
            {
                sendcount9920B--;
            }
            byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据
            string result = "";
            //testTotalResult = null;

            int received_count = 0;

            received_count += n;//增加计数器

            com9920B.Read(buf, 0, n);
            result += System.Text.Encoding.Default.GetString(buf);
            Console.WriteLine("Recieved:" + result);
            //接收结果处理
            if (instrumentsState.CS9920BState == EnumInstrumentState.INIT)
            {
                if (result.Equals("+0,\"No error\""))
                {
                    instrumentsState.CS9920BState = EnumInstrumentState.SADD;
                }
                else
                {
                    Console.WriteLine("CS9920B: sadd error");
                }
            }
            else if (instrumentsState.CS9920BState == EnumInstrumentState.SADD)
            {
                if (result.Equals("+0,\"No error\""))
                {
                    instrumentsState.CS9920BState = EnumInstrumentState.REMOTE;                 
                }
                else
                {
                    Console.WriteLine("CS9920B: remote error");
                }
            }
            else if (instrumentsState.CS9920BState == EnumInstrumentState.REMOTE)
            {
                if (result.Equals("+0,\"No error\""))
                {

                }
                else
                {
                    Console.WriteLine("CS9920B: calibration error"); 
                }
            }
        }
        //CS9920A定时发送函数
        private void CS9920ATimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (caliState == EnumCaliState.COMPLETE)
            {
                CS9920ATimer.Enabled = false;
                return;
            }
            ProcessDelegate showProcess = new ProcessDelegate(Load_Page);

            if (sendcount9920A != 0)
                return;
            string command = "";
            //判断是否已处于远控状态
            if (instrumentsState.CS9920AState == EnumInstrumentState.INIT)
            {
                //寻址
                command += CommandCS9920.Communication[(int)CommandCS9920.cmdCommunication.SADD];
                command += comSetForm.numSadd9920AValue;
            }
            else if (instrumentsState.CS9920AState == EnumInstrumentState.SADD)
            {
                //远控
                command += CommandCS9920.Communication[(int)CommandCS9920.cmdCommunication.REMOTE];
            }
            //已是远控状态，开始校准，设置电压并输出
            else
            {
                //源和表都已读完数据
                if (caliProcess.curNumTest != -1 && caliProcess.curNumTest == caliProcess.curNumStand)
                {
                    if (caliProcess.curNum == caliProcess.curNumTest)
                    {
                        if (stateCS9920A == StateCS9920.OUT_STABLE)
                        {
                            //校准点更新为已校准
                            this.gridViewModel.updateItem(caliData, caliProcess);
                            //刷新列表
                            dgvCaliItem.Invoke(showProcess, false);
                            caliProcess.curTotalNum++;
                            caliProcess.curNum++;
                            //是否要切换模式
                            caliProcess.getCurMode(caliData);
                            dgvCaliItem.Invoke(showProcess, true);
                            //停止源输出
                            command += CommandCS9920.SourceControl[(int)CommandCS9920.cmdSourceControl.STOP];
                            stateCS9920A = StateCS9920.STOP;
                            command += "#";//结束符
                            com9920A.Write(command);
                            //已发送命令数加1
                            sendcount9920A++;
                            Console.WriteLine("发送到CS9920A:" + command);
                            if (caliProcess.complete == EnumCaliState.COMPLETE)
                                caliState = EnumCaliState.COMPLETE;
                            return;
                        }
                    }
                }
                //先设置电压
                if (stateCS9920A == StateCS9920.STOP)
                {
                    command += CommandCS9920.SetVoltage[(int)CommandCS9920.cmdSetVoltage.AC];
                    command += this.caliProcess.getCS9920SourceString(caliData, "CS9920A");
                    stateCS9920A = StateCS9920.SET_VALUE;
                }
                //再设置时间
                else if (stateCS9920A == StateCS9920.SET_VALUE)
                {
                    command += CommandCS9920.SetTime[(int)CommandCS9920.cmdSetTime.AC];
                    stateCS9920A = StateCS9920.SET_TIME;
                }
                //再开始输出电压
                else if (stateCS9920A == StateCS9920.SET_TIME)
                {
                    command += CommandCS9920.SourceControl[(int)CommandCS9920.cmdSourceControl.STAR];
                    stateCS9920A = StateCS9920.OUT_START;
                }
                else if (stateCS9920A == StateCS9920.OUT_START) {
                    stateCS9920A = StateCS9920.OUT_STABLE;
                }
            }
            if (command.Equals(""))
                return;
            command += "#";//结束符
            com9920A.Write(command);
            //已发送命令数加1
            sendcount9920A++;
            Console.WriteLine("发送到CS9920A:" + command);
        }
        //CS9920A的串口接收函数
        private void com9920A_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int n = com9920A.BytesToRead;
            if (n <= 0)
                return;
            //收到数据，已发送命令数减1
            if (sendcount9920A > 0)
            {
                sendcount9920A--;
            }
            byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据
            string result = "";
            //testTotalResult = null;

            int received_count = 0;

            received_count += n;//增加计数器

            com9920A.Read(buf, 0, n);
            result += System.Text.Encoding.Default.GetString(buf);
            Console.WriteLine("CS9020ARecieved:" + result);
            //接收结果处理
            if (instrumentsState.CS9920AState == EnumInstrumentState.INIT)
            {
                if (result.Equals("+0 No error#"))
                {
                    instrumentsState.CS9920AState = EnumInstrumentState.SADD;
                }
                else
                {
                    Console.WriteLine("CS9920A: sadd error");
                }
            }
            else if (instrumentsState.CS9920AState == EnumInstrumentState.SADD)
            {
                if (result.Equals("+0 No error#"))
                {
                    instrumentsState.CS9920AState = EnumInstrumentState.REMOTE;
                }
                else
                {
                    Console.WriteLine("CS9920A: remote error");
                }
            }
            else if (instrumentsState.CS9920AState == EnumInstrumentState.REMOTE)
            {
                if (result.Equals("+0 No error#"))
                {

                }
                else
                {
                    Console.WriteLine("CS9920A: calibration error");
                }
            }
        }
        private void rbType_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            switch (rb.Text)
            {
                case "全部":
                    this.caliProcess.type = true;
                    break;
                case "当前":
                    this.caliProcess.type = false;
                    break;
            }
            caliProcess.setTotalNumByType(caliData);
        }
    }
}
