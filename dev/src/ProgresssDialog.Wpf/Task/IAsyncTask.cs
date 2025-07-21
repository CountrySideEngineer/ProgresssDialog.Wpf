using SystemTask = System.Threading.Tasks.Task;

namespace ProgresssDialog.Wpf.Task
{
    public interface IAsyncTask<T>
    {
        SystemTask RunAsync(IProgress<T> progress, CancellationToken? cancelToke = null);
    }
}
