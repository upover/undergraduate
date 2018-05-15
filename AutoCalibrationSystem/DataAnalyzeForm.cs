using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoCalibrationSystem.DataAccess;

namespace AutoCalibrationSystem
{
    public partial class DataAnalyzeForm : Form
    {
        public DataAnalyzeForm()
        {
            InitializeComponent();
        }

        private void DataAnalyzeForm_Load(object sender, EventArgs e)
        {

        }
        //选择数据
        private void btnSelectData_Click(object sender, EventArgs e)
        {
            DateTime start = this.timePickerStart.Value;
            DateTime end = this.timePickerEnd.Value;
            int id = (int)this.numDividerId.Value;
            //数据源
            DataTable dt = DAO.SelectDividerDataByTime(start,end,id);
            this.dgvDividerHistory.DataSource = dt;  //绑定数据源
        }
    }
}
