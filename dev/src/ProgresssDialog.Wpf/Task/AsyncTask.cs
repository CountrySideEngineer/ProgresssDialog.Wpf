using System;
using System.Runtime.CompilerServices;
using SystemTask = System.Threading.Tasks.Task;

namespace ProgresssDialog.Wpf.Task
{
    /// <summary>
    /// Represents an asynchronous task that executes an action with progress reporting.
    /// </summary>
    /// <typeparam name="T">The type of progress update value.</typeparam>
    public class AsyncTask<T> : IAsyncTask<T>
    {
        /// <summary>
        /// Gets or sets the action to be executed asynchronously with progress reporting.
        /// </summary>
        public Action<IProgress<T>>? Action { get; set; } = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncTask{T}"/> class.
        /// </summary>
        public AsyncTask() { }

        /// <summary>
        /// Runs the specified action asynchronously, providing progress updates.
        /// </summary>
        /// <param name="progress">The progress reporter to be used by the action.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the <see cref="Action"/> property is not set.
        /// </exception>
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
