using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCalibrationSystem
{
    public class DividerItem
    {
        public String Mode
        {
            get;
            set;
        }
        public float Source {
            get;
            set;
        }
        //标准分压器电压值
        public float StandOut
        {
            get;
            set;
        }
        //待测试分压器电压值
        public float TestOut
        {
            get;
            set;
        }
        //频率
        public float Frequency
        {
            get;
            set;
        }
        //温度
        public float Temperature
        {
            get;
            set;
        }
        //湿度
        public float Humidity
        {
            get;
            set;
        }
        //状态，0未测量，1已测量
        public byte State
        {
            get;
            set;
        }
        public DividerItem(String mode) {
            Mode = mode;
            StandOut = 0;
            TestOut = 0;
            State = 0;
        }
    }
}
