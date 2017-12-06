using System;
using System.Collections.Generic;
using System.Text;

namespace BittrexRxSharp.Models
{
    public class Transaction : ApiResponseType
    {
        public int Id { get; set; }
        public Decimal Amount { get; set; }
        public String Currency { get; set; }
        public int Confirmations { get; set; }
        public DateTime LastUpdated { get; set; }
        public String TxId { get; set; }
        public String CryptoAddress { get; set; }
    }
}

