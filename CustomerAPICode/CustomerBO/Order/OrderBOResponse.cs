using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBO.Order
{
    public class OrderBOResponse
    {
        public bool issuccessfull { get; set; }
        public string OrderNumber { get; set; }
        public string responsemessage { get; set; }
    }
}
