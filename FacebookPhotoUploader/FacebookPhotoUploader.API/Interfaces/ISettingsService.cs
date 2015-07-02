using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookPhotoUploader.API.Interfaces
{
    public interface ISettingsService
    {
        Task<bool> AddToStoreAsync(KeyValuePair<string, string> keyValuePair);
        Task<string> GetFromStoreAsync(string key);
    }
}
