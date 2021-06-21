using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Envision.WebApi.Common
{
    public class Response<T>
    {
        public int StatusCode { get; set; } = (int)HttpStatusCode.OK;
        public string Message { get; set; } = "Success";
        public T Result { get; set; }
    }
}
