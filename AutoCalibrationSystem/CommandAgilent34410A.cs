using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCalibrationSystem
{
    public class CommandAgilent34410A
    {
        public static string[] System = { "SYST:REM", "SYST:LOC","*CLS" };
        public static string[] Measure = { "MEAS:VOLT:DC? DEF,DEF", "MEAS:VOLT:AC? DEF,DEF", "MEAS:CURR:DC? DEF,DEF", "MEAS:CURR:AC? DEF,DEF", "MEAS:FREQ? DEF,DEF" };
        public enum cmdSystem { Remote, Local,Clear };
        public enum cmdMeasure { VDC, VAC, IDC, IAC, FREQ };
    }
}
