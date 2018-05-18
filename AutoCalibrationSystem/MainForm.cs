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
using AutoCalibrationSystem.DataAccess;

namespace AutoCalibrationSystem
{
    public partial class MainForm : Form
    {
        public const int INTERNAL = 1000;
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
        public SerialPort comStand2 = new SerialPort();
        public SerialPort com5520A = new SerialPort();
        public SerialPort com9920A = new SerialPort();
        public SerialPort com9920B = new SerialPort();
        public SerialPort comTandH = new SerialPort();
        public SerialPort comRelay = new SerialPort();

        private EnumCaliState caliState = EnumCaliState.INITI;
        private EnumCaliState dividerState = EnumCaliState.INITI;
        //网口通信相关
        public Socket socketStand;
        Thread threadReceive;

        public Socket socketStand2;
        Thread threadReceive2;

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
        System.Timers.Timer Agilent34410ATimer_2;
        System.Timers.Timer Fluke5520ATimer;
        System.Timers.Timer CS9920ATimer;
        System.Timers.Timer CS9920BTimer;
        System.Timers.Timer SocketTimer;
        System.Timers.Timer SocketTimer_2;
        System.Timers.Timer TandHTimer;

        //发送命令计数
        private int sendcount9010 = 0;
        private int sendcount34410A = 0;
        private int sendcount34410A_2 = 0;
        private int sendcount9920A = 0;
        private int sendcount9920B = 0;
        private int sendcountTandH = 0;
        //源的状态
        public enum StateSource { STOP, OUT_START, OUT_STABLE, SET_VALUE, SET_TIME};

        public StateSource stateCS9920B = StateSource.STOP;
        public StateSource stateCS9920A = StateSource.STOP;
        public StateSource stateFluke5520A = StateSource.STOP;
        //仪表状态
        InstrumentsState instrumentsState = new InstrumentsState();

        //校准表格DataGridView相关数据
        GridViewModel gridViewModel = new GridViewModel();
        static CaliData caliData = new CaliData();

        //当前校准状态
        CaliProcess caliProcess = new CaliProcess(EnumMode.IACI, caliData);

        //分压器表格相关数据
        DividerGridViewModel dividerGridViewModel = new DividerGridViewModel();
        static DividerData dividerData = new DividerData();

        //当前分压器测量状态
        DividerProcess dividerProcess = new DividerProcess(EnumMode.Divider_V_DCP, dividerData);

        //当前测量选项是校准还是分压器监测
        public enum MainMeasureType { CALIBRATION, DIVIDER };
        MainMeasureType mainMeasureType = MainMeasureType.CALIBRATION;
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
        private void InitGridViewByMode(EnumMode mode) 
        {
            this.gridViewModel.curMode = mode;
            this.gridViewModel.curModePage = 0;
            switch (mode)
            {
                case EnumMode.IACI:
                    this.gridViewModel.currentPage = 0;
                    break;
                case EnumMode.IACF:
                    this.gridViewModel.currentPage = getPageByCount(caliData.iaciData.Count);
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
                        + getPageByCount(caliData.vacfData.Count) + getPageByCount(CaliData.VLOWNUM) + getPageByCount(CaliData.VLOWNUM * 2);
                    break;
                case EnumMode.VDCHP:
                    this.gridViewModel.currentPage = getPageByCount(caliData.iaciData.Count) + getPageByCount(caliData.iacfData.Count) + getPageByCount(caliData.idcData.Count)
                        + getPageByCount(caliData.vacfData.Count) + getPageByCount(CaliData.VLOWNUM) + getPageByCount(CaliData.VLOWNUM * 2) + getPageByCount(CaliData.VACNUM - CaliData.VLOWNUM);
                    break;
                case EnumMode.VDCHN:
                    this.gridViewModel.currentPage = getPageByCount(caliData.iaciData.Count) + getPageByCount(caliData.iacfData.Count) + getPageByCount(caliData.idcData.Count)
                        + getPageByCount(caliData.vacfData.Count) + getPageByCount(CaliData.VLOWNUM) + getPageByCount(CaliData.VLOWNUM * 2) + getPageByCount(CaliData.VACNUM - CaliData.VLOWNUM)
                        + getPageByCount(CaliData.VDCPNUM - CaliData.VLOWNUM);
                    break;
            }        
        }
        private void InitGridView() 
        {                       
            this.gridViewModel.recordCount = caliData.iacfData.Count + caliData.iaciData.Count + caliData.idcData.Count +
                                             caliData.vacfData.Count + caliData.vacvData.Count + caliData.vdcData.Count;
            this.gridViewModel.pageCount = getPageByCount(caliData.iaciData.Count) + getPageByCount(caliData.iacfData.Count) + getPageByCount(caliData.idcData.Count)
                        + getPageByCount(caliData.vacfData.Count) + getPageByCount(CaliData.VLOWNUM) + getPageByCount(CaliData.VLOWNUM * 2) + getPageByCount(CaliData.VACNUM - CaliData.VLOWNUM)
                        + getPageByCount(CaliData.VDCPNUM - CaliData.VLOWNUM) + getPageByCount(CaliData.VDCPNUM - CaliData.VLOWNUM);
            this.dividerGridViewModel.recordCount = dividerData.voltageDCPData.Count + dividerData.voltageDCNData.Count +
                                                    dividerData.voltageACData.Count + dividerData.frequencyData.Count;
            this.dividerGridViewModel.pageCount = getPageByCount(dividerData.voltageDCPData.Count) + getPageByCount(dividerData.voltageDCNData.Count) +
                                                  getPageByCount(dividerData.voltageACData.Count) + getPageByCount(dividerData.frequencyData.Count);
        }
        private void InitDividerGridViewByMode(EnumMode mode)
        {
            this.dividerGridViewModel.curMode = mode;
            this.dividerGridViewModel.curModePage = 0;
            switch (mode) { 
                case EnumMode.Divider_V_DCP:
                    this.dividerGridViewModel.currentPage = 0;
                    break;
                case EnumMode.Divider_V_DCN:
                    this.dividerGridViewModel.currentPage = getPageByCount(dividerData.voltageDCPData.Count);
                    break;
                case EnumMode.Divider_V_AC:
                    this.dividerGridViewModel.currentPage = getPageByCount(dividerData.voltageDCPData.Count) + getPageByCount(dividerData.voltageDCNData.Count);
                    break;
                case EnumMode.Divider_F:
                    this.dividerGridViewModel.currentPage = getPageByCount(dividerData.voltageDCPData.Count) + getPageByCount(dividerData.voltageDCNData.Count)+
                                                            getPageByCount(dividerData.voltageACData.Count);
                    break;
            }
        }
        //窗体加载初始化函数
        private void MainForm_Load(object sender, EventArgs e)
        {
            SocketTimer = new System.Timers.Timer();
            SocketTimer_2 = new System.Timers.Timer();
 
            CS9010Timer = new System.Timers.Timer();
            CS9010Timer.Elapsed += CS9010Timer_Elapsed;
            
            Agilent34410ATimer = new System.Timers.Timer();
            Agilent34410ATimer.Elapsed += Agilent34410ATimer_Elapsed;

            Agilent34410ATimer_2 = new System.Timers.Timer();
            Agilent34410ATimer_2.Elapsed += Agilent34410ATimer_2_Elapsed_Divider;

            Fluke5520ATimer = new System.Timers.Timer();
            Fluke5520ATimer.Elapsed += Fluke5520ATimer_Elapsed;

            CS9920ATimer = new System.Timers.Timer();
            CS9920ATimer.Elapsed += CS9920ATimer_Elapsed;

            CS9920BTimer = new System.Timers.Timer();
            CS9920BTimer.Elapsed += CS9920BTimer_Elapsed;

            TandHTimer = new System.Timers.Timer();
            TandHTimer.Elapsed += TandHTimer_Elapsed; 
           
            comSetForm.comOpenHandler += comSetForm_ComOpenHandler;
            comSetForm.comCloseHandler += comSetForm_ComCloseHandler;
            comSetForm.lanOpenHandler += comSetForm_LanOpenHandler;
            comSetForm.lanCloseHandler += comSetForm_LanCloseHandler;


            //默认先选iaci模式
            this.rbIACI.Checked = true;
            this.rbvdcp.Checked = true;

            //默认校准方式为当前
            this.rbTypeCur.Checked = true;            

            //暂时提供统一的一个单位
            this.rbUnitBig.Checked = true;
            this.rbBigUtilDivider.Checked = true;

            this.rbUnitSmall.Visible = false;
            this.rbSmallUtilDivider.Visible = false;
            //改变行的高度; 
            dgvCaliItem.RowTemplate.MinimumHeight = 32;
            dgvCaliItem.AutoGenerateColumns = false;
            
            dgvDivider.RowTemplate.MinimumHeight = 32;
            dgvDivider.AutoGenerateColumns = false;

            //分压器监测默认设置
            this.rbCycleNo.Checked = true;
            this.rbMeasTypeCur.Checked = true;
            this.rbSaveMan.Checked = true;
            this.rbvdcp.Checked = true;

            //显示列表
            //IntiCaliItemGridView();
            InitGridView();
            InitGridViewByMode(EnumMode.IACI);

            InitDividerGridViewByMode(EnumMode.Divider_V_DCP);
            Load_Page(false);
            Load_Page_Divider(false);

            //添加标签切换函数
            this.dmTabCtrl.SelectedIndexChanged += dmTabCtrl_SelectedIndexChanged;
        }

