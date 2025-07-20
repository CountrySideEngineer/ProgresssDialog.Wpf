using ProgresssDialog.Wpf.Model;
using ProgresssDialog.Wpf.Model.Args;
using ProgresssDialog.Wpf.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProgresssDialog.Wpf.ViewModel
{
    /// <summary>
    /// ViewModel for representing the progress of a single process item in the UI.
    /// </summary>
    internal class ProgressItemViewModel : ViewModelBase
    {
        public delegate void ProcessFinishedEvent(object sender, EventArgs e);
        public event ProcessFinishedEvent? ProcessFinished;

        /// <summary>
        /// Backing field for the process name.
        /// </summary>
        protected string _processName = string.Empty;

        /// <summary>
        /// Backing field for the numerator (current progress value).
        /// </summary>
        protected int _numerator = 0;

        /// <summary>
        /// Backing field for the denominator (maximum progress value).
        /// </summary>
        protected int _denominator = 0;

        /// <summary>
        /// Gets or sets the name of the process.
        /// </summary>
        public string ProcessName
        {
            get => _processName;
            set
            {
                _processName = value;
                RaisePropertyChange();
            }
        }

        protected int _progress = 0;

        public IProgress<ProgressInfo>? ProgressReporter { get; set; }

        public IAsyncTask<ProgressInfo>? AsyncTask { get; set; }

        /// <summary>
        /// Gets the progress percentage (0-100). Returns 0 if denominator is 0.
        /// </summary>
        public int Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                RaisePropertyChange();
            }
        }

        /// <summary>
        /// Gets or sets the current progress value (numerator).
        /// </summary>
        public int Numerator
        {
            get => _numerator;
            set
            {
                _numerator = value;

                RaisePropertyChange();

                UpdateProgress();
            }
        }

        /// <summary>
        /// Gets or sets the maximum progress value (denominator).
        /// </summary>
        public int Denominator
        {
            get => _denominator;
            set
            {
                _denominator = 0 == value ? 0 : value;

                RaisePropertyChange();

                UpdateProgress();
            }
        }

        /// <summary>
        /// Updates the <see cref="Progress"/> property based on the current values of <see cref="Numerator"/> and <see cref="Denominator"/>.
        /// If the denominator is zero, sets the progress to 0 to avoid division by zero.
        /// Otherwise, calculates the progress percentage as (Numerator * 100) / Denominator.
        /// </summary>
        protected virtual void UpdateProgress()
        {
            if (0 != Denominator)
            {
                Progress = (Numerator * 100) / Denominator;
            }
            else
            {
                Progress = 0;
            }

        }

        /// <summary>
        /// Handles progress changed events and updates the ViewModel properties accordingly.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="e">The event arguments containing progress information.</param>
        public virtual void OnProgressChanged(object sender, ProgressChangedEventArg e)
        {
            ProcessName = e.ProgressInfo.ProcessName;
            Denominator = e.ProgressInfo.Denominator;
            Numerator = e.ProgressInfo.Numerator;
        }

        /// <summary>
        /// Handles the event that requests the start of the progress operation.
        /// This method checks if the <see cref="ProgressReporter"/> is set and then starts the asynchronous task
        /// by passing the progress reporter to <see cref="AsyncTask"/>.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if <see cref="ProgressReporter"/> is not set.
        /// </exception>
        public virtual void OnProgressRequested(object sender, EventArgs e)
        {
            if (null == ProgressReporter)
            {
                throw new InvalidOperationException(nameof(ProgressReporter));
            }
            AsyncTask?.RunAsync(ProgressReporter);
        }

        public virtual void OnProgressFinished(object sender, EventArgs e)
        {
            ProcessFinished?.Invoke(this, e);
        }
    }
}
