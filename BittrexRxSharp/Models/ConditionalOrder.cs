using System;
using System.Collections.Generic;
using System.Text;

namespace BittrexRxSharp.Models
{
    public class ConditionalOrder: ApiResponseType
    {
        public string OrderId { get; set; }
        public string MarketName { get; set; }
        public string MarketCurrency { get; set; }
        public string BuyOrSell { get; set; }
        public string OrderType { get; set; }
        public Decimal Quantity { get; set; }
        public Decimal Rate { get; set; }
    } 
}
