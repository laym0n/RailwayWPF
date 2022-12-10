using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model.Collections
{
    public class ModelCollection <T> : INotifyPropertyChanged
    {
        public ObservableCollection<T> collection { get; set; }
        public ObservableCollection<T> Collection
        {
            get => collection;
            set
            {
                collection = value;
                OnPropertyChanged("Collection");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
