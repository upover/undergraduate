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

namespace AutoCalibrationSystem
{
    public partial class ComSetForm : Form
    {

        //public event EventHandler ComOpenHandler;
        //public event EventHandler LanOpenHandler;
        //public event EventHandler LanCloseHandler;
        //public event EventHandler ComCloseHandler;
        public UtilEventArgs utilEventArgs = new UtilEventArgs();
        public delegate bool ComOpenHandler(object sender,UtilEventArgs args);
        public delegate bool ComCloseHandler(object sender, UtilEventArgs args);
        public delegate bool LanOpenHandler(object sender, UtilEventArgs args);
        public delegate bool LanCloseHandler(object sender, UtilEventArgs args);

        public ComOpenHandler comOpenHandler;
        public ComCloseHandler comCloseHandler;
        public LanOpenHandler lanOpenHandler;
        public LanCloseHandler lanCloseHandler;

        //返回各表的仪器地址号
        public string numSaddTestValue
        {
            get
            {
                return this.SaddTest.Value.ToString();
            }
            set
            {
                this.SaddTest.Value = int.Parse(value);
            }
        }
        public string numSadd9920AValue
        {
            get
            {
                return this.Sadd9920A.Value.ToString();
            }
            set
            {
                this.Sadd9920A.Value = int.Parse(value);
            }
        }
        public string numSadd9920BValue
        {
            get
            {
                return this.Sadd9920B.Value.ToString();
            }
            set
            {
                this.Sadd9920B.Value = int.Parse(value);
            }
        }

