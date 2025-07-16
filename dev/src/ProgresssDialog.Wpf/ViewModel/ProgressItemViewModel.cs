using ProgresssDialog.Wpf.Model.Args;
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
                RaisePropertyChange(nameof(Progress));
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
                RaisePropertyChange(nameof(Progress));
            }
        }

        protected virtual void UpdateProgress()
        {
            if (0 == Denominator)
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
    }
}
