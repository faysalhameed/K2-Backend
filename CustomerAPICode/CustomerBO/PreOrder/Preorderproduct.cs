using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBO.PreOrder
{
    public class Preorderproduct
    {
        public string preorderproductid { get; set; }
        public string productid { get; set; }
        public string dresscategoryid { get; set; }
        public string estimatedcost { get; set; }
        public string productsource { get; set; }
        public string productsourceid { get; set; }
        public string tailorstitchcost { get; set; }
        public List<Preorderproductimage> preorderproductimages { get; set; }
        public List<Preorderproductaddon> preorderproductaddons { get; set; }
        public List<Preorderproductmeasurement> preorderproductmeasurements { get; set; }
    }
}
