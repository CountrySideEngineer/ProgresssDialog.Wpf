using ProgresssDialog.Wpf.Model;
using ProgresssDialog.Wpf.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgresssDialog.Wpf.Sample.Model.Task
{
    internal class HeavyTask : IAsyncTask<ProgressInfo>
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public HeavyTask() { }

        /// <summary>
        /// Run task.
        /// </summary>
        /// <param name="progress">Object to handle progress of task.</param>
        public System.Threading.Tasks.Task RunAsync(IProgress<ProgressInfo> progress)
        {
            System.Threading.Tasks.Task task = this.Run(progress);

            return task;
        }

        /// <summary>
        /// Run task async.
        /// </summary>
        /// <param name="progress">Object to handle progress of task.</param>
        /// <returns>Task running asynchronously.</returns>
        protected virtual async System.Threading.Tasks.Task Run(IProgress<ProgressInfo> progress)
        {
            System.Threading.Tasks.Task task = this.CreateTask(progress);
            await task;
        }

        /// <summary>
        /// Create task to run.
        /// </summary>
        /// <param name="progress">Object to handle progress of task.</param>
        /// <returns>Task running asynchronously.</returns>
        protected virtual System.Threading.Tasks.Task CreateTask(IProgress<ProgressInfo> progress)
        {
            System.Threading.Tasks.Task task = System.Threading.Tasks.Task.Run(() =>
            {
                Console.WriteLine($"Start!");

                ProgressInfo info = new ProgressInfo();

                int denominator = 100;
                var baseProgInfo = new ProgressInfo()
                {
                    Denominator = denominator
                };
                for (int index = 0; index < 10; index++)
                {
                    for (int index2 = 0; index2 <= denominator; index2++)
                    {
                        ProgressInfo progInfo = new ProgressInfo(baseProgInfo);
                        progInfo.ProcessName = $"Process_{index2} ({index} / 10)";
                        progInfo.Numerator = index2;
                        progInfo.ShouldContinue = true;
                        progress.Report(progInfo);

                        Thread.Sleep(5);
                    }
                }

                var endInfo = new ProgressInfo(baseProgInfo);
                endInfo.ShouldContinue = false;
                progress.Report(endInfo);

            });
            return task;
        }
    }
}
