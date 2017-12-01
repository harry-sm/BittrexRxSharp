using System;
using System.Collections.Generic;
using System.Text;

namespace BittrexRxSharp.Models
{
    public class WithdrawalTransaction: ApiResponseType
    {
        string PaymentUuid { get; set; }
        string Currency { get; set; }
        Decimal Amount { get; set; }
        string Address { get; set; }
        DateTime Opened { get; set; }
        Boolean Authorized { get; set; }
        Boolean PendingPayment { get; set; }
        Decimal TxCost { get; set; }
        string TxId { get; set; }
        Boolean Canceled { get; set; }
        Boolean InvalidAddress { get; set; }
    }
}
