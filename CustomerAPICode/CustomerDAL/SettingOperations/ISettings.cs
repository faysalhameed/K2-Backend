using CustomerBO.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerDAL.SettingOperations
{
    interface ISettings
    {
        List<SettingListBO> getEmailSettings();
    }
}
