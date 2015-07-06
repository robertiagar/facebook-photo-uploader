using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace FacebookPhotoUploader.BackgroundTasks
{
    public sealed class UploadPhotosBackgroundTask: IBackgroundTask
    {
        private BackgroundTaskDeferral _defferal; BackgroundTaskCancellationReason _cancelReason = BackgroundTaskCancellationReason.Abort;
        volatile bool _cancelRequested = false;
        BackgroundTaskDeferral _deferral = null;
        uint _progress = 0;

        IBackgroundTaskInstance _taskInstance = null;

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();
            _taskInstance = taskInstance;

            await DoWork();

            _deferral.Complete();
        }

        private async Task DoWork()
        {
            await Task.Delay(500);
        }
    }
}