        //返回串口号
        public string boxComTestValue {
            get {
                return this.boxComTest.SelectedItem.ToString();
            }
            set {
                this.boxComTest.Text = value;
            }
        }
        public string boxComStandValue
        {
            get
            {
                return this.boxComStand.SelectedItem.ToString();
            }
            set
            {
                this.boxComStand.Text = value;
            }
        }
        public string boxCom5520AValue
        {
            get
            {
                return this.boxCom5520A.SelectedItem.ToString();
            }
            set
            {
                this.boxCom5520A.Text = value;
            }
        }
        public string boxCom9920AValue
        {
            get
            {
                return this.boxCom9920A.SelectedItem.ToString();
            }
            set
            {
                this.boxCom9920A.Text = value;
            }
        }
        public string boxCom9920BValue
        {
            get
            {
                return this.boxCom9920B.SelectedItem.ToString();
            }
            set
            {
                this.boxCom9920B.Text = value;
            }
        }
        //返回波特率
        public string boxBaudrateTestValue
        {
            get
            {
                return this.boxBaudrateTest.SelectedItem.ToString();
            }
            set
            {
                this.boxBaudrateTest.Text = value;
            }
        }
        public string boxBaudrateStandValue
        {
            get
            {
                return this.boxBaudrateStand.SelectedItem.ToString();
            }
            set
            {
                this.boxBaudrateStand.Text = value;
            }
        }
        public string boxBaudrate5520AValue
        {
            get
            {
                return this.boxBaudrate5520A.SelectedItem.ToString();
            }
            set
            {
                this.boxBaudrate5520A.Text = value;
            }
        }
        public string boxBaudrate9920AValue
        {
            get
            {
                return this.boxBaudrate9920A.SelectedItem.ToString();
            }
            set
            {
                this.boxBaudrate9920A.Text = value;
            }
        }
        public string boxBaudrate9920BValue
        {
            get
            {
                return this.boxBaudrate9920B.SelectedItem.ToString();
            }
            set
            {
                this.boxBaudrate9920B.Text = value;
            }
        }
        //返回网口号
        public string TextIPStandValue
        {
            get
            {
                return this.textIPStand.Text.Trim().ToString();
            }
            set
            {
                this.textIPStand.Text = value;
            }
        }
        //返回端口号
        public string TextPortStandValue
        {
            get
            {
                return this.textPortStand.Text.Trim().ToString();
            }
            set
            {
                this.textPortStand.Text = value;
            }
        }
        public ComSetForm()
        {
            InitializeComponent();
        }
        private void ComSetForm_Load(object sender, EventArgs e)
        {
            this.boxComTest.Items.Clear();
            this.boxComStand.Items.Clear();
            this.boxCom5520A.Items.Clear();
            this.boxCom9920A.Items.Clear();
            this.boxCom9920B.Items.Clear();
            string[] str = SerialPort.GetPortNames();
            if (str != null)
            {
                foreach (string s in SerialPort.GetPortNames())
                {
                    this.boxComTest.Items.Add(s);
                    this.boxComStand.Items.Add(s);
                    this.boxCom5520A.Items.Add(s);
                    this.boxCom9920A.Items.Add(s);
                    this.boxCom9920B.Items.Add(s);
                }
                this.boxComTest.SelectedIndex = 0;
                this.boxComStand.SelectedIndex = 0;
                this.boxCom5520A.SelectedIndex = 0;
                this.boxCom9920A.SelectedIndex = 0;
                this.boxCom9920B.SelectedIndex = 0;
            }
            this.boxBaudrateTest.SelectedIndex = 1;
            this.boxBaudrateStand.SelectedIndex = 1;
            this.boxBaudrate5520A.SelectedIndex = 1;
            this.boxBaudrate9920B.SelectedIndex = 1;
            this.boxBaudrate9920A.SelectedIndex = 1;
        }
 

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //打开串口按钮待校准表9010
        private void btnComTest_Click(object sender, EventArgs e)
        {
            utilEventArgs.Parmater = "comTest";
            if (btnComTest.Text == "打开串口")
            {
                if (comOpenHandler != null)
                {
                    //打开成功
                    if (comOpenHandler(this, utilEventArgs))
                    {
                        btnComTest.Text = "关闭串口";
                    }
                }
            }
            else if (btnComTest.Text == "关闭串口")
            {
                if (comCloseHandler != null)
                {
                    comCloseHandler(this, utilEventArgs);
                    btnComTest.Text = "打开串口";
                }

            }
        }
        //打开串口按钮标准表34410A
        private void btnComStand_Click(object sender, EventArgs e)
        {
            utilEventArgs.Parmater = "comStand";
            if (btnComStand.Text == "打开串口")
            {
                if (comOpenHandler != null)
                {
                    if (comOpenHandler(this, utilEventArgs))
                    {
                        btnComStand.Text = "关闭串口";
                    }
                }
            }
            else if (btnComStand.Text == "关闭串口")
            {
                if (comCloseHandler != null)
                {
                    if (comCloseHandler(this, utilEventArgs))
                    {
                        btnComStand.Text = "打开串口";
                    }
                }
             }
        }
        //打开串口按钮5520A
        private void btnCom5520A_Click(object sender, EventArgs e)
        {
            utilEventArgs.Parmater = "com5520A";
            if (btnCom5520A.Text == "打开串口")
            {
                if (comOpenHandler != null)
                {
                    if (comOpenHandler(this, utilEventArgs))
                    {
                        btnCom5520A.Text = "关闭串口";
                    }
                }

            }
            else if (btnCom5520A.Text == "关闭串口")
            {
                if (comCloseHandler != null)
                {
                    if (comCloseHandler(this, utilEventArgs))
                    {
                        btnCom5520A.Text = "打开串口";
                    }
                }

            }
        }
        //打开串口按钮9920A
        private void btnCom9920A_Click(object sender, EventArgs e)
        {
            utilEventArgs.Parmater = "com9920A";
            if (btnCom9920A.Text == "打开串口")
            {
                if (comOpenHandler != null)
                {
                    if (comOpenHandler(this, utilEventArgs))
                    {
                        btnCom9920A.Text = "关闭串口";
                    }
                }

            }
            else if (btnCom9920A.Text == "关闭串口")
            {
                if (comCloseHandler != null)
                {
                    if (comCloseHandler(this, utilEventArgs))
                    {
                        btnCom9920A.Text = "打开串口";
                    }
                }
            }
        }
        //打开串口按钮9920B
        private void btn9920B_Click(object sender, EventArgs e)
        {
            utilEventArgs.Parmater = "com9920B";
            if (btnCom9920B.Text == "打开串口")
            {
                if (comOpenHandler != null)
                {
                    if (comOpenHandler(this, utilEventArgs))
                    {
                        btnCom9920B.Text = "关闭串口";
                    }
                }

            }
            else if (btnCom9920B.Text == "关闭串口")
            {
                if (comCloseHandler != null)
                {
                    if (comCloseHandler(this, utilEventArgs))
                    {
                        btnCom9920B.Text = "打开串口";
                    }
                }

            }
        }
        //网口
        private void btnLanStand_Click(object sender, EventArgs e)
        {
            if (btnLanStand.Text == "打开网口")
            {
                if (lanOpenHandler != null)
                {
                    if (lanOpenHandler(this, utilEventArgs))
                    {
                        btnLanStand.Text = "关闭网口";
                    }
                }
            }
            else if (btnLanStand.Text == "关闭网口")
            {
                if (lanCloseHandler != null)
                {
                    if (lanCloseHandler(this, utilEventArgs))
                    {
                        btnLanStand.Text = "打开网口";
                    }
                }
            }
        }
    }
}
