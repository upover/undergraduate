using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AutoCalibrationSystem
{
    //表格DataGridView相关数据
    public class DividerGridViewModel
    {
        public BindingList<DividerItem> items; //数据源
        public static int pageSize = 10;       //每页记录数
        public int recordCount;                //总记录数
        public int pageCount;                  //总页数
        public int currentPage;                //当前页
        public int curModePageCount;           //当前校准项总页数
        public int curModePage;                //当前校准项页数
        public int curModeCount;               //当前校准项总点数
        public int selectIndex;                //当前选中项
        public EnumMode curMode;               //当前校准模式
        public int currentItem;                //当前校准项
        public int maxNum;
        public int minNum;
        public float tempValue;
        public DividerGridViewModel()
        {
            recordCount = 0;
            pageCount = 0;
            currentPage = 0;
            items = new BindingList<DividerItem>();
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
        private void getCurModePageCount(DividerData dividerData)
        {
            switch (this.curMode)
            {
                case EnumMode.Divider_V_DCP:
                    this.curModeCount = dividerData.voltageDCPData.Count;
                    break;
                case EnumMode.Divider_V_DCN:
                    this.curModeCount = dividerData.voltageDCNData.Count;
                    break;
                case EnumMode.Divider_V_AC:
                    this.curModeCount = dividerData.voltageACData.Count;
                    break;
                case EnumMode.Divider_F:
                    this.curModeCount = dividerData.frequencyData.Count;
                    break;
            }
            this.curModePageCount = (this.curModeCount % DividerGridViewModel.pageSize == 0) ? this.curModeCount / DividerGridViewModel.pageSize : this.curModeCount / DividerGridViewModel.pageSize + 1;
        }
        //获取数据源
        public void getItemsList(DividerData dividerData, BindingList<DividerItem> tempItems)
        {
            List<DividerItem> listItems = null;
            this.getCurModePageCount(dividerData);
            //翻页时判断是否需要切换模式
            //下一页
            if (this.curModePage >= this.curModePageCount)
            {
                if (this.curMode < EnumMode.Divider_F)
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
                if (this.curMode > EnumMode.Divider_V_DCP)
                {
                    this.curMode--;
                    this.getCurModePageCount(dividerData);
                    this.curModePage = this.curModePageCount - 1;
                }
                else 
                {
                    this.curModePage = 0;
                }
            }
            int beginRecord = DividerGridViewModel.pageSize * this.curModePage;
            int endRecord = DividerGridViewModel.pageSize * (this.curModePage + 1) - 1;
            switch (this.curMode)
            {
                case EnumMode.Divider_V_DCP:
                    listItems = dividerData.voltageDCPData;
                    this.curModeCount = dividerData.voltageDCPData.Count;                    
                    break;
                case EnumMode.Divider_V_DCN:
                    listItems = dividerData.voltageDCNData;
                    this.curModeCount = dividerData.voltageDCNData.Count;
                    break;
                case EnumMode.Divider_V_AC:
                    listItems = dividerData.voltageACData;
                    this.curModeCount = dividerData.voltageACData.Count;
                    break;
                case EnumMode.Divider_F:
                    listItems = dividerData.frequencyData;
                    this.curModeCount = dividerData.frequencyData.Count;
                    break;
            }
            for (int i = beginRecord; i <= endRecord; i++)
            {
                if (i >= this.curModeCount)
                {
                    break;
                }
                tempItems.Add(listItems.ElementAt(i));                        
            }
        }
        //更新测量项
        public void updateItem(DividerData dividerData, DividerProcess dividerProcess)
        {
            int index = dividerProcess.curNum;
            DividerItem item = null;
            switch (dividerProcess.curMode)
            {
                case EnumMode.Divider_V_DCP:
                    item = dividerData.voltageDCPData[index];                    
                    break;
                case EnumMode.Divider_V_DCN:
                    item = dividerData.voltageDCNData[index];
                    break;
                case EnumMode.Divider_V_AC:
                    item = dividerData.voltageACData[index];
                    break;
                case EnumMode.Divider_F:
                    item = dividerData.frequencyData[index];
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
