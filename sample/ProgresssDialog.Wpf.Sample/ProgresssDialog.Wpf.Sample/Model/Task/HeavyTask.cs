using ProgresssDialog.Wpf.Model;
using ProgresssDialog.Wpf.Task;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
        public System.Threading.Tasks.Task RunAsync(IProgress<ProgressInfo> progress, CancellationToken? cancelToken = null)
        {
            System.Threading.Tasks.Task task = Run(progress, cancelToken);

            return task;
        }

        /// <summary>
        /// Run task async.
        /// </summary>
        /// <param name="progress">Object to handle progress of task.</param>
        /// <returns>Task running asynchronously.</returns>
        protected virtual async System.Threading.Tasks.Task Run(IProgress<ProgressInfo> progress, CancellationToken? cancelToken)
        {
            System.Threading.Tasks.Task task = CreateTask(progress, cancelToken);
            await task;
        }

        /// <summary>
        /// Create task to run.
        /// </summary>
        /// <param name="progress">Object to handle progress of task.</param>
        /// <returns>Task running asynchronously.</returns>
        protected virtual System.Threading.Tasks.Task CreateTask(IProgress<ProgressInfo> progress, CancellationToken? cancelToken)
        {
            System.Threading.Tasks.Task task = System.Threading.Tasks.Task.Run(() =>
            {
                try
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
                            cancelToken?.ThrowIfCancellationRequested();
                            ProgressInfo progInfo = new ProgressInfo(baseProgInfo);
                            progInfo.ProcessName = $"Process_{index2} ({index} / 10)";
                            progInfo.Numerator = index2;
                            progInfo.ShouldContinue = true;
                            progress.Report(progInfo);

                            Thread.Sleep(1 * 1000);
                        }
                    }

                    var endInfo = new ProgressInfo(baseProgInfo);
                    endInfo.ShouldContinue = false;
                    progress.Report(endInfo);
                }
                catch (OperationCanceledException)
                {
                    ProgressInfo progInfo = new ProgressInfo();
                    progInfo.ProcessName = "Process canceled.";
                    progInfo.Numerator = 100;
                    progInfo.Denominator = 100;
                    progInfo.ShouldContinue = false;
                    progress.Report(progInfo);
                }
            });
            return task;
        }
    }
}
