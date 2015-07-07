using Facebook;
using FacebookPhotoUploader.API.Interfaces;
using FacebookPhotoUploader.API.Models;
using System;
using System.IO;
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
        FacebookClient client;

        private ISettingsService settingsService;

        public FacebookService(ISettingsService settingsService)
        {
            client = new FacebookClient();
            this.settingsService = settingsService;
        }

        public async Task LoginAsync()
        {
            string accessToken = settingsService.GetSetting("access_token");
            string userId = settingsService.GetSetting("id");
            if (string.IsNullOrEmpty(accessToken))
            {
                var uri = string.Format(@"fbconnect://authorize?client_id={0}&scope=user_photos,publish_actions&redirect_uri=msft-{1}://authorize", appId, ProductId);

                var result = await Launcher.LaunchUriAsync(new Uri(uri));
            }

            else
            {
                client.AccessToken = accessToken;
                client.AppId = appId;
                client.AppSecret = appSecret;
                client.Version = "v2.3";
                userId = await GetUserIdAsync();
                SaveUserI(userId);
            }
        }

        public bool SaveUserI(string userId)
        {
            return settingsService.SaveSetting(new KeyValuePair<string, string>("id", userId));
        }

        public async Task<string> GetUserIdAsync()
        {
            dynamic result = await client.GetTaskAsync("/me");
            if (result != null)
            {
                if (result.id != null)
                {
                    return result.id.ToString();
                }
            }
            return string.Empty;
        }

        public async Task<bool> LogoutAsync()
        {
            var userdId = settingsService.GetSetting("id");
            var access_token = settingsService.GetSetting("access_token");
            if (userdId != null)
            {
                var url = string.Format("/{0}/permissions", userdId);
                dynamic result = await client.DeleteTaskAsync(url);

                if (result.success != null)
                {
                    var success = (bool)result.success;
                    if (success)
                    {
                        settingsService.SaveSetting("id", "");
                        settingsService.SaveSetting("access_token", "");
                        return true;
                    }
                }
            }

            return false;
        }

        public async Task UploadPhotoAsync(IStorageFile file, Photo photo, CancellationToken cancellationToken, IProgress<FacebookUploadProgressChangedEventArgs> progress)
        {
            var str = await file.OpenReadAsync();
            var stream = str.AsStream();

            using (var imageStream = new FacebookMediaStream()
            {
                ContentType = "image/jpeg",
                FileName = file.Name
            }.SetValue(stream))
            {

                var postInfo = new Dictionary<string, object>();
                postInfo.Add("message", photo.Caption);
                postInfo.Add("file", imageStream);

                var fbResult = await client.PostTaskAsync("/photos", postInfo, null, cancellationToken, progress);
            }
        }

        public async Task<IEnumerable<Album>> GetAlbumsAsync()
        {
            var url = string.Format("/{0}/albums", settingsService.GetSetting("id"));
            var result = new List<Album>();
            dynamic albums = await client.GetTaskAsync(url);
            if (albums != null)
            {
                if (albums.data != null)
                {
                    foreach (dynamic album in albums.data)
                    {
                        var alb = new Album(album);
                        if (album.cover_photo != null)
                        {
                            var imageResult = await client.GetTaskAsync(album.cover_photo.ToString());
                            if (imageResult.images != null)
                            {
                                var images = imageResult.images as List<dynamic>;
                                dynamic image = images.Where(i => i.height == 225).SingleOrDefault();
                                if (image != null)
                                {
                                    alb.CoverPhotoLink = image.source;
                                }
                                else
                                {
                                    alb.CoverPhotoLink = "Assets/LightGray.png";
                                }
                            }
                        }

                        result.Add(alb);
                    }
                }
            }

            return result;
        }

        public async Task<Album> GetAlbumAsync(string albumId)
        {
            var url = string.Format("/{0}", albumId);
            dynamic result = await client.GetTaskAsync(url);
            var album = new Album(result);

            result = await client.GetTaskAsync(url + "/photos");

            if (result.data != null)
            {
                foreach (dynamic image in result.data)
                {
                    var photo = new Photo(image);
                    album.AddPhoto(photo);
                }
            }
            return album;
        }

        public Task<bool> CreateAlbumAsync(Album album)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsLoggedInAsync()
        {
            string accessToken = settingsService.GetSetting("access_token");
            string userId = settingsService.GetSetting("id");
            if (string.IsNullOrEmpty(accessToken))
            {
                return false;
            }
            else
            {
                client.AccessToken = accessToken;
                client.AppId = appId;
                client.AppSecret = appSecret;
                client.Version = "v2.3";
                userId = await GetUserIdAsync();
                SaveUserI(userId);
                return true;
            }
        }
    }
}
