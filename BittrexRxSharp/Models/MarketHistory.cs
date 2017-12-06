using System;
using System.Collections.Generic;
using System.Text;

namespace BittrexRxSharp.Models
{
    public enum FillType
    {
        FILL,
        PARTIAL_FILL
    }
    public enum OrderType
    {
        BUY,
        SELL
    }
    public class MarketHistory: ApiResponseType
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public Decimal Quantity { get; set; }
        public Decimal Price { get; set; }
        public Decimal Total { get; set; }
        public String FillType { get; set; }
        public String OrderType { get; set; }
    }
}
