using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BittrexRxSharp.Models
{
    public class Market: ApiResponseType
    {
        public String MarketCurrency { get; set; }
        public String MarketCurrencyLong { get; set; }
        public String BaseCurrency { get; set; }
        public String BaseCurrencyLong { get; set; }
        public Decimal MinTradeSize { get; set; }
        public String MarketName { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public dynamic Notice { get; set; }
        public dynamic IsSponsored { get; set; }
        public String LogoUrl { get; set; }
    }
}
