using ProgresssDialog.Wpf.Sample.Model.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgresssDialog.Wpf.Sample.Command
{
    internal class ProgressDialogExecuteCommand
    {
        public void Execute()
        {
            var task = new HeavyTask();
            var progWindow = new ProgresssDialog.Wpf.ProgressWindow();
            progWindow.Start(task);

            return;
        }
    }
}
