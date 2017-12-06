using Newtonsoft.Json;
using System;

namespace BittrexRxSharp.Models
{
    public class OrderResult: ApiResponseType
    {
        public Guid Uuid { get; set; }
    }
}
