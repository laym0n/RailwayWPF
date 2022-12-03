using DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model
{
    public class VanModelCollection : INotifyPropertyChanged
    {
        public VanModelCollection()
        {
            vanCollection = new ObservableCollection<VanModel>();
        }
        private ObservableCollection<VanModel> vanCollection;
        public ObservableCollection<VanModel> VanCollection
        {
            get => vanCollection;
            set
            {
                vanCollection = value;
                OnPropertyChanged("VanCollection");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
