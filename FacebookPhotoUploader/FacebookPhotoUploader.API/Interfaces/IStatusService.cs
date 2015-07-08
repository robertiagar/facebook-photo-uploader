using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookPhotoUploader.API.Interfaces
{
    public interface IStatusService
    {
        void DisplayMessage(string message);
        void DisplayMessage(string message, bool displayProgressBar = false);
        Task DisplayMessage(string message, int delay);
        Task DisplayMessage(string messsage, int delay, bool displayProgressBar = true);
        void DisplayProgress(double? progress, string message = null);
        Task HideProgressAsync();
        Task ShowProgressAsync();
    }
}
