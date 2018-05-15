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
        public bool changeMode;         //是否需要切换模式
        public bool changeMode9010;     //9010是否已切换模式
        public bool type;               //校准方式：true连续，false当前
        public EnumStall stall;         //切换电流档位时档位值
        public float curSource;         //当前校准掉的输入源
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
            string source = "OUT ";
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
                    temp += "HZ,1KV";
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
                    this.curSource = list.ElementAt(offset + curNum - CaliData.VLOWNUM).Source;
                }
                else
                    this.curSource = list.ElementAt(curNum).Source;
            }
            else
            {
                this.curSource = list.ElementAt(curNum).Source;
            }
            source += this.curSource.ToString() + temp+";OPER";           
            return source;
        }
        public string getCS9920SourceString(CaliData caliData,String type)
        {
            List<CaliItem> list = null;
            switch(type)
            {
                //直流高压
                case "CS9920B":
                    list = caliData.vdcData;
                    if(this.curMode == EnumMode.VDCHP)
                        this.curSource = list.ElementAt(this.curNum + CaliData.VLOWNUM).Source;
                    else if (this.curMode == EnumMode.VDCHN)
                        this.curSource = list.ElementAt(this.curNum + CaliData.VLOWNUM + CaliData.VDCPNUM).Source;
                    else if (this.curMode == EnumMode.VDCL)
                    {
                        if (this.curNum < CaliData.VLOWNUM)
                        {
                            this.curSource = list.ElementAt(this.curNum).Source;
                        }
                        else
                        {
                            //输出转为正，负电压时换方向实现
                            this.curSource = (-1 * list.ElementAt(CaliData.VDCPNUM + this.curNum - CaliData.VLOWNUM).Source); 
                        }
                    }
                    break;
                //交流高压
                case "CS9920A":
                    list = caliData.vacvData;
                    if (this.curMode == EnumMode.VACVH)
                    {
                        this.curSource = list.ElementAt(this.curNum + CaliData.VLOWNUM).Source; 
                    }
                    else if (this.curMode == EnumMode.VACVL)
                        this.curSource = list.ElementAt(this.curNum).Source;                                                  
                    break;
            }
            return this.curSource.ToString();
        }
        public void getCurMode(CaliData caliData)
        {
            changeMode = false;
            this.stall = EnumStall.STALL_NO;
            //判断当前模式是否校准完成
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
                switch (curMode)
                {
                    case EnumMode.IACI:
                        curMode = EnumMode.IACF;
                        this.stall = EnumStall.STALL_4;//频响校准时需要切换档位
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
                        curModeTotal = CaliData.VLOWNUM * 2;
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
            //电流模式需要切换档位
            if (curMode == EnumMode.IACI || curMode == EnumMode.IDC) 
            {
                //每5个点切换一次档位
                if (curNum % 5 == 0) 
                {
                    changeMode = true;
                    changeMode9010 = false;
                    int tempStall = curNum / 5;
                    switch (tempStall)
                    {
                        case 0:
                        case 6:
                            this.stall = EnumStall.STALL_6;
                            break;
                        case 1:
                        case 7:
                            this.stall = EnumStall.STALL_5;
                            break;
                        case 2:
                        case 8:
                            this.stall = EnumStall.STALL_4;
                            break;
                        case 3:
                        case 9:
                            this.stall = EnumStall.STALL_3;
                            break;
                        case 4:
                        case 10:
                            this.stall = EnumStall.STALL_2;
                            break;
                        case 5:
                        case 11:
                            this.stall = EnumStall.STALL_1;
                            break;
                    }                    
                }
            }        
        }
    }
}
