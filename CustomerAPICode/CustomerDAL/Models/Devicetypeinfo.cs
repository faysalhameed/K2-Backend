using System;
using System.Collections.Generic;

namespace CustomerDAL.Models
{
    public partial class Devicetypeinfo
    {
        public int Devicetypeid { get; set; }
        public string Devicetypename { get; set; }
        public bool? Isactive { get; set; }
    }
}
