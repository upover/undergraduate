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
        public static string[] Configure = { "CONF:MMOD?", "CONF:VOLT DC", "CONF:VOLT AC", "CONF:CURR DC", "CONF:CURR AC", 
                                               "CONF:MANSTAll 1", "CONF:MANSTAll 2", "CONF:MANSTAll 3", "CONF:MANSTAll 4", "CONF:MANSTAll 5", "CONF:MANSTAll 6" };
        public static string[] SwitchStall = { "CONF:CURSTA AUTO","CONF:MANSTAll 1", "CONF:MANSTAll 2", "CONF:MANSTAll 3", "CONF:MANSTAll 4", "CONF:MANSTAll 5", "CONF:MANSTAll 6" };
        public static string[] Measure = { "MEAS:VAL?", "MEAS:FREQ?"};
        public static string[] CaliSwitch = { "CAL:SWIT ON", "CAL:SWIT OFF" };
        public static string[] CalibrationSet = { "CAL:VACV ", "CAL:VACF ", "CAL:VDCP ", "CAL:VDCN ", "CAL:IACI", "CAL:IACF ", "CAL:IDCP", "CAL:IDCN" };
        //因为1、2档电流较大，高压表作保护用，取默认值即可，这里不用校准
        public static string[] CalibrationGet = { "CAL:READ:VACV?#", "CAL:READ:VACF?#", "CAL:READ:VDC P?#", "CAL:READ:VDC N?#", 
                                                  "CAL:READ:IACI 3?#", "CAL:READ:IACI 4?#", 
                                                  "CAL:READ:IACI 5?#", "CAL:READ:IACI 6?#", "CAL:READ:IACI 7?#", "CAL:READ:IACI 8?#", "CAL:READ:IACF?#", 
                                                  "CAL:READ:IDCP 3?#", "CAL:READ:IDCP 4?#", 
                                                  "CAL:READ:IDCP 5?#", "CAL:READ:IDCP 6?#", "CAL:READ:IDCP 7?#", "CAL:READ:IDCP 8?#", 
                                                  "CAL:READ:IDCN 3?#", "CAL:READ:IDCN 4?#",
                                                  "CAL:READ:IDCN 5?#", "CAL:READ:IDCN 6?#", "CAL:READ:IDCN 7?#", "CAL:READ:IDCN 8?#" 
                                                };
        public enum cmdCommunication { QUERY, SADD, REMOTE, LOCAL };
        public enum cmdConfigure { QUERY, VDC, VAC, IDC, IAC, STALL1, STALL2, STALL3, STALL4, STALL5, STALL6 };
        public enum cmdSwitchStall { STALLAUTO, STALL1, STALL2, STALL3, STALL4, STALL5, STALL6 };

        public enum cmdMeasure { VALUE, FREQ };
        public enum cmdCaliSwitch { ON,OFF};
        public enum cmdCalibrationSet { VACV, VACF, VDCP, VDCN, IACI, IACF, IDCP, IDCN };
        public enum cmdCalibrationGet { VACV, VACF, VDCP, VDCN,  
                                        IACI3, IACI4, IACI5, IACI6, IACI7, IACI8,IACF,
                                        IDCP3, IDCP4, IDCP5, IDCP6, IDCP7, IDCP8, 
                                        IDCN3, IDCN4, IDCN5, IDCN6, IDCN7, IDCN8 
                                       };

    }
}
