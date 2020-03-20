using System;
using System.Collections.Generic;
using System.Text;

namespace CeloTest.Service.Models
{
    public class CommonResult<T>
    {
        public bool Success { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
