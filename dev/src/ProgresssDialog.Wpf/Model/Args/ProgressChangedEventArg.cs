using System;

namespace ProgresssDialog.Wpf.Model.Args
{
    /// <summary>
    /// Provides data for a progress changed event.
    /// </summary>
    internal class ProgressChangedEventArg : EventArgs
    {
        /// <summary>
        /// Gets the progress information associated with the event.
        /// </summary>
        public ProgressInfo ProgressInfo { get; set; } = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressChangedEventArg"/> class.
        /// </summary>
        public ProgressChangedEventArg()
        {
            ProgressInfo = new ProgressInfo();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressChangedEventArg"/> class with the specified progress information.
        /// </summary>
        /// <param name="progressInfo">The progress information to associate with the event.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="progressInfo"/> is null.</exception>
        public ProgressChangedEventArg(ProgressInfo progressInfo)
        {
            ArgumentNullException.ThrowIfNull(progressInfo);

            ProgressInfo = new ProgressInfo(progressInfo);
        }
    }
}
