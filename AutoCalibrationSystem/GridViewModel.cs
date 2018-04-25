using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AutoCalibrationSystem
{
    //表格DataGridView相关数据
    public class GridViewModel
    {
        public BindingList<CaliItem> items; //数据源
        public static int pageSize = 10;      //每页记录数
        public static int MINNUM = 2;
        public static int MAXNUM_V = 30;
        public static int MAXNUM_F = 20;
        public static int MAXNUM_I = 10;
        public int recordCount;    //总记录数
        public int pageCount;      //总页数
        public int currentPage;    //当前页
        public int curModePageCount;      //当前校准项总页数
        public int curModePage;      //当前校准项页数
        public int curModeCount;      //当前校准项总点数
        public int selectIndex;    //当前选中项
        public EnumMode curMode;    //当前校准模式
        public int currentItem;    //当前校准项
        public int maxNum;
        public int minNum;
        public float tempValue;
        public GridViewModel()
        {
            minNum = MINNUM;
            maxNum = MAXNUM_V;
            recordCount = 0;
            pageCount = 0;
            currentPage = 0;
            items = new BindingList<CaliItem>();
        }
        //设置选中项索引
        public void SetSelectedIndex(int index) {
            
            if (index >= pageSize)
            {
                index = pageSize - 1;
            }
            if (index <= 0) {
                index = 0;
            }
            selectIndex = index;
        }
        //设置当前校准种类
        public void SetCurrentType(EnumMode type)
        {
            curMode = type;
        }
        public EnumMode GetCurrentType()
        {
            return curMode;
        }
        //根据校准项的点数获取页数
        private void getCurModePageCount(CaliData caliData)
        {
            switch (this.curMode)
            {
                case EnumMode.IACI:
                    this.curModeCount = caliData.iaciData.Count;
                    break;
                case EnumMode.IACF:
                    this.curModeCount = caliData.iacfData.Count;
                    break;
                case EnumMode.IDC:
                    this.curModeCount = caliData.idcData.Count;
                    break;
                case EnumMode.VACF:
                    this.curModeCount = caliData.vacfData.Count;
                    break;
                case EnumMode.VACVL:
                    this.curModeCount = CaliData.VLOWNUM;
                    break;
                case EnumMode.VDCL:
                    this.curModeCount = CaliData.VLOWNUM * 2;
                    break;
                case EnumMode.VACVH:
                    this.curModeCount = CaliData.VACNUM - CaliData.VLOWNUM;
                    break;
                case EnumMode.VDCHP:
                    this.curModeCount = CaliData.VDCPNUM - CaliData.VLOWNUM;
                    break;
                case EnumMode.VDCHN:
                    this.curModeCount = CaliData.VDCPNUM - CaliData.VLOWNUM;
                    break;
            }
            this.curModePageCount = (this.curModeCount % GridViewModel.pageSize == 0) ? this.curModeCount / GridViewModel.pageSize : this.curModeCount / GridViewModel.pageSize + 1;
        }
        //获取数据源
        public void getItemsList(CaliData caliData, BindingList<CaliItem> tempItems)
        {
            List<CaliItem> listItems = null;
            this.getCurModePageCount(caliData);
            //翻页时判断是否需要切换模式
            //下一页
            if (this.curModePage >= this.curModePageCount)
            {
                if (this.curMode < EnumMode.VDCHN)
                {
                    this.curMode++;
                    this.curModePage = 0;
                }
                else 
                {
                    this.curModePage = this.curModePageCount-1; 
                }
            }
            //上一页
            else if (this.curModePage < 0)
            {
                if (this.curMode > EnumMode.IACI)
                {
                    this.curMode--;
                    this.getCurModePageCount(caliData);
                    this.curModePage = this.curModePageCount - 1;
                }
                else 
                {
                    this.curModePage = 0;
                }
            }
            int beginRecord = GridViewModel.pageSize * this.curModePage;
            int endRecord = GridViewModel.pageSize * (this.curModePage + 1) - 1;
            int offset = 0;
            switch (this.curMode)
            {
                case EnumMode.IACI:
                    listItems = caliData.iaciData;
                    this.curModeCount = caliData.iaciData.Count;                    
                    break;
                case EnumMode.IACF:
                    listItems = caliData.iacfData;
                    this.curModeCount = caliData.iacfData.Count;
                    break;
                case EnumMode.IDC:
                    listItems = caliData.idcData;
                    this.curModeCount = caliData.idcData.Count;
                    break;
                case EnumMode.VACF:
                    listItems = caliData.vacfData;
                    this.curModeCount = caliData.vacfData.Count;
                    break;
                case EnumMode.VACVL:
                    listItems = caliData.vacvData;
                    this.curModeCount = CaliData.VLOWNUM;
                    break;
                case EnumMode.VDCL:
                    listItems = caliData.vdcData;
                    this.curModeCount = CaliData.VLOWNUM*2;
                    offset = CaliData.VDCPNUM;
                    break;
                case EnumMode.VACVH:
                    listItems = caliData.vacvData;
                    this.curModeCount = CaliData.VACNUM - CaliData.VLOWNUM;
                    offset = CaliData.VLOWNUM;
                    break;
                case EnumMode.VDCHP:
                    listItems = caliData.vdcData;
                    this.curModeCount = CaliData.VDCPNUM - CaliData.VLOWNUM;
                    offset = CaliData.VLOWNUM;
                    break;
                case EnumMode.VDCHN:
                    listItems = caliData.vdcData;
                    this.curModeCount = CaliData.VDCPNUM - CaliData.VLOWNUM;
                    offset = CaliData.VDCPNUM + CaliData.VLOWNUM;
                    break;
            }
            if (this.curMode == EnumMode.VDCL)
            {
                for (int i = beginRecord; i <= endRecord; i++)
                {
                    if (i >= this.curModeCount)
                    {
                        break;
                    }
                    if(i >= CaliData.VLOWNUM)
                    {
                        tempItems.Add(listItems.ElementAt(offset+i-CaliData.VLOWNUM));                        
                    }
                    else
                    {
                        tempItems.Add(listItems.ElementAt(i));
                    }

                }
            }
            else 
            {
                for (int i = beginRecord; i <= endRecord; i++)
                {
                    if (i >= this.curModeCount)
                    {
                        break;
                    }
                    tempItems.Add(listItems.ElementAt(offset + i));
                }
            }
        }
        //更新校准项
        public void updateItem(CaliData caliData, CaliProcess caliProcess)
        {
            int index = caliProcess.curNum;
            CaliItem item = null;
            switch (caliProcess.curMode)
            {
                case EnumMode.IACI:
                    item = caliData.iaciData[index];                    
                    break;
                case EnumMode.IACF:
                    item = caliData.iacfData[index];
                    break;
                case EnumMode.IDC:
                    item = caliData.idcData[index];
                    break;

                case EnumMode.VACF:
                    item = caliData.vacfData[index];
                    break;
                case EnumMode.VACVL:
                    item = caliData.vacvData[index];
                    break;
                case EnumMode.VDCL:
                    if (index < CaliData.VLOWNUM)
                    {
                        item = caliData.vdcData[index];
                    }
                    else
                    {
                        item = caliData.vdcData[CaliData.VDCPNUM + index - CaliData.VLOWNUM];
                    }
                    break;
                case EnumMode.VACVH:
                    item = caliData.vacvData[CaliData.VLOWNUM + index];
                    break;
                case EnumMode.VDCHP:
                    item = caliData.vdcData[CaliData.VLOWNUM + index];
                    break;
                case EnumMode.VDCHN:
                    item = caliData.vdcData[CaliData.VDCPNUM + CaliData.VLOWNUM + index];                  
                    break;
            }
            item.State = 1;
        }
        /*
        public void updateItem(CaliData caliData,CaliProcess caliProcess) {
            int index = caliProcess.curNum;
            CaliItem item = null;
            switch (caliProcess.curMode)
            {                    
                case EnumMode.IACI:
                    item = caliData.iaciData[index];
                    index += 0;
                    break;
                case EnumMode.IACF:
                    item = caliData.iacfData[index];
                    index += caliData.iaciData.Count;
                    break;
                case EnumMode.IDC:
                    item = caliData.idcData[index];
                    index += caliData.idcData.Count+caliData.iacfData.Count;
                    break;

                case EnumMode.VACF:
                    item = caliData.vacfData[index];
                    index += caliData.iaciData.Count + caliData.idcData.Count+caliData.iacfData.Count;
                    break;
                case EnumMode.VACVL:
                    item = caliData.vacvData[index];
                    index += caliData.iaciData.Count + caliData.idcData.Count+caliData.iacfData.Count+caliData.vacfData.Count;
                    break;
                case EnumMode.VDCL:
                    if (index < CaliData.VLOWNUM)
                    {
                        item = caliData.vdcData[index];
                    }
                    else
                    {
                        item = caliData.vdcData[CaliData.VDCPNUM + index - CaliData.VLOWNUM];
                    }
                    index += caliData.iaciData.Count + caliData.idcData.Count+caliData.iacfData.Count+caliData.vacfData.Count+CaliData.VLOWNUM;
                    break;                     
                case EnumMode.VACVH:
                    item = caliData.vacvData[CaliData.VLOWNUM + index];
                    index += caliData.iaciData.Count + caliData.idcData.Count+caliData.iacfData.Count+caliData.vacfData.Count+CaliData.VLOWNUM*3;
                    break;                     
                case EnumMode.VDCHP:
                    item = caliData.vdcData[CaliData.VLOWNUM + index];
                    index += caliData.iaciData.Count + caliData.idcData.Count+caliData.iacfData.Count+caliData.vacfData.Count+caliData.vacvData.Count+CaliData.VLOWNUM*2;
                    break;
                case EnumMode.VDCHN:
                    item = caliData.vdcData[CaliData.VDCPNUM + CaliData.VLOWNUM + index];
                    index += caliData.iaciData.Count + caliData.idcData.Count+caliData.iacfData.Count+caliData.vacfData.Count+caliData.vacvData.Count+CaliData.VDCPNUM+CaliData.VLOWNUM;
                    break;
            }
            items.RemoveAt(index);
            item.State = 1;
            items.Insert(index, item);
        }
        */
    }
}