        //网口关闭事件
        bool comSetForm_LanCloseHandler(object sender, EventArgs e)
        {
            ComSetForm tempSetForm = (ComSetForm)sender;
            UtilEventArgs util = (UtilEventArgs)e;
            try
            {
                switch (util.Parmater)
                {
                    case "lanStand1":
                        instrumentsState.Agilent34410AState = EnumInstrumentState.INIT;
                        SocketTimer.Enabled = false;
                        //关闭socket
                        socketStand.Close();
                        break;
                    case "lanStand2":
                        instrumentsState.Agilent34410AState_2 = EnumInstrumentState.INIT;
                        SocketTimer_2.Enabled = false;
                        //关闭socket
                        socketStand2.Close();
                        break;
                }
                return true;
            }
            catch (Exception ex) {
                MessageBox.Show("关闭网口出错:" + ex.ToString());
                return false;
            }
        }
        //网口打开事件
        bool comSetForm_LanOpenHandler(object sender, EventArgs e)
        {
            ComSetForm tempSetForm = (ComSetForm)sender;
            UtilEventArgs util = (UtilEventArgs)e;
            try
            {
                socketStand = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = null; 
                switch (util.Parmater)
                {
                    case "lanStand1":
                        ip = IPAddress.Parse(tempSetForm.TextIPStandValue);
                        socketStand.Connect(ip, Convert.ToInt32(tempSetForm.TextPortStandValue));
                        Console.WriteLine("Stand1连接成功");
                        instrumentsState.Agilent34410AState = EnumInstrumentState.REMOTE;
                        //开启一个新的线程不停的接收服务器发送消息的线程
                        threadReceive = new Thread(ReceiveTimer);
                        //设置为后台线程
                        threadReceive.IsBackground = true;
                        threadReceive.Start();
                        break;
                    case "lanStand2":
                        ip = IPAddress.Parse(tempSetForm.TextIPStand2Value);
                        socketStand2.Connect(ip, Convert.ToInt32(tempSetForm.TextPortStand2Value));
                        Console.WriteLine("Stand2连接成功");
                        instrumentsState.Agilent34410AState_2 = EnumInstrumentState.REMOTE;
                        //开启一个新的线程不停的接收服务器发送消息的线程
                        threadReceive2 = new Thread(ReceiveTimer_2);
                        //设置为后台线程
                        threadReceive2.IsBackground = true;
                        threadReceive2.Start();
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接服务端出错:" + ex.ToString());
                return false;
            }

        }
        //串口关闭事件
        private bool comSetForm_ComCloseHandler(object sender, EventArgs e)
        {
            ComSetForm tempSetForm = (ComSetForm)sender;
            UtilEventArgs util = (UtilEventArgs)e;
            try
            {
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
                    case "comStand2":
                        if (comStand2.IsOpen)
                        {
                            comStand2.Close();
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
                    case "comTandH":
                        if (comTandH.IsOpen)
                        {
                            comTandH.Close();
                        }
                        else
                            MessageBox.Show("串口已关闭！");
                        break;
                    case "comRelay":
                        if (comRelay.IsOpen)
                        {
                            comRelay.Close();
                        }
                        else
                            MessageBox.Show("串口已关闭！");
                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("串口关闭异常");
                return false;
            }
            return true;
        }
        //串口打开事件
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
                case "comStand2":
                    if (!comStand2.IsOpen)
                    {
                        comStand2.PortName = tempSetForm.boxComStand2Value;
                        comStand2.BaudRate = int.Parse(tempSetForm.boxBaudrateStand2Value);
                        comStand2.DtrEnable = Enabled;
                        comStand2.DataReceived += comStand2_DataReceived;
                        try
                        {
                            comStand2.Open();
                        }
                        catch (UnauthorizedAccessException)
                        {
                            MessageBox.Show("串口已占用！");
                            return false;
                        }
                        instrumentsState.Agilent34410AState_2 = EnumInstrumentState.INIT;
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
                case "comTandH":
                    if (!comTandH.IsOpen)
                    {
                        comTandH.PortName = tempSetForm.boxComTandHValue;
                        comTandH.BaudRate = int.Parse(tempSetForm.boxBaudrateTandHValue);
                        comTandH.DataReceived += comTandH_DataReceived;
                        try
                        {
                            comTandH.Open();
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
                case "comRelay":
                    if (!comRelay.IsOpen)
                    {
                        comRelay.PortName = tempSetForm.boxComRelayValue;
                        comRelay.BaudRate = int.Parse(tempSetForm.boxBaudrateRelayValue);
                        comRelay.DataReceived += comRelay_DataReceived;
                        try
                        {
                            comRelay.Open();
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

        //通信设置窗体
        private void btncomset_Click(object sender, EventArgs e)
        {
            comSetForm.ShowDialog(this);
        }
        //结果管理窗体
        private void btnresult_Click(object sender, EventArgs e)
        {
            resultManageForm.ShowDialog(this);
        }
        //统计分析窗体
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
            //在开始校准后，才判断是否需要自动翻页
            if (caliProcess.complete == EnumCaliState.START)
            {
                //nextpage用于标记是刷新还是翻页，使页面最后一条记录先更新后再翻页
                if (caliProcess.curTotalNum % 10 == 0 && nextpage)
                {
                    this.gridViewModel.currentPage++;
                    if (gridViewModel.currentPage >= gridViewModel.pageCount)
                        gridViewModel.currentPage = gridViewModel.pageCount - 1;
                    this.gridViewModel.curModePage++;
                }
            }
            BindingList<CaliItem> tempItems = new BindingList<CaliItem>(); //数据源
            this.gridViewModel.getItemsList(caliData, tempItems);//给数据源赋值
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
            this.dgvCaliItem.CellValidating += dgvCaliItem_CellValidating;
            this.dgvCaliItem.DataError += dgvCaliItem_DataError;
        }

        //分压器监测加载页面
        private void Load_Page_Divider(bool nextpage)
        {
            if (dividerProcess.complete == EnumCaliState.START)
            {
                if (dividerProcess.curTotalNum % 10 == 0 && nextpage)
                {
                    dividerGridViewModel.currentPage++;
                    if (dividerGridViewModel.currentPage >= dividerGridViewModel.pageCount)
                        dividerGridViewModel.currentPage = dividerGridViewModel.pageCount - 1;
                    this.dividerGridViewModel.curModePage++;
                }
            }
            BindingList<DividerItem> tempItems = new BindingList<DividerItem>(); //数据源
            this.dividerGridViewModel.getItemsList(dividerData, tempItems);      //给数据源赋值
            this.dgvDivider.DataSource = tempItems;                             //绑定数据源
            //清除默认选中项，选中项为上次修改项
            this.dgvCaliItem.ClearSelection();
            labelCurPageDivider.Text = "当前页：" + (dividerGridViewModel.currentPage + 1).ToString() + "/" + (dividerGridViewModel.pageCount).ToString();//当前页
            labelTotalRecordDivider.Text = "总记录：" + dividerGridViewModel.recordCount.ToString();//总记录数 
            //this.dgvCaliItem.CellValidating += dgvCaliItem_CellValidating;
            //this.dgvCaliItem.DataError += dgvCaliItem_DataError;
        }
        void dgvCaliItem_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }
        void dgvCaliItem_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            float min = 0.0f;
            float max = 0.0f;
            switch (gridViewModel.curMode) { 
                case EnumMode.IACI:
                    max = 4000.0f;
                    break;
                case EnumMode.IACF:
                case EnumMode.VACF:
                    max = 1000.0f;
                    break;
                case EnumMode.IDC:
                    max = -4000.0f;
                    max = 4000.0f;
                    break;
                case EnumMode.VDCL:
                    min = -1.0f;
                    max = 1.0f;
                    break;
                case EnumMode.VACVL:
                    min = 0.0f;
                    max = 1.0f;
                    break;
                case EnumMode.VDCHP:
                case EnumMode.VACVH:
                    min = 1.0f;
                    max = 20.0f;
                    break;
                case EnumMode.VDCHN:
                    min = -20.0f;
                    max = -1.0f;
                    break;
            }
            if (e.ColumnIndex < 1 || e.ColumnIndex > 3)
            {
                return;
            }
            // Confirm that the cell is not empty.
            if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
            {
                e.Cancel = true;
                MessageBox.Show("输入不能为空！");
                this.dgvCaliItem.CancelEdit();
            }
            //判断是否符合范围
            else{
                try { 
                    float temp = float.Parse(e.FormattedValue.ToString());
                    if(temp - min < -0.0001f || temp - max > 0.0001f){
                         e.Cancel = true;
                        MessageBox.Show("输入范围超出限制！");
                        this.dgvCaliItem.CancelEdit();
                    }
                }
                catch(FormatException)
                {
                    e.Cancel = true;
                    MessageBox.Show("输入不是有效数值！");
                    this.dgvCaliItem.CancelEdit();
                }
            }
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
            SetCaliItemRatioButton(this.gridViewModel.curMode);
        }
        //分压器监测上一页
        private void btnprepage_divider_Click(object sender, EventArgs e)
        {
            dividerGridViewModel.currentPage -= 1;
            if (dividerGridViewModel.currentPage < 0)
                dividerGridViewModel.currentPage = 0;
            dividerGridViewModel.curModePage -= 1;
            Load_Page_Divider(false);
            SetDividerRatioButton(dividerGridViewModel.curMode);
        }

        //下一页
        private void btnNextPage_Click(object sender, EventArgs e)
        {
            gridViewModel.currentPage += 1;
            if (gridViewModel.currentPage >= gridViewModel.pageCount)
                gridViewModel.currentPage = gridViewModel.pageCount - 1;
            gridViewModel.curModePage += 1;
            Load_Page(false);
            SetCaliItemRatioButton(this.gridViewModel.curMode);
        }
        //分压器监测下一页
        private void btnnextpage_divider_Click(object sender, EventArgs e)
        {
            dividerGridViewModel.currentPage += 1;
            if (dividerGridViewModel.currentPage >= dividerGridViewModel.pageCount)
                dividerGridViewModel.currentPage = dividerGridViewModel.pageCount - 1;
            dividerGridViewModel.curModePage += 1;
            Load_Page_Divider(false);
            SetDividerRatioButton(dividerGridViewModel.curMode);
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
        //上下页翻页时，显示当前校准项
        private void SetCaliItemRatioButton(EnumMode mode) 
        {            
            switch (mode) { 
                case EnumMode.IACI:
                    this.rbIACI.Checked = true;
                    break;
                case EnumMode.IACF:
                    this.rbIACF.Checked = true;
                    break;
                case EnumMode.IDC:
                    this.rbIDC.Checked = true;
                    break;
                case EnumMode.VACF:
                    this.rbVACF.Checked = true;
                    break;
                case EnumMode.VACVL:
                    this.rbVACVL.Checked = true;
                    break;
                case EnumMode.VACVH:
                    this.rbVACVH.Checked = true;
                    break;
                case EnumMode.VDCL:
                    this.rbVDCL.Checked = true;
                    break;
                case EnumMode.VDCHP:
                    this.rbVDCHP.Checked = true;
                    break;
                case EnumMode.VDCHN:
                    this.rbVDCHN.Checked = true;
                    break;
            }            
        }
        //分压器监测上下页翻页时显示测量选项
        public void SetDividerRatioButton(EnumMode mode)
        {
            switch (mode)
            {
                case EnumMode.Divider_V_DCP:
                    this.rbvdcp.Checked = true;
                    break;
                case EnumMode.Divider_V_DCN:
                    this.rbvdcn.Checked = true;
                    break;
                case EnumMode.Divider_V_AC:
                    this.rbvac.Checked = true;
                    break;
                case EnumMode.Divider_F:
                    this.rbf.Checked = true;
                    break;
            }
        }

        //根据当前分压器测量选项，点数限制等
        private void radioButton_CheckedChanged_Divider(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            EnumMode mode = new EnumMode();
            switch (rb.Text) {
                case "电压(DC+)":
                    mode = EnumMode.Divider_V_DCP;
                    break;
                case "电压(DC-)":
                    mode = EnumMode.Divider_V_DCN;
                    break;
                case "电压(AC)":
                    mode = EnumMode.Divider_V_AC;
                    break;
                case "频率":
                    mode = EnumMode.Divider_F;
                    break;
            }
            dividerProcess.initiMode = mode;
            dividerProcess.ResetProcess(mode, dividerData);
            InitDividerGridViewByMode(mode);
            Load_Page_Divider(false);
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
            InitGridViewByMode(mode); 
            Load_Page(false);
        }
        //开始校准
        private void btnstartcali_Click(object sender, EventArgs e)
        {            
            if (caliState == EnumCaliState.INITI)
            {
                //设置定时器定时时间
                Agilent34410ATimer.Interval = INTERNAL;
                CS9010Timer.Interval = INTERNAL;
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
                        Fluke5520ATimer.Interval = INTERNAL;
                        Fluke5520ATimer.Enabled = true;
                        instrumentsState.Fluke5520AState = EnumInstrumentState.LOCAL;
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
                        CS9920ATimer.Interval = INTERNAL;
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
                        CS9920BTimer.Interval = INTERNAL;
                        CS9920BTimer.Enabled = true;
                        instrumentsState.CS9920BState = EnumInstrumentState.INIT;
                    }
                    break;
            }
            Agilent34410ATimer.Enabled = true;
            CS9010Timer.Enabled = true;

            if (caliState != EnumCaliState.PAUSE)
            {
                InitialProgressBar(caliProcess.totalNum,mainMeasureType);
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
        //分压器进度条控件更新值
        private void ShowProgressDivider(int current)
        {
            //说明，progressbar在主线程中定义，进度条线程访问progressbar时需要使用invoke，invokeRequired属性为true
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ProgressBarShow(ShowProgressDivider), current);
            }
            //progressbar在主线程中定义，主线程直接访问progressbar
            else
            {
                this.progressBarDivider.Value = current;
                this.labelCompleteDivider.Text = "已完成:" + current.ToString() + "/" + this.progressBarDivider.Maximum;
            }

        }
        //进度条线程更新函数
        private void updateProgress(object end)
        {
            int current = 0;
            ShowProgress(current);
            int max = (int)end;
            while (current < this.progressBarCali.Maximum)
            {
                current = caliProcess.curTotalNum;
                ShowProgress(current);
                Thread.Sleep(1000);
            }
            progressbarThread.Abort();
            progressbarThread.Join();
        }
        //分压器进度条线程更新函数
        private void updateProgressDivider(object end)
        {
            int current = 0;
            ShowProgressDivider(current);
            int max = (int)end;
            while (current < this.progressBarDivider.Maximum)
            {
                current = dividerProcess.curTotalNum;
                ShowProgressDivider(current);
                Thread.Sleep(1000);
            }
            progressbarThread.Abort();
            progressbarThread.Join();
        }        
        //开始进度条线程
        private void InitialProgressBar(int end,MainMeasureType type)
        {
            if (type == MainMeasureType.CALIBRATION)
            {
                this.progressBarCali.Maximum = end;
                progressbarThread = new Thread(new ParameterizedThreadStart(updateProgress));
            }
            else
            {
                this.progressBarDivider.Maximum = end;
                progressbarThread = new Thread(new ParameterizedThreadStart(updateProgressDivider));
            }
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
                else if (result.Equals("Local\n"))
                {
                    instrumentsState.CS9010State = EnumInstrumentState.SADD;
                }
            }
            else if (instrumentsState.CS9010State == EnumInstrumentState.REMOTE)
            {
                //处理查询到的数据
                string[] datas = result.Split(':');
                //去掉后面的单位
                switch (caliProcess.curMode) { 
                    case EnumMode.IACI:
                    case EnumMode.IDC:
                    case EnumMode.IACF:
                        datas = datas[1].Split('m');
                        break;
                    default:
                        datas = datas[1].Split('k');
                        break;
                }
                float data = float.Parse(datas[0]);
                //读数与源误差较大时，不保存
                if (data / caliProcess.curSource < 0.6f || data / caliProcess.curSource > 1.4f)
                {
                    return;
                }
                else
                {
                    //读数准确时，保存数据
                    if (stateCS9920B == StateSource.OUT_STABLE || stateCS9920A == StateSource.OUT_STABLE || stateFluke5520A == StateSource.OUT_STABLE)
                    {
                        //保存到caliData
                        SaveTestToCaliData(data, caliProcess);
                    }
                }
            }
            else if (instrumentsState.CS9010State == EnumInstrumentState.SENDCALIDATA)
            {
                Console.WriteLine(result);
                if (result.Equals("CAL:Complete\n"))
                {

                    return;
                }
                else {
                }
            }
            else if (instrumentsState.CS9010State == EnumInstrumentState.READCALIDATA)
            {
                //接收数据格式为：CAL:IACI3:1:40.00:40.00
                string[] datas = result.Split(':');
                string mode = datas[1];
                int num = int.Parse(datas[2]) - 1;
                float standValue = float.Parse(datas[3]);
                float testValue = float.Parse(datas[4]);
                switch (mode)
                {
                    //IAC
                    case "IACI3":
                        caliData.iaciData[num].Source = standValue;
                        caliData.iaciData[num].StandOut = standValue;
                        caliData.iaciData[num].TestOut = testValue;
                        break;
                    case "IACI4":
                        caliData.iaciData[5 + num].Source = standValue;
                        caliData.iaciData[5 + num].StandOut = standValue;
                        caliData.iaciData[5 + num].TestOut = testValue;
                        break;
                    case "IACI5":
                        caliData.iaciData[10 + num].Source = standValue;
                        caliData.iaciData[10 + num].StandOut = standValue;
                        caliData.iaciData[10 + num].TestOut = testValue;
                        break;
                    case "IACI6":
                        caliData.iaciData[15 + num].Source = standValue;
                        caliData.iaciData[15 + num].StandOut = standValue;
                        caliData.iaciData[15 + num].TestOut = testValue;
                        break;
                    case "IACI7":
                        caliData.iaciData[20 + num].Source = standValue;
                        caliData.iaciData[20 + num].StandOut = standValue;
                        caliData.iaciData[20 + num].TestOut = testValue;
                        break;
                    case "IACI8":
                        caliData.iaciData[25 + num].Source = standValue;
                        caliData.iaciData[25 + num].StandOut = standValue;
                        caliData.iaciData[25 + num].TestOut = testValue;
                        break;
                    //IDCP
                    case "IDCP3":
                        caliData.idcData[num].Source = standValue;
                        caliData.idcData[num].StandOut = standValue;
                        caliData.idcData[num].TestOut = testValue;
                        break;
                    case "IDCP4":
                        caliData.idcData[5 + num].Source = standValue;
                        caliData.idcData[5 + num].StandOut = standValue;
                        caliData.idcData[5 + num].TestOut = testValue;
                        break;
                    case "IDCP5":
                        caliData.idcData[10 + num].Source = standValue;
                        caliData.idcData[10 + num].StandOut = standValue;
                        caliData.idcData[10 + num].TestOut = testValue;
                        break;
                    case "IDCP6":
                        caliData.idcData[15 + num].Source = standValue;
                        caliData.idcData[15 + num].StandOut = standValue;
                        caliData.idcData[15 + num].TestOut = testValue;
                        break;
                    case "IDCP7":
                        caliData.idcData[20 + num].Source = standValue;
                        caliData.idcData[20 + num].StandOut = standValue;
                        caliData.idcData[20 + num].TestOut = testValue;
                        break;
                    case "IDCP8":
                        caliData.idcData[25 + num].Source = standValue;
                        caliData.idcData[25 + num].StandOut = standValue;
                        caliData.idcData[25 + num].TestOut = testValue;
                        break;
                    //IDCN
                    case "IDCN3":
                        caliData.idcData[30 + num].Source = standValue;
                        caliData.idcData[30 + num].StandOut = standValue;
                        caliData.idcData[30 + num].TestOut = testValue;
                        break;
                    case "IDCN4":
                        caliData.idcData[35 + num].Source = standValue;
                        caliData.idcData[35 + num].StandOut = standValue;
                        caliData.idcData[35 + num].TestOut = testValue;
                        break;
                    case "IDCN5":
                        caliData.idcData[40 + num].Source = standValue;
                        caliData.idcData[40 + num].StandOut = standValue;
                        caliData.idcData[40 + num].TestOut = testValue;
                        break;
                    case "IDCN6":
                        caliData.idcData[45 + num].Source = standValue;
                        caliData.idcData[45 + num].StandOut = standValue;
                        caliData.idcData[45 + num].TestOut = testValue;
                        break;
                    case "IDCN7":
                        caliData.idcData[50 + num].Source = standValue;
                        caliData.idcData[50 + num].StandOut = standValue;
                        caliData.idcData[50 + num].TestOut = testValue;
                        break;
                    case "IDCN8":
                        caliData.idcData[55 + num].Source = standValue;
                        caliData.idcData[55 + num].StandOut = standValue;
                        caliData.idcData[55 + num].TestOut = testValue;
                        break;
                    //IACF
                    case "IACF":
                        caliData.iacfData[num].Source = standValue;
                        caliData.iacfData[num].StandOut = standValue;
                        caliData.iacfData[num].TestOut = testValue;
                        break;
                    //VACV
                    case "VACV":
                        if (caliData.vacvData.Count < num + 1)
                        {
                            CaliItem item = new CaliItem("VACV");
                            caliData.vacvData.Add(item);
                        }
                        caliData.vacvData[num].Source = standValue;
                        caliData.vacvData[num].StandOut = standValue;
                        caliData.vacvData[num].TestOut = testValue;
                        break;
                    //VACF
                    case "VACF":
                        caliData.vacfData[num].Source = standValue;
                        caliData.vacfData[num].StandOut = standValue;
                        caliData.vacfData[num].TestOut = testValue;
                        break;
                    //VDCP
                    case "VDCP":
                        caliData.vdcData[num].Source = standValue;
                        caliData.vdcData[num].StandOut = standValue;
                        caliData.vdcData[num].TestOut = testValue;
                        break;
                    //VDCN
                    case "VDCN":
                        caliData.vdcData[num].Source = standValue;
                        caliData.vdcData[num].StandOut = standValue;
                        caliData.vdcData[num].TestOut = testValue;
                        break;
                }
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
                    caliData.vacvData[thisCaliProcess.curNum].TestOut = data;
                    break;
                case EnumMode.VACVH:
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
                    caliData.vdcData[CaliData.VLOWNUM + thisCaliProcess.curNum].TestOut = data;                   
                    break;
                case EnumMode.VDCHN:
                    caliData.vdcData[CaliData.VDCPNUM + CaliData.VLOWNUM + thisCaliProcess.curNum].TestOut = data;
                    break;
            }
            //已读
            if (thisCaliProcess.curNumTest == thisCaliProcess.curNum-1)
            {
                thisCaliProcess.curNumTest++;
            }
        }
        //CS9010定时发送指令函数
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
            }
            else {               
                //判断是否需要切换模式
                if(caliProcess.changeMode && !caliProcess.changeMode9010)
                {
                    switch(caliProcess.curMode)
                    {
                        case EnumMode.IACI:                            
                            //第一个档位时需要切换模式
                            if (caliProcess.stall == EnumStall.STALL_6) 
                            {
                                command += CommandCS9010.Configure[(int)CommandCS9010.cmdConfigure.IAC];
                                TestSendMessage(command, false);
                                command = "";                                
                            }
                            //判断是否切换档位
                            if (caliProcess.stall != EnumStall.STALL_NO) {
                                command += CommandCS9010.SwitchStall[(int)caliProcess.stall];                                                                                                             
                            }
                            break;
                        case EnumMode.IACF:
                            command += CommandCS9010.Configure[(int)CommandCS9010.cmdConfigure.IAC];
                            TestSendMessage(command, false);
                            command = "";  
                            //切换档位为30mA档
                            command += CommandCS9010.Configure[(int)CommandCS9010.cmdConfigure.STALL4];                                                       
                            break;
                        case EnumMode.IDC:
                            //第一个档位时需要切换模式
                            if (caliProcess.stall == EnumStall.STALL_6)
                            {
                                command += CommandCS9010.Configure[(int)CommandCS9010.cmdConfigure.IAC];
                                TestSendMessage(command, false);
                                command = "";  
                            }
                            //判断是否切换档位
                            if (caliProcess.stall != EnumStall.STALL_NO)
                            {
                                command += CommandCS9010.SwitchStall[(int)caliProcess.stall];
                            }
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
                    if (command != "")
                    {
                        TestSendMessage(command, false);
                    }  
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
                    //发送查询指令时，判断是否源已经稳定
                    if (!(stateCS9920B == StateSource.OUT_STABLE || stateCS9920A == StateSource.OUT_STABLE || stateFluke5520A == StateSource.OUT_STABLE))
                    {
                        return;
                    }
                    if (caliProcess.curMode == EnumMode.IACF || caliProcess.curMode == EnumMode.VACF)
                    {
                        //command += CommandCS9010.Measure[(int)CommandCS9010.cmdMeasure.FREQ];
                        command += CommandCS9010.Measure[(int)CommandCS9010.cmdMeasure.VALUE];
                    }
                    else {
                        command += CommandCS9010.Measure[(int)CommandCS9010.cmdMeasure.VALUE];
                    }
                }

            }
            TestSendMessage(command,true);
        }
        //CS9010串口发送数据
        public void TestSendMessage(string command,bool addCount)
        {
            string cmd = command;
            if (cmd == "")
                return;
            cmd += "#";//结束符
            if (addCount)
            {
                //已发送命令数加1
                sendcount9010++;
            }
            comTest.Write(cmd);
            Console.WriteLine("发送到CS9010:" + cmd);
        } 
        //标准表1串口接收函数
        private void comStand_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int n = comStand.BytesToRead;
            if (n <= 0)
                return;
            byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据
            comStand.Read(buf, 0, n);
            string result = System.Text.Encoding.Default.GetString(buf);
            ResolveDataById(result,1);             
        }
        //标准表1网口socket的定时器
        private void ReceiveTimer() 
        {
            SocketTimer.Elapsed += ReceiveHandler;    //定时事件的方法
            SocketTimer.Interval = 1000;
            SocketTimer.Enabled = true;
        }
        //标准表2网口socket的定时器
        private void ReceiveTimer_2()
        {
            SocketTimer_2.Elapsed += ReceiveHandler_2;    //定时事件的方法
            SocketTimer_2.Interval = 1000;
            SocketTimer_2.Enabled = true;
        }
        //接收标准表1返回的数据
        private void ReceiveHandler(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                byte[] buffer = new byte[100];
                int len = 0;
                if (socketStand.Available > 0)
                {
                    //实际接收到的字节数
                    len = socketStand.Receive(buffer, SocketFlags.None);
                }
                if (len != 0)              
                {
                    string str = Encoding.Default.GetString(buffer, 0, len);
                    ResolveDataById(str,1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("接收标准表1的消息出错:" + ex.ToString());
            }
        }
        //接收标准表2返回的数据
        private void ReceiveHandler_2(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                byte[] buffer = new byte[100];
                int len = 0;
                if (socketStand2.Available > 0)
                {
                    //实际接收到的字节数
                    len = socketStand2.Receive(buffer, SocketFlags.None);
                }
                if (len != 0)
                {
                    string str = Encoding.Default.GetString(buffer, 0, len);
                    ResolveDataById(str, 2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("接收标准表2的消息出错:" + ex.ToString());
            }
        }
        //标准表接收数据后的处理函数
        public void ResolveDataById(string receiveData,int id)
        {
            //对收到的字符进行转换
            string[] strs = receiveData.Split('E');
            int sign = strs[0].StartsWith("+") == true ? 1 : -1;
            float digit = float.Parse(strs[0]);
            int exponent = int.Parse(strs[1]);
            float value = digit * (float)Math.Pow(10, exponent);
            if (stateCS9920B == StateSource.OUT_STABLE || stateCS9920A == StateSource.OUT_STABLE || stateFluke5520A == StateSource.OUT_STABLE)
            {
                if (mainMeasureType == MainMeasureType.CALIBRATION)
                {
                    //保存到caliData
                    SaveStandToCaliData(value, caliProcess);
                }
                else
                {
                    //保存到dividerData
                    SaveVoltToDividerData(value, dividerProcess,id);
                }
            }
            if (id == 1)
            {
                if (sendcount34410A > 0)
                {
                    sendcount34410A--;
                }
            }
            else
            {
                if (sendcount34410A_2 > 0)
                {
                    sendcount34410A_2--;
                } 
            }
            Console.WriteLine("接收到34410A编号" + id + ":" + receiveData);
        }
        //分压器监测时，标准表保存测量值
        public void SaveVoltToDividerData(float data, DividerProcess thisDividerProcess,int id)
        {
            List<DividerItem> list = null;
            switch (thisDividerProcess.curMode)
            {
                case EnumMode.Divider_V_AC:
                    list = dividerData.voltageACData;
                    break;
                case EnumMode.Divider_V_DCP:
                    list = dividerData.voltageDCPData;
                    break;
                case EnumMode.Divider_V_DCN:
                    list = dividerData.voltageDCNData;
                    break;
                case EnumMode.Divider_F:
                    list = dividerData.frequencyData;
                    break;
            }
            if (id == 1)
            {
                //已读
                if (thisDividerProcess.curNumStand == thisDividerProcess.curNum - 1)
                {
                    list[thisDividerProcess.curNum].StandOut = data;
                    thisDividerProcess.curNumStand++;
                }
            }
            else
            {                
                //已读
                if (thisDividerProcess.curNumTest == thisDividerProcess.curNum - 1)
                {
                    list[thisDividerProcess.curNum].TestOut = data;
                    thisDividerProcess.curNumTest++;
                } 
            }
        }

        //校准时标准表34410A保存测量值
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
                    caliData.vacvData[thisCaliProcess.curNum].StandOut = data;
                    break;
                case EnumMode.VACVH:
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
                    caliData.vdcData[CaliData.VLOWNUM + thisCaliProcess.curNum].StandOut = data;
                    break;
                case EnumMode.VDCHN:                   
                    caliData.vdcData[CaliData.VDCPNUM + CaliData.VLOWNUM + thisCaliProcess.curNum].StandOut = data;
                    break;
            }
            //已读
            if (thisCaliProcess.curNumStand == thisCaliProcess.curNum-1)
            {
                thisCaliProcess.curNumStand++;
            }
        }
        //标准表1发送消息函数
        private void StandSendMessage(string command)
        {
            try
            {
                string cmd = command;
                cmd += "\n";
                if (!comStand.IsOpen)
                {
                    byte[] buffer = new byte[100];
                    buffer = Encoding.Default.GetBytes(cmd);
                    //用网口
                    socketStand.Send(buffer);
                }
                else
                {
                    //用串口
                    comStand.Write(cmd);
                }
                Console.WriteLine("发送到34410A编号1:" + cmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show("标准表1发送消息出错:" + ex.Message);
            }
        }
        //标准表2发送消息函数
        private void Stand2SendMessage(string command)
        {
            try
            {
                string cmd = command;
                cmd += "\r\n";
                if (!comStand2.IsOpen)
                {
                    byte[] buffer = new byte[100];
                    buffer = Encoding.Default.GetBytes(cmd);
                    //用网口
                    socketStand2.Send(buffer);
                }
                else
                {
                    //用串口
                    comStand2.Write(cmd);
                }
                Console.WriteLine("发送到34410A编号2:" + cmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show("标准表2发送消息出错:" + ex.Message);
            }
        }
        //校准时34410A定时发送函数
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
                command += CommandAgilent34410A.System[(int)CommandAgilent34410A.cmdSystem.Remote];
                instrumentsState.Agilent34410AState = EnumInstrumentState.REMOTE;
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
                //curNumStand从-1开始，到caliProcess.curModeTotal - 1时，所有点已完成
                if (caliProcess.curNumStand == caliProcess.curModeTotal - 1)
                {
                    return;
                }
                sendcount34410A++;
            }
            StandSendMessage(command);
        }
        //校准时5520A定时发送函数
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
                command += "REMOTE";
                Fluke5520ASendCommand(command);
                instrumentsState.Fluke5520AState = EnumInstrumentState.REMOTE;
            }
            else
            {
                //源和表都已读完数据
                if (caliProcess.curNumTest != -1 && caliProcess.curNumTest == caliProcess.curNumStand)
                {
                    if (caliProcess.curNum == caliProcess.curNumTest)
                    {
                        if (stateFluke5520A == StateSource.OUT_STABLE)
                        {
                            //停止源输出
                            command += "STBY";
                            Fluke5520ASendCommand(command);
                            stateFluke5520A = StateSource.STOP;
                            //校准点更新为已校准
                            this.gridViewModel.updateItem(caliData, caliProcess);
                            //刷新列表
                            dgvCaliItem.Invoke(showProcess, false);
                            caliProcess.curTotalNum++;
                            caliProcess.curNum++;
                            //是否要切换模式
                            caliProcess.getCurMode(caliData);
                            dgvCaliItem.Invoke(showProcess, true);
                            if (caliProcess.complete == EnumCaliState.COMPLETE)
                                caliState = EnumCaliState.COMPLETE;
                            return;
                        }
                    }
                }
                //等待9010切换好模式和电流档位
                if (caliProcess.changeMode && !caliProcess.changeMode9010)
                {
                    return;
                }
                if (stateFluke5520A == StateSource.STOP)
                {
                    command += this.caliProcess.getFlukeSourceString(caliData);
                    stateFluke5520A = StateSource.OUT_START;
                    Fluke5520ASendCommand(command);
                }
                else if (stateFluke5520A == StateSource.OUT_START)
                {
                    stateFluke5520A = StateSource.OUT_STABLE;
                }
            }
        }
        //校准时Fluke5520A的串口接收函数        
        private void com5520A_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

        }
        //校准时CS9920B定时发送函数
        private void CS9920BTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (caliState == EnumCaliState.COMPLETE)
            {
                CS9920BTimer.Enabled = false;
                return;
            }
            //更新进度条
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
                        if(stateCS9920B == StateSource.OUT_STABLE)
                        {
                            //停止源输出
                            command += CommandCS9920.SourceControl[(int)CommandCS9920.cmdSourceControl.STOP];
                            stateCS9920B = StateSource.STOP;
                            CS9920SendCommand(command, EnumInstrumentType.CS9920B);
                            //校准点更新为已校准
                            this.gridViewModel.updateItem(caliData, caliProcess);
                            //刷新列表
                            dgvCaliItem.Invoke(showProcess, false);
                            caliProcess.curTotalNum++;
                            caliProcess.curNum++;
                            //是否要切换模式
                            caliProcess.getCurMode(caliData);
                            dgvCaliItem.Invoke(showProcess, true);
                            if (caliProcess.complete == EnumCaliState.COMPLETE)
                                caliState = EnumCaliState.COMPLETE;
                            return;
                        }
                    }
                }
                //先设置电压
                if (stateCS9920B == StateSource.STOP)
                {
                    command += CommandCS9920.SetVoltage[(int)CommandCS9920.cmdSetVoltage.DC];
                    command += this.caliProcess.getCS9920SourceString(caliData, "CS9920B");
                    stateCS9920B = StateSource.SET_VALUE;
                }
                //再开始输出电压
                else if (stateCS9920B == StateSource.SET_VALUE)
                {
                    command += CommandCS9920.SourceControl[(int)CommandCS9920.cmdSourceControl.STAR];
                    stateCS9920B = StateSource.OUT_START;
                }
                //到达稳定状态
                else if (stateCS9920B == StateSource.OUT_START)
                {
                    stateCS9920B = StateSource.OUT_STABLE;
                }
            }
            if (command.Equals(""))
                return;
            CS9920SendCommand(command, EnumInstrumentType.CS9920B);
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
                        if (stateCS9920A == StateSource.OUT_STABLE)
                        {
                            //停止源输出
                            command += CommandCS9920.SourceControl[(int)CommandCS9920.cmdSourceControl.STOP];
                            stateCS9920A = StateSource.STOP;
                            CS9920SendCommand(command, EnumInstrumentType.CS9920A);
                            //校准点更新为已校准
                            this.gridViewModel.updateItem(caliData, caliProcess);
                            //刷新列表
                            dgvCaliItem.Invoke(showProcess, false);
                            caliProcess.curTotalNum++;
                            caliProcess.curNum++;
                            //是否要切换模式
                            caliProcess.getCurMode(caliData);
                            dgvCaliItem.Invoke(showProcess, true);
                            if (caliProcess.complete == EnumCaliState.COMPLETE)
                                caliState = EnumCaliState.COMPLETE;
                            return;
                        }
                    }
                }
                //先设置电压
                if (stateCS9920A == StateSource.STOP)
                {
                    command += CommandCS9920.SetVoltage[(int)CommandCS9920.cmdSetVoltage.AC];
                    command += this.caliProcess.getCS9920SourceString(caliData, "CS9920A");
                    stateCS9920A = StateSource.SET_VALUE;
                }
                //再设置时间
                else if (stateCS9920A == StateSource.SET_VALUE)
                {
                    command += CommandCS9920.SetTime[(int)CommandCS9920.cmdSetTime.AC];
                    stateCS9920A = StateSource.SET_TIME;
                }
                //再开始输出电压
                else if (stateCS9920A == StateSource.SET_TIME)
                {
                    command += CommandCS9920.SourceControl[(int)CommandCS9920.cmdSourceControl.STAR];
                    stateCS9920A = StateSource.OUT_START;
                }
                else if (stateCS9920A == StateSource.OUT_START) {
                    stateCS9920A = StateSource.OUT_STABLE;
                }
            }
            if (command.Equals(""))
                return;
            CS9920SendCommand(command, EnumInstrumentType.CS9920A);
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
            com9920A.Read(buf, 0, n);
            result += System.Text.Encoding.Default.GetString(buf);
            Console.WriteLine("接收到CS9020A:" + result);
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
        //切换标签函数
        private void dmTabCtrl_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            switch (this.dmTabCtrl.SelectedIndex) { 
                //校准
                case 0:
                    Fluke5520ATimer.Elapsed -= Fluke5520ATimer_Elapsed_Divider;
                    Fluke5520ATimer.Elapsed += Fluke5520ATimer_Elapsed;
                    CS9920ATimer.Elapsed -= CS9920ATimer_Elapsed_Divider;
                    CS9920ATimer.Elapsed += CS9920ATimer_Elapsed;
                    CS9920BTimer.Elapsed -= CS9920BTimer_Elapsed_Divider;
                    CS9920BTimer.Elapsed += CS9920BTimer_Elapsed;
                    Agilent34410ATimer.Elapsed += Agilent34410ATimer_Elapsed_Divider;
                    Agilent34410ATimer.Elapsed +=Agilent34410ATimer_Elapsed;
                    mainMeasureType = MainMeasureType.CALIBRATION;
                    break;
                //分压器监测
                case 1:
                    Fluke5520ATimer.Elapsed -= Fluke5520ATimer_Elapsed;
                    Fluke5520ATimer.Elapsed += Fluke5520ATimer_Elapsed_Divider;
                    CS9920ATimer.Elapsed -= CS9920ATimer_Elapsed;
                    CS9920ATimer.Elapsed += CS9920ATimer_Elapsed_Divider;
                    CS9920BTimer.Elapsed -= CS9920BTimer_Elapsed;
                    CS9920BTimer.Elapsed += CS9920BTimer_Elapsed_Divider;
                    Agilent34410ATimer.Elapsed -= Agilent34410ATimer_Elapsed;
                    Agilent34410ATimer.Elapsed += Agilent34410ATimer_Elapsed_Divider;
                    mainMeasureType = MainMeasureType.DIVIDER;
                    break;
            }
        }
        //分压器监测时标准表1定时发送函数
        private void Agilent34410ATimer_Elapsed_Divider(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (dividerState == EnumCaliState.COMPLETE)
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
                command += CommandAgilent34410A.System[(int)CommandAgilent34410A.cmdSystem.Remote];
                instrumentsState.Agilent34410AState = EnumInstrumentState.REMOTE;
            }
            else
            {
                if (!(stateCS9920B == StateSource.OUT_STABLE || stateCS9920A == StateSource.OUT_STABLE || stateFluke5520A == StateSource.OUT_STABLE))
                {
                    return;
                }
                //根据校准模式发送查询指令                
                switch (dividerProcess.curMode)
                {
                    case EnumMode.Divider_V_AC:
                    case EnumMode.Divider_F:
                        command += CommandAgilent34410A.Measure[(int)CommandAgilent34410A.cmdMeasure.VAC];
                        break;
                    case EnumMode.Divider_V_DCP:
                    case EnumMode.Divider_V_DCN:
                        command += CommandAgilent34410A.Measure[(int)CommandAgilent34410A.cmdMeasure.VDC];
                        break;
                }
                if (dividerProcess.curNumStand == dividerProcess.curModeTotal - 1)
                {
                    return;
                }
                sendcount34410A++;
            }
            StandSendMessage(command);
        }
        //分压器监测时标准表2定时发送函数
        private void Agilent34410ATimer_2_Elapsed_Divider(object sender, System.Timers.ElapsedEventArgs e)       
        {
            if (dividerState == EnumCaliState.COMPLETE)
            {
                Agilent34410ATimer_2.Enabled = false;
                return;
            }
            if (sendcount34410A_2 != 0)
                return;
            string command = "";
            //判断是否已处于远控状态
            if (instrumentsState.Agilent34410AState_2 != EnumInstrumentState.REMOTE)
            {
                command += CommandAgilent34410A.System[(int)CommandAgilent34410A.cmdSystem.Remote];
                Stand2SendMessage(command);
                command = "";
                command += CommandAgilent34410A.System[(int)CommandAgilent34410A.cmdSystem.Clear];
                instrumentsState.Agilent34410AState_2 = EnumInstrumentState.REMOTE;
            }
            else
            {
                if ( !(stateCS9920B == StateSource.OUT_STABLE || stateCS9920A == StateSource.OUT_STABLE || stateFluke5520A == StateSource.OUT_STABLE))
                {
                    return;
                }
                //根据校准模式发送查询指令                
                switch (dividerProcess.curMode)
                {
                    case EnumMode.Divider_V_AC:
                    case EnumMode.Divider_F:
                        command += CommandAgilent34410A.Measure[(int)CommandAgilent34410A.cmdMeasure.VAC];
                        break;
                    case EnumMode.Divider_V_DCP:
                    case EnumMode.Divider_V_DCN:
                        command += CommandAgilent34410A.Measure[(int)CommandAgilent34410A.cmdMeasure.VDC];
                        break;
                }
                if (dividerProcess.curNumTest == dividerProcess.curModeTotal - 1)
                {
                    return;
                }
                sendcount34410A_2++;
            }
            Stand2SendMessage(command);
        }
        //分压器监测5520A定时发送函数
        private void Fluke5520ATimer_Elapsed_Divider(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (dividerState == EnumCaliState.COMPLETE)
            {
                Fluke5520ATimer.Enabled = false;
                return;
            }
            ProcessDelegate showProcess = new ProcessDelegate(Load_Page_Divider);
            string command = "";
            //判断是否已远控

            //todo:发送查询远控命令
            
            if (instrumentsState.Fluke5520AState == EnumInstrumentState.LOCAL)
            {
                command += "REMOTE";
                Fluke5520ASendCommand(command);
                instrumentsState.Fluke5520AState = EnumInstrumentState.REMOTE;
            }
            else
            {
                //标准、待测分压器都已读完电压
                if (dividerProcess.curNumTest != -1 && dividerProcess.curNumTest == dividerProcess.curNumStand && dividerProcess.curNumTest == dividerProcess.curNumTandH)
                {
                    if (dividerProcess.curNum == dividerProcess.curNumTest)
                    {
                        if (stateFluke5520A == StateSource.OUT_STABLE)
                        {
                            //停止源输出
                            command += "STBY";
                            Fluke5520ASendCommand(command);
                            stateFluke5520A = StateSource.STOP;
                            //测量点更新为已测量
                            dividerGridViewModel.updateItem(dividerData, dividerProcess);
                            //刷新列表
                            dgvDivider.Invoke(showProcess, false);
                            //点数加1
                            dividerProcess.curTotalNum++;
                            dividerProcess.curNum++;
                            //是否要切换模式
                            dividerProcess.getCurMode(dividerData);
                            dgvDivider.Invoke(showProcess, true);
                            if (dividerProcess.complete == EnumCaliState.COMPLETE)
                                dividerState = EnumCaliState.COMPLETE;
                            return;
                        }
                    }
                }
                if (stateFluke5520A == StateSource.STOP)
                {
                    command += dividerProcess.getFlukeSourceString(dividerData);
                    stateFluke5520A = StateSource.OUT_START;
                    Fluke5520ASendCommand(command);
                }
                else if (stateFluke5520A == StateSource.OUT_START)
                {
                    stateFluke5520A = StateSource.OUT_STABLE;
                }
            }
        }
        //分压器监测时CS9920A定时发送函数
        private void CS9920ATimer_Elapsed_Divider(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (dividerState == EnumCaliState.COMPLETE)
            {
                CS9920ATimer.Enabled = false;
                return;
            }
            ProcessDelegate showProcess = new ProcessDelegate(Load_Page_Divider);

            if (sendcount9920A != 0)
                return;
            string command = "";
            //判断是否已处于远控状态
            if (instrumentsState.CS9920AState == EnumInstrumentState.INIT)
            {
                //寻址命令
                command += CommandCS9920.Communication[(int)CommandCS9920.cmdCommunication.SADD];
                command += comSetForm.numSadd9920AValue;
            }
            else if (instrumentsState.CS9920AState == EnumInstrumentState.SADD)
            {
                //远控命令
                command += CommandCS9920.Communication[(int)CommandCS9920.cmdCommunication.REMOTE];
            }
            //已是远控状态，开始测量，设置电压并输出
            else
            {
                //每个表都已读完数据
                if (dividerProcess.curNumTest != -1 && dividerProcess.curNumTest == dividerProcess.curNumStand && dividerProcess.curNumTest == dividerProcess.curNumTandH)
                {
                    if (dividerProcess.curNum == dividerProcess.curNumTest)
                    {
                        if (stateCS9920A == StateSource.OUT_STABLE)
                        {
                            //停止源输出
                            command += CommandCS9920.SourceControl[(int)CommandCS9920.cmdSourceControl.STOP];
                            stateCS9920A = StateSource.STOP;
                            CS9920SendCommand(command, EnumInstrumentType.CS9920A);
                            //测量点更新为已测量
                            this.dividerGridViewModel.updateItem(dividerData, dividerProcess);
                            //刷新列表
                            dgvDivider.Invoke(showProcess, false);
                            dividerProcess.curTotalNum++;
                            dividerProcess.curNum++;
                            //是否要切换模式
                            dividerProcess.getCurMode(dividerData);
                            dgvDivider.Invoke(showProcess, true);

                            if (dividerProcess.complete == EnumCaliState.COMPLETE)
                                dividerState = EnumCaliState.COMPLETE;
                            return;
                        }
                    }
                }
                //先设置电压
                if (stateCS9920A == StateSource.STOP)
                {
                    command += CommandCS9920.SetVoltage[(int)CommandCS9920.cmdSetVoltage.AC];
                    string source = this.dividerProcess.getCS9920SourceString(dividerData);
                    if(source == "")
                        return;
                    else
                    {
                        command += source;
                        stateCS9920A = StateSource.SET_VALUE;
                    }
                }
                //再设置时间，默认时间是3s，这里需要设置为不限制时间
                else if (stateCS9920A == StateSource.SET_VALUE)
                {
                    command += CommandCS9920.SetTime[(int)CommandCS9920.cmdSetTime.AC];
                    stateCS9920A = StateSource.SET_TIME;
                }
                //再开始输出电压
                else if (stateCS9920A == StateSource.SET_TIME)
                {
                    command += CommandCS9920.SourceControl[(int)CommandCS9920.cmdSourceControl.STAR];
                    stateCS9920A = StateSource.OUT_START;
                }
                //输出为稳定状态
                else if (stateCS9920A == StateSource.OUT_START)
                {
                    stateCS9920A = StateSource.OUT_STABLE;
                }
            }
            if (command.Equals(""))
                return;
            CS9920SendCommand(command, EnumInstrumentType.CS9920A);
        }
        //分压器监测时CS9920B定时发送函数
        private void CS9920BTimer_Elapsed_Divider(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (dividerState == EnumCaliState.COMPLETE)
            {
                CS9920BTimer.Enabled = false;
                return;
            }
            ProcessDelegate showProcess = new ProcessDelegate(Load_Page_Divider);

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
            else if (instrumentsState.CS9920AState == EnumInstrumentState.SADD)
            {
                //远控
                command += CommandCS9920.Communication[(int)CommandCS9920.cmdCommunication.REMOTE];
            }
            //已是远控状态，开始校准，设置电压并输出
            else
            {
                //都已读完数据
                if (dividerProcess.curNumTest != -1 && dividerProcess.curNumTest == dividerProcess.curNumStand && dividerProcess.curNumTest == dividerProcess.curNumTandH)
                {
                    if (dividerProcess.curNum == dividerProcess.curNumTest)
                    {
                        if (stateCS9920B == StateSource.OUT_STABLE)
                        {
                            //停止源输出
                            command += CommandCS9920.SourceControl[(int)CommandCS9920.cmdSourceControl.STOP];
                            CS9920SendCommand(command, EnumInstrumentType.CS9920B);
                            stateCS9920B = StateSource.STOP;
                            //测量点更新为已测量
                            this.dividerGridViewModel.updateItem(dividerData, dividerProcess);
                            //刷新列表
                            dgvDivider.Invoke(showProcess, false);
                            dividerProcess.curTotalNum++;
                            dividerProcess.curNum++;
                            //是否要切换模式
                            dividerProcess.getCurMode(dividerData);
                            dgvDivider.Invoke(showProcess, true);

                            if (dividerProcess.complete == EnumCaliState.COMPLETE)
                                dividerState = EnumCaliState.COMPLETE;
                            return;
                        }
                    }
                }
                //先设置电压
                if (stateCS9920B == StateSource.STOP)
                {
                    command += CommandCS9920.SetVoltage[(int)CommandCS9920.cmdSetVoltage.AC];
                    command += this.dividerProcess.getCS9920SourceString(dividerData);
                    stateCS9920B = StateSource.SET_VALUE;
                }
                //再设置时间
                else if (stateCS9920B == StateSource.SET_VALUE)
                {
                    command += CommandCS9920.SetTime[(int)CommandCS9920.cmdSetTime.AC];
                    stateCS9920B = StateSource.SET_TIME;
                }
                //再开始输出电压
                else if (stateCS9920B == StateSource.SET_TIME)
                {
                    command += CommandCS9920.SourceControl[(int)CommandCS9920.cmdSourceControl.STAR];
                    stateCS9920B = StateSource.OUT_START;
                }
                else if (stateCS9920B == StateSource.OUT_START)
                {
                    stateCS9920B = StateSource.OUT_STABLE;
                }
            }
            if (command.Equals(""))
                return;
            CS9920SendCommand(command, EnumInstrumentType.CS9920B);
        }
        //9920高压源发送命令
        /*
         * @param:command 需要发送的命令 type 仪器类型
         * 
         */
        public void CS9920SendCommand(string command,EnumInstrumentType type)
        {
            string cmd = command;
            cmd += "#";//结束符
            switch (type)
            {
                case EnumInstrumentType.CS9920A:
                    com9920A.Write(cmd);
                    //已发送命令数加1
                    sendcount9920A++;
                    Console.WriteLine("发送到CS9920A:" + cmd);
                    break;
                case EnumInstrumentType.CS9920B:
                    com9920B.Write(cmd);
                    //已发送命令数加1
                    sendcount9920B++;
                    Console.WriteLine("发送到CS9920B:" + cmd);
                    break;
            }
        }
        //5520A发送命令
        /*
         * @param:command 需要发送的命令
         * 
         */ 
        public void Fluke5520ASendCommand(string command) 
        {
            string cmd = command;
            if (cmd == "")
                return;
            cmd += "\r\n";//结束符
            com5520A.Write(command);
            Console.WriteLine("发送到5520A:" + command);
        }
        //继电器串口接收数据函数
        void comRelay_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }
        //分压器监测时温湿度模块定时发送函数
        public void TandHTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (sendcountTandH > 0)
                return;
            string queryString = "01 03 00 00 00 02 c4 0b";
            byte[] queryBytes = HexString2Bytes(queryString);

            if (stateCS9920B == StateSource.OUT_STABLE || stateCS9920A == StateSource.OUT_STABLE || stateFluke5520A == StateSource.OUT_STABLE)
            {
                comTandH.Write(queryBytes, 0, queryBytes.Length);
                Console.WriteLine("发送到TandH:"+queryString);
                sendcountTandH++;
            }
        }
        //十六进制字符串转换为字节数组
        public byte[] HexString2Bytes(string hs)
        {
            string[] strArr = hs.Trim().Split(' ');
            byte[] bytes = new byte[strArr.Length];
            //逐个字符变为16进制字节数据
            for (int i = 0; i < strArr.Length; i++)
            {
                bytes[i] = Convert.ToByte(strArr[i], 16);
            }
            //按照指定编码将字节数组变为字符串
            return bytes;
        }
        //温湿度模块串口接收数据函数
        void comTandH_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int n = comTandH.BytesToRead;
            if (n < 9)
            {
                return;
            }
            byte[] readBuffer = new byte[n];
            comTandH.Read(readBuffer, 0, n);
            string result = "";
            for (int i = 0; i < readBuffer.Length; i++)
            {
                result += readBuffer[i]+" ";
            }
            Console.WriteLine("接收到TandH:" + result);
            if (stateCS9920B == StateSource.OUT_STABLE || stateCS9920A == StateSource.OUT_STABLE || stateFluke5520A == StateSource.OUT_STABLE)
            {
                //保存到dividerData
                SaveTandHToDividerData(readBuffer,dividerProcess);
            }
            if (sendcountTandH > 0)
            {
                sendcountTandH--;
            } 
        }
       
        //温湿度保存测量值
        public void SaveTandHToDividerData(byte[] data,DividerProcess tempProcess) 
        {
            //计算温度
            byte highT = data[5];
            byte lowT = data[6];
            int sign = 1;
            //温度为负，原数据用补码表示
            if (data[5] >= 128)
            {
                highT = (byte)~highT;
                lowT = (byte)(~lowT + 1);
                sign = -1;
            }
            float temperature = sign * (highT * 256 + lowT) / 10.0f;
            //计算湿度
            int highH = data[3];
            int lowH = data[4];
            float humidity = (highH * 256 + lowH) / 10.0f;
            switch (tempProcess.curMode)
            {
                case EnumMode.Divider_V_DCP:
                    dividerData.voltageDCPData[tempProcess.curNum].Temperature = temperature;
                    dividerData.voltageDCPData[tempProcess.curNum].Humidity = humidity;
                    break;
                case EnumMode.Divider_V_DCN:
                    dividerData.voltageDCNData[tempProcess.curNum].Temperature = temperature;
                    dividerData.voltageDCNData[tempProcess.curNum].Humidity = humidity;
                    break;
                case EnumMode.Divider_V_AC:
                    dividerData.voltageACData[tempProcess.curNum].Temperature = temperature;
                    dividerData.voltageACData[tempProcess.curNum].Humidity = humidity;
                    break;
                case EnumMode.Divider_F:
                    dividerData.frequencyData[tempProcess.curNum].Temperature = temperature;
                    dividerData.frequencyData[tempProcess.curNum].Humidity = humidity;
                    break;
            }
            //已读
            if (tempProcess.curNumTandH == tempProcess.curNum - 1)
            {
                tempProcess.curNumTandH++;
            }
        }
        //标准表2串口接收数据函数
        private void comStand2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int n = comStand2.BytesToRead;
            if (n <= 0)
                return;
            byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据
            comStand2.Read(buf, 0, n);
            string result = System.Text.Encoding.Default.GetString(buf);
            ResolveDataById(result,2);             
        }
        //保存校准结果到数据库
        private void btnsavecali_Click(object sender, EventArgs e)
        {
            if ( DAO.SaveCaliDataAll(caliData) )
                MessageBox.Show("保存数据成功");
            else
                MessageBox.Show("保存数据出错");
        }
        //保存分压器测量数据到数据库
        private void btnSaveDivider_Click(object sender, EventArgs e)
        {
            bool result = false;
            //判断测量种类
            if (dividerProcess.modeMeasType)
            {
                //全部
                result = DAO.SaveDividerDataAll(dividerData);
            }
            else
            {
                dividerProcess.initiMode = EnumMode.Divider_V_AC;
                //当前，保存当前测量项的数据
                result = DAO.SaveDividerDataByMode(dividerData, dividerProcess.initiMode);
            }
            if (result)
                MessageBox.Show("保存数据成功");
            else
                MessageBox.Show("保存数据出错");
        }
        //数据下传至待校表
        private void btnsendcali_Click(object sender, EventArgs e)
        {
            instrumentsState.CS9010State = EnumInstrumentState.SENDCALIDATA;
            //CAL:VACF 1:50.00:1001.00#CAL:VACF 2:150.00:1000.00#...CAL:VACF 10:950.00:1000.00#CAL:VACF 10E#
            var cmd = new CommandCS9010.cmdCalibrationSet();
            string[] cmds = Enum.GetNames(cmd.GetType()); 
            int cmdNum = 0;
            int num = 0;
            for (; cmdNum < cmds.Length; cmdNum++) 
            {
                string command = "";
                string preffix = CommandCS9010.CalibrationSet[cmdNum];
                int start = 0;
                int end = 0;
                float stand = 0;
                float test = 0;
                List<CaliItem> list = null;
                switch (cmdNum)
                {
                    case (int)CommandCS9010.cmdCalibrationSet.IACI:                        
                        end = caliData.iaciData.Count;
                        list = caliData.iaciData;
                        break;
                    case (int)CommandCS9010.cmdCalibrationSet.IDCP:
                        end = 30;
                        list = caliData.idcData;
                        break;
                    case (int)CommandCS9010.cmdCalibrationSet.IDCN:
                        start = 30; 
                        end = caliData.idcData.Count;
                        list = caliData.idcData;
                        break;
                    case (int)CommandCS9010.cmdCalibrationSet.IACF:
                        end = caliData.iacfData.Count;
                        list = caliData.iacfData;
                        break;
                    case (int)CommandCS9010.cmdCalibrationSet.VACF:
                        end = caliData.vacfData.Count;
                        list = caliData.vacfData;
                        break;
                    case (int)CommandCS9010.cmdCalibrationSet.VACV:
                        end = caliData.vacvData.Count;
                        list = caliData.vacvData;
                        break;
                    case (int)CommandCS9010.cmdCalibrationSet.VDCP:
                        end = CaliData.VDCPNUM;
                        list = caliData.vdcData;
                        break;
                    case (int)CommandCS9010.cmdCalibrationSet.VDCN:
                        start = CaliData.VDCPNUM;
                        end = caliData.vdcData.Count;
                        list = caliData.vdcData;
                        break;                             
                }
                if(cmdNum == (int)CommandCS9010.cmdCalibrationSet.IACI || cmdNum == (int)CommandCS9010.cmdCalibrationSet.IDCP || cmdNum == (int)CommandCS9010.cmdCalibrationSet.IDCN)
                {
                    for (num = start; num < end; num++)
                    {
                        stand = list[num].StandOut;
                        test = list[num].TestOut;
                        command += preffix;
                        //电阻序号
                        int stall = 3 + (num-start) / 5;
                        command += stall + " ";
                        //序号
                        command += 1 + (num % 5) + ":";
                        //标准值
                        command += stand + ":";
                        //测量值
                        command += test+"#";
                        if (num % 5 == 4)
                        {
                            //总和
                            command += preffix + stall + " " + 5 + "E";
                            TestSendMessage(command, false);
                            command = "";
                            Thread.Sleep(1000);//休眠时间
                        }
                    }
                }
                else{
                    for (num = start; num < end; num++)
                    {
                        stand = list[num].StandOut;
                        if (cmdNum == (int)CommandCS9010.cmdCalibrationSet.IACI || cmdNum == (int)CommandCS9010.cmdCalibrationSet.VACF)
                        {
                            stand = list[num].Source;
                        }
                        test = list[num].TestOut;
                        command += preffix;
                        //序号
                        command += 1 + num-start + ":";
                        //标准值
                        command += stand +":";
                        //测量值
                        command += test + "#";
                    }
                    //总和
                    command += preffix + (num-start) + "E";
                    TestSendMessage(command, false);                
                }
                Thread.Sleep(1000);//休眠时间
            }
            instrumentsState.CS9010State = EnumInstrumentState.REMOTE;
        }
        //读取待校表数据

        private void btnReadCali_Click(object sender, EventArgs e)
        {
            //数据查询
            var cmd = new CommandCS9010.cmdCalibrationGet();
            int i = 0;
            for (; i < Enum.GetNames(cmd.GetType()).Length; i++)
            {
                string command = CommandCS9010.CalibrationGet[i];
                TestSendMessage(command, true);
                Thread.Sleep(200);//休眠时间
            }
        }
        //分压器某一种类测量结束后，保存数据是否是自动
        private void rbSaveAuto_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            switch (rb.Text) { 
                case "自动":
                    this.dividerProcess.modeSaveData = true;
                    break;
                case "手动":
                    this.dividerProcess.modeSaveData = false;
                    break;
            }
        }
        //分压器测量种类，当前和全部
        private void rbMeasTypeCur_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            switch (rb.Text)
            {
                case "当前":
                    this.dividerProcess.modeMeasType = false;
                    break;
                case "全部":
                    this.dividerProcess.modeMeasType = true;
                    break;
            }
        }
        //分压器测量时是否循环测量
        private void rbCycleYes_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            switch (rb.Text)
            {
                case "是":
                    this.dividerProcess.modeCycleSwitch = true;
                    break;
                case "否":
                    this.dividerProcess.modeCycleSwitch = false;
                    break;
            }
        }
        //分压器开始监测
        private void btnDividerStart_Click(object sender, EventArgs e)
        {
            this.dividerProcess.initiMode = this.dividerProcess.curMode;
            if (dividerState == EnumCaliState.INITI)
            {
                //设置定时器定时时间
                Agilent34410ATimer.Interval = INTERNAL;
                Agilent34410ATimer_2.Interval = INTERNAL;
                TandHTimer.Interval = INTERNAL;
            }
            //从取消校准，开始校准
            else if (dividerState == EnumCaliState.CANCEL)
            {
                //重置caliData,caliProcess
                dividerData.Reset(dividerProcess.modeMeasType,dividerProcess.initiMode);
                dividerProcess.ResetProcess(dividerProcess.initiMode, dividerData);
            }
            //判断两个Agilent34410表是否连接
            if ((socketStand == null || !socketStand.Connected) && !comStand.IsOpen)
            {
                MessageBox.Show("标准表1未连接");
                return;
            }
            if ((socketStand2 == null || !socketStand2.Connected) && !comStand2.IsOpen)
            {
                MessageBox.Show("标准表2未连接");
                return;
            }
            if (!comTandH.IsOpen)
            {
                MessageBox.Show("温湿度模块未连接");
                return;
            }
            //根据模式判断源是否已连接，打开对应定时器
            switch (dividerProcess.curMode)
            {
                case EnumMode.Divider_F:
                    if (!com5520A.IsOpen)
                    {
                        MessageBox.Show("源Fluke5520A未连接");
                        return;
                    }
                    else
                    {
                        Fluke5520ATimer.Interval = INTERNAL;
                        Fluke5520ATimer.Enabled = true;
                        instrumentsState.Fluke5520AState = EnumInstrumentState.LOCAL;
                    }
                    break;
                case EnumMode.Divider_V_AC:
                    if (!com9920A.IsOpen)
                    {
                        MessageBox.Show("源CS9920A未连接");
                        return;
                    }
                    else
                    {
                        CS9920ATimer.Interval = INTERNAL;
                        instrumentsState.CS9920AState = EnumInstrumentState.INIT;
                        CS9920ATimer.Enabled = true;
                    }
                    break;
                case EnumMode.Divider_V_DCP:
                case EnumMode.Divider_V_DCN:
                    if (!com9920B.IsOpen)
                    {
                        MessageBox.Show("源CS9920B未连接");
                        return;
                    }
                    else
                    {
                        CS9920BTimer.Interval = INTERNAL;
                        CS9920BTimer.Enabled = true;
                        instrumentsState.CS9920BState = EnumInstrumentState.INIT;
                    }
                    break;
            }
            Agilent34410ATimer.Enabled = true;
            Agilent34410ATimer_2.Enabled = true;
            TandHTimer.Enabled = true;

            if (dividerState != EnumCaliState.PAUSE)
            {
                InitialProgressBar(dividerProcess.totalNum,mainMeasureType);
            }
            dividerProcess.complete = EnumCaliState.START;
            dividerState = EnumCaliState.START;
        }
    }
}
