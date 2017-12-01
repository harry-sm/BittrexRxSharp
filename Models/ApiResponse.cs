using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BittrexRxSharp.Models
{
    public class ApiResponse<T>
    {
        public ApiResponse(bool success, String message, T result)
        {
            Success = success;
            Message = message;
            Result = result;
        }
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }
        [JsonProperty(PropertyName = "message")]
        public String Message { get; set; }
        [JsonProperty(PropertyName = "result")]
        public T Result { get; set; }
        //public dynamic[] Result { get; set; }
    }
}
