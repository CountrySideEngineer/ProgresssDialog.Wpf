using ProgresssDialog.Wpf.Sample.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgresssDialog.Wpf.Sample.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        protected DelegateCommand? _executeAsyncCommand;
        public DelegateCommand ExecuteAsyncCommand
        {
            get
            {
                if (null == _executeAsyncCommand)
                {
                    _executeAsyncCommand = new DelegateCommand(this.ExecuteAsyncCommandExecute);
                }
                return _executeAsyncCommand;
            }
        }

        public void ExecuteAsyncCommandExecute()
        {
            var command = new ProgressDialogExecuteCommand();
            command.Execute(200, 300);
        }
    }
}
