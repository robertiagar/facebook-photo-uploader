using FacebookPhotoUploader.API.Interfaces;
using FacebookPhotoUploader.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.System;

namespace FacebookPhotoUploader.API.Services
{
    public class FacebookService : IFacebookService
    {
        const string AppId = "1656516901244654";
        const string ProductId = "2f567fdc7cd24adfa9464dc3eed894e1";

        public FacebookService()
        {
            AsyncOAuth.OAuthUtility.ComputeHash = (key, buffer) =>
            {
                var crypt = Windows.Security.Cryptography.Core.MacAlgorithmProvider.OpenAlgorithm("HMAC_SHA1");

                var keyBuffer = Windows.Security.Cryptography.CryptographicBuffer.CreateFromByteArray(key);

                var cryptKey = crypt.CreateKey(keyBuffer);


                var dataBuffer = Windows.Security.Cryptography.CryptographicBuffer.CreateFromByteArray(buffer);

                var signBuffer = Windows.Security.Cryptography.Core.CryptographicEngine.Sign(cryptKey, dataBuffer);

                byte[] value;

                Windows.Security.Cryptography.CryptographicBuffer.CopyToByteArray(signBuffer, out value);

                return value;
            };
        }

        public Task<bool> Login()
        {
            var uri = string.Format(@"fbconnect://authorize?client_id={0}&scope=user_photos&redirect_uri=msft-{1}://authorize", AppId, ProductId);

            return Launcher.LaunchUriAsync(new Uri(uri)).AsTask();
        }

        public Task<bool> Logout()
        {
            throw new NotImplementedException();
        }

        public Task UploadFotoAsync(IStorageFile file, Action<UploadOperation> progressAction, Photo photo)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Album>> GetAlbumsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateAlbumAsync(Album album)
        {
            throw new NotImplementedException();
        }
    }
}
