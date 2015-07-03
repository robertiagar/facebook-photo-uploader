using FacebookPhotoUploader.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace FacebookPhotoUploader.API.Services
{
    public class SettingsService : ISettingsService
    {
        public bool SaveSetting(KeyValuePair<string, string> keyValuePair)
        {
            try
            {
                var localSettings = ApplicationData.Current.LocalSettings;
                localSettings.Values[keyValuePair.Key] = keyValuePair.Value;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetSetting(string key)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            var result = localSettings.Values[key];
            if (result != null)
            {
                return result.ToString();
            }

            return null;
        }


        public bool SaveSetting(string key, string value)
        {
            return SaveSetting(new KeyValuePair<string, string>(key, value));
        }
    }
}
