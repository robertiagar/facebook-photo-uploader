using FacebookPhotoUploader.API.Interfaces;
using FacebookPhotoUploader.API.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookPhotoUploader.ViewModel
{
    public class AlbumViewModel : ViewModelBase
    {
        private Album _album;
        private IFacebookService faceboookService;

        public Album Album
        {
            get
            { return _album; }
            set
            {
                Set<Album>(() => this.Album, ref _album, value);
            }
        }

        public AlbumViewModel(IFacebookService facebookService)
        {
            this.faceboookService = facebookService;
        }

        public async Task GetAlbum(string albumId)
        {
            var album = await faceboookService.GetAlbumAsync(albumId);
            Album = album;
        }
    }
}
