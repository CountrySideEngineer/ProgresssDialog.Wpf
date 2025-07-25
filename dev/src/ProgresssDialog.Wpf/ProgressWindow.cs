using ProgresssDialog.Wpf.Model;
using ProgresssDialog.Wpf.Model.Args;
using ProgresssDialog.Wpf.Task;
using ProgresssDialog.Wpf.View;
using ProgresssDialog.Wpf.ViewModel;

namespace ProgresssDialog.Wpf
{
    public class ProgressWindow
    {
        public virtual void Start(IAsyncTask<ProgressInfo> task)
        {
            var viewModel = new ProgressItemViewModel()
            {
                AsyncTask = task
            };
            var progressReporter = new Progress<ProgressInfo>((progressInfo) =>
            {
                var arg = new ProgressChangedEventArg()
                {
                    ProgressInfo = progressInfo
                };
                viewModel.OnProgressChanged(this, arg);

                if (!progressInfo.ShouldContinue)
                {
                    viewModel.OnProgressFinished(this, EventArgs.Empty);
                }

            });
            viewModel.ProgressReporter = progressReporter;
            var windowViewModel = new ProgressWindowViewModel()
            {
                ItemContext = viewModel
            };
            viewModel.ProcessFinished += windowViewModel.ProgressItemProcessDone;

            var progressWindow = new ProgressWindowView()
            {
                DataContext = windowViewModel
            };
            progressWindow.ShowDialog();
        }

        public virtual void Start(IAsyncTask<ProgressInfo> task, int height, int width)
        {
            var viewModel = new ProgressItemViewModel()
            {
                AsyncTask = task
            };
            var progressReporter = new Progress<ProgressInfo>((progressInfo) =>
            {
                var arg = new ProgressChangedEventArg()
                {
                    ProgressInfo = progressInfo
                };
                viewModel.OnProgressChanged(this, arg);

                if (!progressInfo.ShouldContinue)
                {
                    viewModel.OnProgressFinished(this, EventArgs.Empty);
                }

            });
            viewModel.ProgressReporter = progressReporter;
            var windowViewModel = new ProgressWindowViewModel()
            {
                ItemContext = viewModel
            };
            viewModel.ProcessFinished += windowViewModel.ProgressItemProcessDone;

            var progressWindow = new ProgressWindowView(height:height, width:width)
            {
                DataContext = windowViewModel
            };
            progressWindow.ShowDialog();
        }
    }
}
