using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCalibrationSystem
{
    //委托事件传递参数，继承自EventArgs
    public class UtilEventArgs : EventArgs
    {
        public String Parmater
        {
            get;
            set;
        }
        public UtilEventArgs(String parmater)
        {
            this.Parmater = parmater;
        }

        public UtilEventArgs()
        {
            // TODO: Complete member initialization
        }
    }
}
