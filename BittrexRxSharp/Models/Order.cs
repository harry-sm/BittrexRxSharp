using System;
using System.Collections.Generic;
using System.Text;

namespace BittrexRxSharp.Models
{
    public class Order: ApiResponseType
    {
        public string AccountId { get; set; }
        public string OrderUuid { get; set; } 
        public string Exchange { get; set; }
        public string Type { get; set; }
        public Decimal Quantity { get; set; }
        public Decimal QuantityRemaining { get; set; }
        public Decimal Limit { get; set; }
        public Decimal Reserved { get; set; }
        public Decimal ReserveRemaining { get; set; }
        public Decimal CommissionReserved { get; set; }
        public Decimal CommissionReserveRemaining { get; set; }
        public Decimal CommissionPaid { get; set; }
        public Decimal Price { get; set; }
        public Decimal PricePerUnit { get; set; }
        public DateTime Opened { get; set; }
        public DateTime Closed { get; set; }
        public Boolean IsOpen { get; set; } 
        public string Sentinel { get; set; } 
        public Boolean CancelInitiated { get; set; } 
        public Boolean ImmediateOrCancel { get; set; }
        public Boolean IsConditional { get; set; } 
        public string Condition { get; set; }
        public dynamic ConditionTarget { get; set; }
    }
}
