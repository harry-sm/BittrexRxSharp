using System;
using System.Collections.Generic;
using System.Text;

namespace BittrexRxSharp.Models
{
    public class DepositAddress: ApiResponseType
    {
        public String Currency { get; set; }
        public String Address { get; set; }
    }
}
