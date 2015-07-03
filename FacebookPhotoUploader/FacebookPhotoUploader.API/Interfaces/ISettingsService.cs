using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookPhotoUploader.API.Interfaces
{
    public interface ISettingsService
    {
        bool SaveSetting(KeyValuePair<string, string> keyValuePair);
        bool SaveSetting(string key, string value);
        string GetSetting(string key);
    }
}
