using System;
using System.Collections.Generic;
using System.Text;

namespace BittrexRxSharp.Models
{
    public class Ticker: ApiResponseType
    {
        public Decimal Bid { get; set; }
        public Decimal Ask { get; set; }
        public Decimal Last { get; set; }
    }
}
