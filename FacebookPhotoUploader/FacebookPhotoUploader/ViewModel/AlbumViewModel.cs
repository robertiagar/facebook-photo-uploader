using FacebookPhotoUploader.API.Interfaces;
using FacebookPhotoUploader.API.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage.Pickers;

namespace FacebookPhotoUploader.ViewModel
{
    public class AlbumViewModel : AppBaseViewModel, IProgress<Facebook.FacebookUploadProgressChangedEventArgs>
    {
        private Album _album;
        private IFacebookService faceboookService;
        private CancellationTokenSource cts;

        public Album Album
        {
            get
            { return _album; }
            set
            {
                Set<Album>(() => this.Album, ref _album, value);
            }
        }

        public ICommand AddPhotoCommand { get; private set; }

        public AlbumViewModel(IFacebookService facebookService, IStatusService statusService)
            :base(statusService)
        {
            this.faceboookService = facebookService;
            this.cts = new CancellationTokenSource();
            this.AddPhotoCommand = new RelayCommand(() =>
            {
                FileOpenPicker opener = new FileOpenPicker();
                opener.ViewMode = PickerViewMode.Thumbnail;
                opener.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
                opener.FileTypeFilter.Add(".jpg");
                opener.FileTypeFilter.Add(".jpeg");
                opener.FileTypeFilter.Add(".png");

                opener.PickSingleFileAndContinue();
            });

            this.PageName = ResourceLoader.GetString("PhotosPageName");

            if (this.IsInDesignMode)
            {
                this.ApplicationName = "Robert's Photo Uploader";
                this.PageName = "PHOTOS";

                Album = new Album();
                Album.AddPhoto(new Photo()
                {
                    Id="43jk2h42kj3h4",
                    Caption="photo 1",
                    ImageLink="Assets/LighGray.png"
                });
                Album.Name = "design album";
            }
        }

        public async Task GetAlbum(string albumId)
        {
            var album = await faceboookService.GetAlbumAsync(albumId);
            Album = album;
        }

        internal async Task UploadPhotoAsync(Windows.Storage.StorageFile file)
        {
            var dialog = new PhotoCaption();
            await dialog.SetImageAsync(file);
            await dialog.ShowAsync();

            if (dialog.Result == PhotoCaptionResult.CaptionOk)
            {
                var photo = new Photo()
                {
                    Caption = dialog.Caption
                };
                await faceboookService.UploadPhotoAsync(file, photo, cts.Token, this);
            }
        }

        public override string PageName
        {
            get { return Album.Name.ToUpper(); }
        }

        public async void Report(Facebook.FacebookUploadProgressChangedEventArgs value)
        {
            //await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            //{
            //    statusBar.ProgressIndicator.ProgressValue = (float)value.ProgressPercentage / 100;
            //    if (value.ProgressPercentage == 100)
            //    {
            //        await statusBar.ProgressIndicator.HideAsync();
            //    }
            //});
        }

        private void Cancel() { cts.Cancel(); }
    }
}
