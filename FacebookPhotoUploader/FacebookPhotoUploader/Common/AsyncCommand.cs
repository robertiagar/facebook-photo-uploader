using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FacebookPhotoUploader.Common
{
    public class AsyncCommand : ICommand
    {
        private readonly Func<Task> asyncAction;
        private bool isRunning = false;

        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                isRunning = value;
                if (CanExecuteChanged != null)
                {
                    CanExecuteChanged(this, EventArgs.Empty);
                }
            }
        }

        public AsyncCommand(Func<Task> asyncAction)
        {
            this.asyncAction = asyncAction;
        }

        public bool CanExecute(object parameter)
        {
            return !isRunning;
        }

        public event EventHandler CanExecuteChanged;

        public async void Execute(object parameter)
        {
            IsRunning = true;
            await asyncAction();
            IsRunning = false;
        }
    }
}
