using System;
using BittrexRxSharp.ValueTypes;

namespace BittrexRxSharp.Models
{
    public class OrderBookOrderItem: ApiResponseType
    {
        public Decimal Quantity { get; set; }
        public Decimal Rate { get; set; }
    }

    public class OrderBook: ApiResponseType
    {
        public OrderBookOrderItem[] Buy;
        public OrderBookOrderItem[] Sell;
    }
}
