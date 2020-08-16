using System;
using System.Collections.Generic;

namespace CustomerDAL.Models
{
    public partial class Usertypeinfo
    {
        public int Usertypeid { get; set; }
        public string Usertype { get; set; }
        public bool? Isactive { get; set; }
    }
}
