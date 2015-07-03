using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers.Provider;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace FacebookPhotoUploader
{
    public enum PhotoCaptionResult
    {
        CaptionOk,
        NoCaption,
        Canceled
    }

    public sealed partial class PhotoCaption : ContentDialog
    {
        public PhotoCaptionResult Result { get; private set; }
        public string Caption { get; private set; }

        public PhotoCaption()
        {
            this.InitializeComponent();
            this.Closing += PhotoCaption_Closing;
        }

        public async Task SetImageAsync(IStorageFile file)
        {
            using (IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
            {
                // Set the image source to the selected bitmap 
                BitmapImage bitmapImage = new BitmapImage();
                await bitmapImage.SetSourceAsync(fileStream);
                this.image.Source = bitmapImage;
            }
        }

        void PhotoCaption_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            if (Result == PhotoCaptionResult.CaptionOk)
            {
                this.Caption = caption.Text;
            }
            else
            {
                this.Caption = string.Empty;
            }
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (!string.IsNullOrEmpty(caption.Text))
            {
                this.Result = PhotoCaptionResult.CaptionOk;
            }
            else
            {
                this.Result = PhotoCaptionResult.NoCaption;
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.Result = PhotoCaptionResult.Canceled;
        }
    }
}
