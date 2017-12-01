using System;
using System.Collections.Generic;
using System.Text;

namespace BittrexRxSharp.Models
{
    public class CryptoCurrency: ApiResponseType
    {
        public String Currency { get; set; }
        public String CurrencyLong { get; set; }
        public int MinConfirmation { get; set; }
        public Decimal TxFee { get; set; }
        public bool IsActive { get; set; }
        public String CoinType { get; set; }
        public String BaseAddress { get; set; }
        public String Notice { get; set; }
    }
}

