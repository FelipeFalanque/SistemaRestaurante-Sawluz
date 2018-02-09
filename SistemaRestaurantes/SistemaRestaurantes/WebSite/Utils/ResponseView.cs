using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Utils
{
    public class ResponseView
    {
        public Status Status { get; set; }
        public object Result { get; set; }
    }

    public enum Status
    {
        NotFound = 0,
        Found = 1,
        OK = 2,
        NOK = 3
    }
}