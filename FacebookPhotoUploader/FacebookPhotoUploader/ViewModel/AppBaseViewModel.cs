using FacebookPhotoUploader.API.Interfaces;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace FacebookPhotoUploader.ViewModel
{
    public class AppBaseViewModel : ViewModelBase
    {
        private IStatusService statusService;
        public string ApplicationName { get; set; }
        public virtual string PageName { get; set; }
        public ResourceLoader ResourceLoader { get; set; }

        public AppBaseViewModel(IStatusService statusService)
        {
            this.ResourceLoader = ResourceLoader.GetForCurrentView("Resources");
            this.statusService = statusService;

            ApplicationName = ResourceLoader.GetString("ApplicationName");
        }
    }
}
