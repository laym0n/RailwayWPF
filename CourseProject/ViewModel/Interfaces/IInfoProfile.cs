using CourseProject.Model;
using DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseProject.ViewModel.Interfaces
{
    public interface IInfoProfile
    {
        void LoadPassengers(PassengerProfileCollection passengers);
        ObservableCollection<PassengerViewModel> PassengerViewModels { get; } 
        ICommand ChangePassword { get; }
        ICommand SetPassengerByUser { get; }
        ICommand AddPassenger { get; }
        ICommand RemovePassenger { get; }
        ICommand SavePassenger { get; }
    }
}
