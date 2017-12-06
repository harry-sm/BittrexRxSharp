using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BittrexRxSharp.Models;

namespace BittrexRxSharp.Models
{
    public class SocketResponse<T>
    {
        public SocketResponse(String h, String m, T a)
        {
            H = h;
            M = m;
            A = a;
        }
        [JsonProperty(PropertyName = "H")]
        public String H { get; set; }
        [JsonProperty(PropertyName = "M")]
        public String M { get; set; }
        [JsonProperty(PropertyName = "A")]
        public T A { get; set; }
        //public SummarySate[] A { get; set; }
    }

    public class SummarySate
    {
        public int Nounce { get; set; }
        public MarketSummary[] Deltas { get; set; }
    }

    public class OrderBookStream
    {
        public string MarketName { get; set; }
        public long Nounce { get; set; }
        public OrderBookOrderStream[] Buys { get; set; }
        public OrderBookOrderStream[] Sells { get; set; }
        public OrderBookOrderFillsStream[] Fills { get; set; }
    }

    public class OrderBookOrderStream
    {
        public Decimal OrderType { get; set; }
        public Decimal Quantity { get; set; }
        public Decimal Rate { get; set; }
    }
    public class OrderBookOrderFillsStream
    {
        public string Type { get; set; }
        public Decimal Quantity { get; set; }
        public Decimal Rate { get; set; }
        public DateTime TimeStamp { get; set; } // 2017-08-15T19:44:40.89
    }
}
