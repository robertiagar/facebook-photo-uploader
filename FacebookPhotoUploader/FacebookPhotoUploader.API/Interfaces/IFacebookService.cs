using FacebookPhotoUploader.API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;

namespace FacebookPhotoUploader.API.Interfaces
{
    public interface IFacebookService
    {
        Task<bool> Login();
        Task<bool> Logout();
        Task UploadFotoAsync(IStorageFile file, Action<UploadOperation> progressAction, Photo photo, CancellationToken cancellationToken, IProgress<Facebook.FacebookUploadProgressChangedEventArgs> progress);
        Task<IEnumerable<Album>> GetAlbumsAsync();
        Task<bool> CreateAlbumAsync(Album album);
    }
}
