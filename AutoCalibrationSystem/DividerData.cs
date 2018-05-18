using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCalibrationSystem
{
    public class DividerData
    {
        //存放测量点
        public List<DividerItem> voltageDCPData;
        public List<DividerItem> voltageDCNData;
        public List<DividerItem> voltageACData;
        public List<DividerItem> frequencyData;
        public List<DividerItem> temperatureData;
        public List<DividerItem> humidityData;

        public static int MINNUM = 5;
        public static int MAXNUM = 10;
        public static int num = 10;
        public DividerData()
        {
            DividerItem item = null;

            voltageDCPData = new List<DividerItem>(num);
            for (int i = 0; i < num; i++)
            {
                item = new DividerItem("VDCP");                
                voltageDCPData.Add(item);
            }
            voltageDCNData = new List<DividerItem>(num);
            for (int i = 0; i < num; i++)
            {
                item = new DividerItem("VDCN");
                voltageDCNData.Add(item);
            }
            voltageACData = new List<DividerItem>(num);
            for (int i = 0; i < num; i++)
            {
                item = new DividerItem("VAC");
                voltageACData.Add(item);
            }
            frequencyData = new List<DividerItem>(num*2);
            for (int i = 0; i < num*2; i++)
            {
                item = new DividerItem("F");
                frequencyData.Add(item);
            }
            ResetSource();
        }
        public void Reset(bool measureType,EnumMode mode)
        {
            int i=0;
            switch (mode) 
            { 
                case EnumMode.Divider_V_DCP:
                    for(i = 0;i<voltageDCPData.Count;i++)
                    {
                        voltageDCPData[i].StandOut = 0;
                        voltageDCPData[i].TestOut = 0;
                        voltageDCPData[i].Humidity = 0;
                        voltageDCPData[i].Temperature = 0;
                        voltageDCPData[i].State = 0;
                    }
                    if (measureType)
                    {
                        goto case EnumMode.Divider_V_DCN;
                    }
                    break;
                case EnumMode.Divider_V_DCN:
                    for (i = 0; i < voltageDCNData.Count; i++)
                    {
                        voltageDCNData[i].StandOut = 0;
                        voltageDCNData[i].TestOut = 0;
                        voltageDCNData[i].Humidity = 0;
                        voltageDCNData[i].Temperature = 0;
                        voltageDCNData[i].State = 0;
                    }
                    if (measureType)
                    {
                        goto case EnumMode.Divider_V_AC;
                    }
                    break;
                case EnumMode.Divider_V_AC:
                    for (i = 0; i < voltageACData.Count; i++)
                    {
                        voltageACData[i].StandOut = 0;
                        voltageACData[i].TestOut = 0;
                        voltageACData[i].Humidity = 0;
                        voltageACData[i].Temperature = 0;
                        voltageACData[i].State = 0;
                    }
                    if (measureType)
                    {
                        goto case EnumMode.Divider_F;
                    }
                    break;
                case EnumMode.Divider_F:
                    for (i=0; i < frequencyData.Count; i++)
                    {
                        frequencyData[i].StandOut = 0;
                        frequencyData[i].TestOut = 0;
                        frequencyData[i].Humidity = 0;
                        frequencyData[i].Temperature = 0;
                        frequencyData[i].State = 0;
                    }
                    if (measureType)
                    {
                        //最后一个测量模式;
                    }
                    break;
            }
        }
        public void ResetSource()
        {
            //以电压为变量
            //DC:正向和反向 AC: 50Hz
            for (int i = 0; i < num; i++)
            {
                voltageDCPData[i].Source = i+1;
                voltageDCNData[i].Source = -1 * (i + 1);
                voltageACData[i].Source = (i + 1)*0.07f;
                voltageACData[i].Frequency = 50;
            }
            //以频率为变量
            //100V
            for (int i = 0; i < num; i++)
            {
                frequencyData[i].Source = 0.1f;
                frequencyData[i].Frequency = 50 + i * 100;
            }
            //500V
            for (int i = 0; i < num; i++)
            {
                frequencyData[i+10].Source = 0.5f;
                frequencyData[i+10].Frequency = 50 + i * 100;
            }
        }
    }
}
