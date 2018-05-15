using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCalibrationSystem
{
    public enum EnumInstrumentState
    {
        INIT, 
        SADD, 
        REMOTE, 
        LOCAL, 
        QUERYREMOTE,
        MODEABLE,
        STALLABLE,
        READCALIDATA,
        SENDCALIDATA,
    }
}
