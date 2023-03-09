using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT_Technical
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string eventName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(eventName));
        }
    }
}
