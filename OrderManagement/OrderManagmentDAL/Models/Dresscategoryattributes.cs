using System;
using System.Collections.Generic;

namespace OrderManagmentDAL.Models
{
    public partial class Dresscategoryattributes
    {
        public int Measurementid { get; set; }
        public int? Dresscategoryid { get; set; }
        public int? Sizemin { get; set; }
        public int? Sizemax { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? Creationdatetime { get; set; }
        public string Attributename { get; set; }
    }
}
