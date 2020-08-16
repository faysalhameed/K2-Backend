using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBO.Settings
{
    public class SettingBO
    {
        public string FromEmailAddress { get; set; }
        public int SMTPPort { get; set; }
        public string SMTPHost { get; set; }
        public bool SMTPEnableSSL { get; set; }
        public bool SMTPUseDefaultCredential { get; set; }
        public string FromEmailAddressPassword { get; set; }
    }
}
