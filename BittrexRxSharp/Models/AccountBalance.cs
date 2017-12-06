using System;
using System.Collections.Generic;
using System.Text;

namespace BittrexRxSharp.Models
{
    public class AccountBalance: ApiResponseType
    {
        public String Currency { get; set; }
        public Decimal Balance { get; set; }
        public Decimal Available { get; set; }
        public Decimal Pending { get; set; }
        public String CryptoAddress { get; set; }
        public bool Requested { get; set; }
        public String Uuid { get; set; }
    }
}
