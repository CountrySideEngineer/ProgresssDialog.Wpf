using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemTask = System.Threading.Tasks.Task;

namespace ProgresssDialog.Wpf.Task
{
    public class AsyncTask<T> : IAsyncTask<T>
    {
        protected IProgress<T>? _progress = null;

        public Action<IProgress<T>>? Action { get; set; } = null;

        public AsyncTask()
        {
            _progress = null;
            Action = null;
        }

        public virtual void Run(IProgress<T> progress)
        {
            if ((null == _progress) || (null == Action))
            {
                throw new NullReferenceException();
            }
            SystemTask _ = RunAsync(progress);
        }

        protected virtual async SystemTask RunAsync(IProgress<T> progress)
        {
            _progress = progress;
            SystemTask task = SystemTask.Run(() =>
            {
                Action?.Invoke(progress);
            });
            await task;
        }

    }
}
