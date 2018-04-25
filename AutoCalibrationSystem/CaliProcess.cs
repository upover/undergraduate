using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCalibrationSystem
{
    public class CaliProcess
    {

        //校准序号
        public EnumMode curMode;        //当前校准模式
        public EnumMode initiMode;      //初始校准模式
        public int curNum;              //当前点序号
        public int curModeTotal;        //当前模式总点数
        public int curNumTest;          //当前待校表点数
        public int curNumStand;         //当前标准表点数
        public EnumCaliState complete;  //校准状态
        public int curTotalNum;         //当前已校准的总点数
        public int totalNum;            //需要校准的总点数 
        public bool changeMode;
        public bool changeMode9010;
        public bool type;              //校准方式：true连续，false当前
        public CaliProcess() {
           
        }
        public CaliProcess(EnumMode mode,CaliData caliData) {
            this.curNum = 0;
            this.curNumTest = -1;
            this.curNumStand = -1;
            this.changeMode = true;
            this.complete = EnumCaliState.INITI;
            this.curTotalNum = 0;
            this.curMode = mode;
            this.initiMode = mode;
            setTotalNumByType(caliData);
        }
        public void setTotalNumByType(CaliData caliData) 
        {
            int total = 0;
            switch (this.curMode)
            {
                case EnumMode.IACI:
                    curModeTotal = caliData.iaciData.Count;
                    total = caliData.iaciData.Count + caliData.iacfData.Count + caliData.idcData.Count
                            + caliData.vacfData.Count + caliData.vacvData.Count + caliData.vdcData.Count;
                    break;
                case EnumMode.IACF:
                    curModeTotal = caliData.iacfData.Count;
                    total = caliData.iacfData.Count + caliData.idcData.Count
                            + caliData.vacfData.Count + caliData.vacvData.Count + caliData.vdcData.Count;
                    break;
                case EnumMode.IDC:
                    curModeTotal = caliData.idcData.Count;
                    total = caliData.idcData.Count + caliData.vacfData.Count + caliData.vacvData.Count + caliData.vdcData.Count;
                    break;
                case EnumMode.VACF:
                    curModeTotal = caliData.vacfData.Count;
                    total = caliData.vacfData.Count + caliData.vacvData.Count + caliData.vdcData.Count;
                    break;
                case EnumMode.VACVL:
                    curModeTotal = CaliData.VLOWNUM;
                    total = caliData.vacvData.Count + caliData.vdcData.Count;
                    break;
                case EnumMode.VDCL:
                    curModeTotal = CaliData.VLOWNUM * 2;
                    total = caliData.vacvData.Count - CaliData.VLOWNUM + caliData.vdcData.Count;
                    break;
                case EnumMode.VACVH:
                    curModeTotal = caliData.vacvData.Count - CaliData.VLOWNUM;
                    total = caliData.vacvData.Count - CaliData.VLOWNUM + caliData.vdcData.Count - CaliData.VLOWNUM * 2;  
                    break;
                case EnumMode.VDCHP:
                    curModeTotal = CaliData.VDCPNUM - CaliData.VLOWNUM;
                    total = caliData.vdcData.Count - CaliData.VLOWNUM * 2;                    
                    break;
                case EnumMode.VDCHN:
                    curModeTotal = CaliData.VDCPNUM - CaliData.VLOWNUM; 
                    total = curModeTotal;
                    break;
            }
            //方式是全部校准
            if (this.type)
            {
                this.totalNum = total;
            }
            //方式是当前模式校准
            else
            {
                this.totalNum = this.curModeTotal;
            }
        }
        public string getFlukeSourceString(CaliData caliData)
        {
            string source = "STBY;";
            string temp = "";
            List<CaliItem> list = null;
            int offset = 0;
            switch (curMode)
            {
                case EnumMode.IACI:
                    list = caliData.iaciData;
                    temp += "MA,50HZ";
                    break;
                case EnumMode.IACF:
                    list = caliData.iacfData;
                    temp += "HZ,12MA";
                    break;
                case EnumMode.IDC:
                    list = caliData.idcData;
                    temp += "MA,0HZ";
                    break;
                case EnumMode.VACF:
                    list = caliData.vacfData;
                    temp += "KV,1KV";
                    break;
                case EnumMode.VACVL:
                    list = caliData.vacvData;
                    temp += "KV,50HZ";
                    break;
                case EnumMode.VDCL:
                    list = caliData.vdcData;
                    offset = CaliData.VDCPNUM;
                    temp += "KV,0HZ";
                    break;
                case EnumMode.VACVH:
                    list = caliData.vacvData;
                    offset = CaliData.VLOWNUM;
                    temp += "KV,50HZ";
                    break;
                case EnumMode.VDCHP:
                    list = caliData.vdcData;
                    offset = CaliData.VLOWNUM;
                    temp += "KV,0HZ";
                    break;
                case EnumMode.VDCHN:
                    list = caliData.vdcData;
                    offset = CaliData.VDCPNUM + CaliData.VLOWNUM;
                    temp += "KV,0HZ";
                    break;
            }
            if (curMode == EnumMode.VDCL)
            {
                //负向电压低压部分
                if (curNum >= CaliData.VLOWNUM)
                {
                    source += list.ElementAt(offset + curNum - CaliData.VLOWNUM).Source.ToString();
                }
                else
                    source += list.ElementAt(curNum).Source.ToString();
            }
            else
            {
                source += list.ElementAt(curNum).Source.ToString();
            }
            source += temp+";OPER";
            return source;
        }
        public string getCS9920SourceString(CaliData caliData,String type)
        {
            string source = null;
            List<CaliItem> list = null;
            switch(type)
            {
                //直流高压
                case "CS9920B":
                    list = caliData.vdcData;

                    if(this.curMode == EnumMode.VDCHP)
                        source = list.ElementAt(this.curNum + CaliData.VLOWNUM).Source.ToString();
                    else if (this.curMode == EnumMode.VDCHN)
                        source = list.ElementAt(this.curNum + CaliData.VLOWNUM + CaliData.VDCPNUM).Source.ToString();
                    else if (this.curMode == EnumMode.VDCL)
                    {
                        if (this.curNum < CaliData.VLOWNUM)
                        {
                            source = list.ElementAt(this.curNum).Source.ToString();
                        }
                        else
                        {
                            //输出转为正，负电压时换方向实现
                            source = (-1*list.ElementAt(CaliData.VDCPNUM + this.curNum - CaliData.VLOWNUM).Source).ToString(); 
                        }

                    }
                    break;
                //交流高压
                case "CS9920A":

                    list = caliData.vacvData;
                    if (this.curMode == EnumMode.VDCHP)
                    {
                        source = list.ElementAt(this.curNum + CaliData.VLOWNUM).Source.ToString(); 
                    }
                    else if (this.curMode == EnumMode.VACVL)
                        source = list.ElementAt(this.curNum).Source.ToString();                                                  
                    break;
            }
            return source;
        }
        public void getCurMode(CaliData caliData)
        {
            if (curNum == curModeTotal)
            {
                if (type == false)
                {
                    this.complete = EnumCaliState.COMPLETE;
                    return;
                }
                curNum = 0;
                curNumTest = -1;
                curNumStand = -1;
                changeMode = true;
                changeMode9010 = false;
                //进入到下一模式
                switch (curMode) { 
                    case EnumMode.IACI:
                        curMode = EnumMode.IACF;
                        curModeTotal = caliData.iacfData.Count;
                        break;
                    case EnumMode.IACF:
                        curMode = EnumMode.IDC;
                        curModeTotal = caliData.idcData.Count;
                        break;
                    case EnumMode.IDC:
                        curMode = EnumMode.VACF;
                        curModeTotal = caliData.vacfData.Count;
                        break;
                    case EnumMode.VACF:
                        curMode = EnumMode.VACVL;
                        curModeTotal = CaliData.VLOWNUM;
                        break;
                    case EnumMode.VACVL:
                        curMode = EnumMode.VDCL;
                        curModeTotal = CaliData.VLOWNUM*2;
                        break;
                    case EnumMode.VDCL:
                        curMode = EnumMode.VACVH;
                        curModeTotal = caliData.vacvData.Count - CaliData.VLOWNUM;
                        break;
                    case EnumMode.VACVH:
                        curMode = EnumMode.VDCHP;
                        curModeTotal = CaliData.VDCPNUM - CaliData.VLOWNUM;
                        break;
                    case EnumMode.VDCHP:
                        curMode = EnumMode.VDCHN;
                        curModeTotal = CaliData.VDCPNUM - CaliData.VLOWNUM;
                        break;
                    case EnumMode.VDCHN:
                        this.complete = EnumCaliState.COMPLETE;
                        break;
                }
            }
            else
                changeMode = false;
        }
    }
}
