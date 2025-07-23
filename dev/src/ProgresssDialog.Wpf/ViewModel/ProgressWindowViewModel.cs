using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgresssDialog.Wpf.ViewModel
{
    /// <summary>
    /// ViewModel for managing the state and behavior of the progress window in the UI.
    /// </summary>
    internal class ProgressWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// Backing field for the window title.
        /// </summary>
        protected string _title = string.Empty;

        /// <summary>
        /// Backing field for the progress item context.
        /// </summary>
        protected ProgressItemViewModel _itemContext = new();

        /// <summary>
        /// Occurs when the window should be closed, for example, when an error occurs during progress processing.
        /// </summary>
        public event EventHandler? CloseRequested;

        /// <summary>
        /// Gets or sets the title of the progress window.
        /// </summary>
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                RaisePropertyChange();
            }
        }

        /// <summary>
        /// Gets or sets the ViewModel representing the progress item displayed in the window.
        /// </summary>
        public ProgressItemViewModel ItemContext
        {
            get => _itemContext;
            set
            {
                _itemContext = value;
                RaisePropertyChange();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressWindowViewModel"/> class.
        /// </summary>
        public ProgressWindowViewModel() : base() { }

        /// <summary>
        /// Handles the event that requests the start of the progress operation.
        /// If an error occurs, the window close event is raised.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        public virtual void OnProgressRequested(object sender, EventArgs e)
        {
            try
            {
                _itemContext.OnProgressRequested(this, e);
            }
            catch (InvalidOperationException)
            {
                // TODO: Log the exception or notify the user as needed
                OnRequestClose();
            }
        }

        /// <summary>
        /// Raises the <see cref="CloseRequested"/> event to notify that the window should be closed.
        /// </summary>
        public virtual void OnRequestClose()
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Handles the event indicating that the progress item process has completed.
        /// Invokes the <see cref="OnRequestClose"/> method to request closing the progress window.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        public virtual void ProgressItemProcessDone(object? sender, EventArgs e)
        {
            OnRequestClose();
        }
    }
}
