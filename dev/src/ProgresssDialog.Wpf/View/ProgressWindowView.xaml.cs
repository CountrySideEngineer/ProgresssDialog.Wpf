using ProgresssDialog.Wpf.ViewModel;
using System;
using System.Windows;

namespace ProgresssDialog.Wpf.View
{
    /// <summary>
    /// ProgressDialogView.xaml の相互作用ロジック
    /// </summary>
    public partial class ProgressWindowView : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressWindowView"/> class.
        /// </summary>
        public ProgressWindowView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressWindowView"/> class.
        /// </summary>
        /// <param name="height">Window height.</param>
        /// <param name="width">Window width.</param>
        public ProgressWindowView(int height, int width)
        {
            Height = height;
            Width = width;

            InitializeComponent();
        }

        /// <summary>
        /// Handles the DataContextChanged event of the window.
        /// Subscribes to the CloseRequested event of the <see cref="ProgressWindowViewModel"/> to close the window when requested.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void Window_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((e.OldValue != null) && (e.OldValue is ProgressWindowViewModel oldViewModel))
            {
                oldViewModel.CloseRequested -= OnWindowClose;
            }
            if (e.NewValue is ProgressWindowViewModel viewModel)
            {
                viewModel.CloseRequested += OnWindowClose;
            }
        }

        /// <summary>
        /// Handles the CloseRequested event from the ViewModel and closes the window.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void OnWindowClose(object? sender, EventArgs e)
        {
            // Ensure Close is called on the UI thread
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(Close);
            }
            else
            {
                Close();
            }
        }

        /// <summary>
        /// Handles the ContentRendered event of the window.
        /// Invokes the <see cref="ProgressWindowViewModel.OnProgressRequested"/> method on the DataContext
        /// to start the progress operation when the window content is first rendered.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            if (DataContext is ProgressWindowViewModel viewModel)
            {
                viewModel.OnProgressRequested(this, EventArgs.Empty);
            }
        }
    }
}
