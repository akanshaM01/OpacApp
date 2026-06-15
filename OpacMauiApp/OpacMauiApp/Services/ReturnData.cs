using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace OpacMauiApp.Services
{
    public class ReturnData<T>
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode code { get; set; } = HttpStatusCode.OK;
        public bool InternalSuccess { get; set; }
        public bool IsTechError { get; set; }
        public string? Mesg { get; set; }
        public T? Data { get; set; }
    }
}
