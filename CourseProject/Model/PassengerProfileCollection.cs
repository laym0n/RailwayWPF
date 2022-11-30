using CourseProject.ViewModel.Interfaces;
using DAL;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseProject.Model
{
    public class PassengerProfileCollection : INotifyPropertyChanged
    {
        public PassengerProfileCollection()
        {
            PassengerCollection = new ObservableCollection<PassengerViewModel>();
        }
        private ObservableCollection<PassengerViewModel> passengerCollection;
        public ObservableCollection<PassengerViewModel> PassengerCollection {
            get => passengerCollection;
            set
            {
                passengerCollection = value;
                OnPropertyChanged("PassengerCollection");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
