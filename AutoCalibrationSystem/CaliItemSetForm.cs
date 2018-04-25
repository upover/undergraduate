using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCalibrationSystem
{
    public partial class CaliItemSetForm : Form
    {
        public CaliItemSetForm()
        {
            InitializeComponent();
        }

        private void CaliItemSetForm_Load(object sender, EventArgs e)
        {
            this.udPoints.Value = 10;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
