using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCalibrationSystem
{
    class InstrumentsState
    {

        public EnumInstrumentState CS9010State;
        public EnumInstrumentState Agilent34410AState;
        public EnumInstrumentState Agilent34410AState_2;
        public EnumInstrumentState CS9920AState;
        public EnumInstrumentState CS9920BState;
        public EnumInstrumentState Fluke5520AState;
        public InstrumentsState()
        {
            CS9010State = EnumInstrumentState.INIT;
            Agilent34410AState = EnumInstrumentState.INIT;
            Agilent34410AState_2 = EnumInstrumentState.INIT;
            CS9920BState = EnumInstrumentState.INIT;
            Fluke5520AState = EnumInstrumentState.INIT;
        }
    }
}
