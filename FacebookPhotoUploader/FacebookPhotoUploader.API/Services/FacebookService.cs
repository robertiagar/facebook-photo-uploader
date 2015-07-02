using AsyncOAuth;
using Facebook;
using FacebookPhotoUploader.API.Interfaces;
using FacebookPhotoUploader.API.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;

namespace FacebookPhotoUploader.API.Services
{
    public class FacebookService : IFacebookService
    {
        const string appId = "1656516901244654";
        const string ProductId = "2f567fdc7cd24adfa9464dc3eed894e1";
        const string appSecret = "e23f848ba6cb89deaa421c99a4d7a664";
        const string graphApi = "https://graph.facebook.com/v2.3/";
        FacebookClient client;

        private SettingsService settingService;

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

            client = new FacebookClient();
        }

        public async Task<bool> Login()
        {
            string accessToken = await settingService.GetFromStoreAsync("access_token");
            if (string.IsNullOrEmpty(accessToken))
            {
                var uri = string.Format(@"fbconnect://authorize?client_id={0}&scope=user_photos,publish_actions&redirect_uri=msft-{1}://authorize", appId, ProductId);

                var result = await Launcher.LaunchUriAsync(new Uri(uri));

                return result;
            }

            else
            {
                client.AccessToken = accessToken;
                client.AppId = appId;
                client.AppSecret = appSecret;
                client.Version = "v2.3";
                return true;
            }
        }

        public async Task<dynamic> GetAsync()
        {
            var obj = await client.GetTaskAsync("/me");
            return obj;
        }

        public Task<bool> Logout()
        {
            return settingService.AddToStoreAsync(new KeyValuePair<string, string>("access_token", ""));
        }

        public async Task UploadFotoAsync(IStorageFile file, Action<UploadOperation> progressAction, Photo photo, CancellationToken cancellationToken, IProgress<FacebookUploadProgressChangedEventArgs> progress)
        {
            var image = new FacebookMediaObject();
            image.ContentType = "image/jpg";
            image.FileName = file.Name;
            byte[] fileBytes = await GetFileBytes(file);
            image.SetValue(fileBytes);

            var postInfo = new Dictionary<string, object>();
            postInfo.Add("message", photo.Caption);
            postInfo.Add("image", image);


            var fbResult = await client.PostTaskAsync("/photos", postInfo, null, cancellationToken, progress);
        }

        private static async Task<byte[]> GetFileBytes(IStorageFile file)
        {
            byte[] fileBytes = null;

            using (IRandomAccessStreamWithContentType stream = await file.OpenReadAsync())
            {
                fileBytes = new byte[stream.Size];
                using (DataReader reader = new DataReader(stream))
                {
                    await reader.LoadAsync((uint)stream.Size);
                    reader.ReadBytes(fileBytes);
                }
            }
            return fileBytes;
        }

        public Task<IEnumerable<Album>> GetAlbumsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateAlbumAsync(Album album)
        {
            throw new NotImplementedException();
        }

        public void SetSettingsService(SettingsService settingsService)
        {
            this.settingService = settingsService;
        }

        public async void Report(FacebookUploadProgressChangedEventArgs value)
        {
            var dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {

            });
        }
    }
}
