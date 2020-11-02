using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagmentBO.Order
{
    public class Orderproduct
    {
        public string preorderproductid { get; set; }
        public string productid { get; set; }
        public string dresscategoryid { get; set; }
        public string estimatedcost { get; set; }
        public string productsource { get; set; }
        public string productsourceid { get; set; }
        public string tailorstitchcost { get; set; }
        public string isproductimagesavailable { get; set; }
        public string isproductaddonavailable { get; set; }
        public string isproductmeasurementavailable { get; set; }

        public List<Orderproductimage> preorderproductimages { get; set; }
        public List<Orderproductaddon> preorderproductaddons { get; set; }
        public List<Orderproductmeasurement> preorderproductmeasurements { get; set; }
    }
}
