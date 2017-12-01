using System;
using System.Collections.Generic;
using System.Text;

namespace BittrexRxSharp.ValueTypes
{
    public enum TimeInEffectValue
    {
        IMMEDIATE_OR_CANCEL,
        GOOD_TIL_CANCELLED,
        FILL_OR_KILL
    }
}
