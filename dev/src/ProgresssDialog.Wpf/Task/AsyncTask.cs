using System;
using System.Runtime.CompilerServices;
using SystemTask = System.Threading.Tasks.Task;

namespace ProgresssDialog.Wpf.Task
{
    public class AsyncTask<T> : IAsyncTask<T>
    {
        public Action<IProgress<T>>? Action { get; set; } = null;

        public AsyncTask() { }

        public virtual async SystemTask RunAsync(IProgress<T> progress)
        {
            if (null == Action)
            {
                throw new InvalidOperationException(Properties.Resources.IDS_ERR_MSG_ACTION_NOT_SET);
            }

            await SystemTask.Run(() => Action(progress));
        }
    }
}
