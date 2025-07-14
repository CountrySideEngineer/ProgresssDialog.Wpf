using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProgresssDialog.Wpf.ViewModel
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public virtual void RaisePropertyChange([CallerMemberName ]string proName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(proName));
        }
    }
}
