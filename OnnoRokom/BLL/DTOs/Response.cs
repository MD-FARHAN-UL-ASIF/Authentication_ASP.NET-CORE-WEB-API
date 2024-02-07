using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class Response
    {
        public Response(HttpStatusCode status, string message, object result) 
        {
            Status = status.ToString();
            Message = message;
            Result = result;
        }

        public Response(HttpStatusCode status, string message)
        {
            Status = status.ToString();
            Message = message;
            Result = string.Empty;
        }

        public string Status { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }
    }
}
