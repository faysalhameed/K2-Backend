using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagmentBO.Order
{
    public class OrderDetailBO
    {
        public string preorderid { get; set; }
        public string customerid { get; set; }
        public string tailorid { get; set; }
        public string istailoravailable { get; set; }
        public string status { get; set; }
        public string totalestimatedcost { get; set; }
        public string pickcountry { get; set; }
        public string pickprovince { get; set; }
        public string pickcity { get; set; }
        public string pickzipcode { get; set; }
        public string pickaddress { get; set; }
        public string ispickaddresscomplete { get; set; }
        public string dropcountry { get; set; }
        public string dropprovince { get; set; }
        public string dropcity { get; set; }
        public string dropzipcode { get; set; }
        public string dropaddress { get; set; }
        public string isdropaddresscomplete { get; set; }
        public string totalproductcount { get; set; }

        public string devicetype { get; set; }
        public string deviceid { get; set; }
        public string sessiontoken { get; set; }
        public List<Orderproduct> preorderproducts { get; set; }
    }
}
