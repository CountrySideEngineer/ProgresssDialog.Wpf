using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgresssDialog.Wpf.Task
{
    public interface IAsyncTask<T>
    {
        void Run(IProgress<T> progress);
    }
}
