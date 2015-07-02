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
        public Task<bool> AddToStoreAsync(KeyValuePair<string, string> keyValuePair)
        {
            try
            {
                var localSettings = ApplicationData.Current.LocalSettings;
                localSettings.Values[keyValuePair.Key] = keyValuePair.Value;
                return Task.FromResult<bool>(true);
            }
            catch
            {
                return Task.FromResult<bool>(false);
            }
        }

        public Task<string> GetFromStoreAsync(string key)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            var result = localSettings.Values[key];
            if (result != null)
            {
                return Task.FromResult<string>(result.ToString());
            }

            return Task.FromResult<string>(null);
        }
    }
}
