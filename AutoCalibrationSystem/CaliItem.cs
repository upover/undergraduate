using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCalibrationSystem
{
    public class CaliItem
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
        public float StandOut
        {
            get;
            set;
        }
        public float TestOut
        {
            get;
            set;
        }
        //状态，0未校准，1已校准
        public byte State
        {
            get;
            set;
        }
        public CaliItem(String mode) {
            Mode = mode;
            StandOut = 0;
            TestOut = 0;
            State = 0;
        }
        public CaliItem(int source, int standOut, int testOut)
        {
            Source = source;
            StandOut = standOut;
            TestOut = testOut;
            State = 0;
        }
    }
}
