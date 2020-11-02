using System;
using System.Collections.Generic;

namespace OrderManagmentDAL.Models
{
    public partial class Dresscategories
    {
        public int Dresscategoryid { get; set; }
        public string Categoryname { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? Creationdatetime { get; set; }
        public string Gender { get; set; }
    }
}
