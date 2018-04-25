using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCalibrationSystem
{
    public class CommandAgilent34410A
    {
        public static string[] System = { "SYST:REM", "SYST:LOC" };
        public static string[] Measure = { "MEAS:VOLT:DC?", "MEAS:VOLT:AC?", "MEAS:CURR:DC?", "MEAS:CURR:AC?", "MEAS:FREQ?" };
        public enum cmdSystem { Remote, Local };
        public enum cmdMeasure { VDC, VAC, IDC, IAC, FREQ };
    }
}
