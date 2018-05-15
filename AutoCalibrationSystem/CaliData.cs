using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCalibrationSystem
{
    public class CaliData
    {
        //存放校准点
        public List<CaliItem> vdcData; 
        public List<CaliItem> vacvData;
        public List<CaliItem> vacfData;
        public List<CaliItem> idcData;
        public List<CaliItem> iaciData;
        public List<CaliItem> iacfData;

        public static int MINNUM = 2;
        public static int MAXNUMF = 20;
        public static int MAXNUMV = 30;
        public static int VLOWNUM = 3;
        public static int VDCPNUM = 23;
        public static int VACNUM = 23;
        public CaliData()
        {
            vdcData = new List<CaliItem>(VDCPNUM*2);
            for (int i = 0; i < VDCPNUM*2; i++)
            {
                CaliItem item; 
                if(i < VDCPNUM)
                {
                    item = new CaliItem("VDCP");
                }
                else
                {
                    item = new CaliItem("VDCN");
                }
                vdcData.Add(item);
            }
            vacvData = new List<CaliItem>(VACNUM);
            for (int i = 0; i < VACNUM; i++)
            {
                CaliItem item;
                if (i < VLOWNUM)
                {
                    item = new CaliItem("VACVL");
                }
                else
                {
                    item = new CaliItem("VACVH");
                }
                vacvData.Add(item);
            }
            vacfData = new List<CaliItem>(10);
            for (int i = 0; i < 10; i++)
            {
                CaliItem item = new CaliItem("VACF");
                vacfData.Add(item);
            }
            idcData = new List<CaliItem>(60);
            for (int i = 0; i < 60; i++)
            {
                CaliItem item = new CaliItem("IDC");
                idcData.Add(item);
            }
            iaciData = new List<CaliItem>(30);
            for (int i = 0; i < 30; i++)
            {
                CaliItem item = new CaliItem("IACI");
                iaciData.Add(item);
            }
            iacfData = new List<CaliItem>(10);
            for (int i = 0; i < 10; i++)
            {
                CaliItem item = new CaliItem("IACF");
                iacfData.Add(item);
            }
            ResetSource();
        }
        public void Reset(CaliProcess caliProcess)
        {
            int i=0;
            switch (caliProcess.curMode) 
            { 
                case EnumMode.IACF:
                    for(i = 0;i<iacfData.Count;i++)
                    {
                        iacfData[i].StandOut = 0;
                        iacfData[i].TestOut = 0;
                    }
                    if (caliProcess.type)
                    {
                        goto case EnumMode.IACI;
                    }
                    break;
                case EnumMode.IACI:
                    for (i=0; i < iaciData.Count; i++)
                    {
                        iaciData[i].StandOut = 0;
                        iaciData[i].TestOut = 0;
                    }
                    if (caliProcess.type)
                    {
                        goto case EnumMode.IDC;
                    }
                    break;
                case EnumMode.IDC:
                    for (i = 0; i < idcData.Count; i++)
                    {
                        idcData[i].StandOut = 0;
                        idcData[i].TestOut = 0;
                    }
                    if (caliProcess.type)
                    {
                        goto case EnumMode.VACF;
                    }
                    break;
                case EnumMode.VACF:
                    for (i = 0; i < vacfData.Count; i++)
                    {
                        vacfData[i].StandOut = 0;
                        vacfData[i].TestOut = 0;
                    }
                    if (caliProcess.type)
                    {
                        goto case EnumMode.VACVL;
                    }
                    break;
                case EnumMode.VACVL:
                    for (i = 0; i < CaliData.VLOWNUM; i++)
                    {
                        vacvData[i].StandOut = 0;
                        vacvData[i].TestOut = 0;
                    }
                    if (caliProcess.type)
                    {
                        goto case EnumMode.VDCL;
                    }
                    break;
                case EnumMode.VDCL:
                    for (i = 0; i < CaliData.VLOWNUM; i++)
                    {
                        vdcData[i].StandOut = 0;
                        vdcData[i].TestOut = 0;
                    }
                    if (caliProcess.type)
                    {
                        goto case EnumMode.VACVH;
                    }
                    break;
                case EnumMode.VACVH:
                    for (i = CaliData.VLOWNUM; i < vacvData.Count; i++)
                    {
                        vacvData[i].StandOut = 0;
                        vacvData[i].TestOut = 0;
                    }
                    if (caliProcess.type)
                    {
                        goto case EnumMode.VDCHP;
                    }
                    break;
                case EnumMode.VDCHP:
                    for (i = CaliData.VLOWNUM; i < CaliData.VDCPNUM; i++)
                    {
                        vdcData[i].StandOut = 0;
                        vdcData[i].TestOut = 0;
                    }
                    if (caliProcess.type)
                    {
                        goto case EnumMode.VDCHN;
                    }
                    break;
                case EnumMode.VDCHN:
                    for (i = CaliData.VLOWNUM + CaliData.VDCPNUM; i < vdcData.Count; i++)
                    {
                        vdcData[i].StandOut = 0;
                        vdcData[i].TestOut = 0;
                    }
                    break;
            }
        }
        public void ResetSource()
        {
            //vdc初始化
            //正向
            vdcData[0].Source = 0.05f; //索引从1开始
            vdcData[1].Source = 0.1f;
            vdcData[2].Source = 0.5f;
            for (int i = VLOWNUM; i < VDCPNUM; i++)
            {
                vdcData[i].Source = i - (VLOWNUM-1);
            }
            //反向
            vdcData[VDCPNUM].Source = -0.05f;
            vdcData[VDCPNUM + 1].Source = -0.1f;
            vdcData[VDCPNUM + 2].Source = -0.5f;
            for (int i = VDCPNUM + 5; i < vdcData.Count; i++)
            {
                vdcData[i].Source = -1 * (i - (VDCPNUM +4));
            }
            //vacv初始化
            vacvData[0].Source = 0.05f;
            vacvData[1].Source = 0.1f;
            vacvData[2].Source = 0.3f;           
            for (int i = VLOWNUM; i < vacvData.Count; i++)
            {
                vacvData[i].Source = i - (VLOWNUM - 1);
            }
            //vacf初始化
            for (int i = 0; i < vacfData.Count; i++)
            {
                vacfData[i].Source = 50 + 100*i;
            }
            //iaci初始化
            float[] temparray = { 
                                  40,80,120,160,200,     
                                  8,16,24,32,40,
                                  1.810f,3.620f,5.4298f,7.2398f,9.0498f,
                                  0.4f,0.8f,1.2f,1.6f,2.0f,
                                  0.08f,0.16f,0.24f,0.32f,0.40f,
                                  0.0266f,0.05332f,0.08f,0.10666f,0.13332f,
                                };
            for (int i = 0; i < temparray.Length; i++) {
                iaciData[i].Source = temparray[i];
            }
            //iacf初始化
            for (int i = 0; i < iacfData.Count; i++)
            {
                iacfData[i].Source = 50 + 100 * i;
            }
            //idc初始化
            //正、反向
            for (int i = 0; i < temparray.Length; i++)
            {
                idcData[i].Source = temparray[i];
                idcData[i+temparray.Length].Source = -1*temparray[i];
            }

        }
    }
}
