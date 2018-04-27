using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCalibrationSystem
{
    public class CommandCS9010
    {
        public static string[] Communication = { "COMM:CONT?","COMM:SADD ","COMM:REM","COMM:LOC"};
        public static string[] Configure = { "CONF:MMOD?", "CONF:VOLT DC", "CONF:VOLT AC", "CONF:CURR DC", "CONF:CURR AC", "CONF:MANSTAll 1", "CONF:MANSTAll 2", "CONF:MANSTAll 3", "CONF:MANSTAll 4", "CONF:MANSTAll 5", "CONF:MANSTAll 6" };
        public static string[] Measure = { "MEAS:VAL?", "MEAS:FREQ?"};
        public enum cmdCommunication { QUERY, SADD, REMOTE, LOCAL };
        public enum cmdConfigure { QUERY, VDC, VAC, IDC, IAC, STALL1, STALL2, STALL3, STALL4, STALL5, STALL6, };
        public enum cmdMeasure { VALUE,FREQ };
    }
}
