using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCalibrationSystem
{
    public class CommandCS9920
    {
        public static string[] Communication = { "COMM:CONT?", "COMM:SADD ", "COMM:REM", "COMM:LOC" };
        public static string[] SourceControl = { "SOUR:TEST:STAR", "SOUR:TEST:STOP","SOUR:TEST:STAT" };
        public static string[] SetVoltage = { "STEP:DCW:VOLTage ", "STEP:ACW:VOLTage " };
        public static string[] SetTime = { "STEP:DCW:TTIM 000.0", "STEP:ACW:TTIM 000.0" };
        public enum cmdType { COMM,SOURCECTRL,SETVOLT,SETTIME};
        public enum cmdCommunication { QUERY, SADD, REMOTE, LOCAL };
        public enum cmdSourceControl { STAR, STOP, STAT };
        public enum cmdSetVoltage { DC, AC };
        public enum cmdSetTime { DC, AC };
    
    }
}
