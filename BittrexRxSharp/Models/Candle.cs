using System;
using System.Collections.Generic;
using System.Text;

namespace BittrexRxSharp.Models
{
    public class Candle: ApiResponseType
    {
        public Decimal O { get; set; } // open
        public Decimal H { get; set; } // high
        public Decimal L { get; set; } // low
        public Decimal C { get; set; } // close
        public Decimal V { get; set; } // volume
        public DateTime T { get; set; } // time
        public Decimal BV { get; set; } // bitcoin value
    }
}
