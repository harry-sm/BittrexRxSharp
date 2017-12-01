using System;
using System.Collections.Generic;
using System.Text;

namespace BittrexRxSharp.Models
{
    public class MarketSummary: ApiResponseType
    {
        public String MarketName { get; set; }
        public Decimal High { get; set; }
        public Decimal Low { get; set; }
        public Decimal Volume { get; set; }
        public Decimal Last { get; set; }
        public Decimal BaseVolume { get; set; }
        public DateTime TimeStamp { get; set; }
        public Decimal Bid { get; set; }
        public Decimal Ask { get; set; }
        public int OpenBuyOrders { get; set; }
        public int OpenSellOrders { get; set; }
        public Decimal PrevDay { get; set; }
        public DateTime Created { get; set; }
    }
}
