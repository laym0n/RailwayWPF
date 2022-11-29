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
        event Func<User> GetCurrentUser;
        ObservableCollection<PassengerViewModel> PassengerViewModels { get; }
        void ClearPassengerCollection();
        ICommand ChangePassword { get; }
        ICommand SetPassengerByUser { get; }
        ICommand AddPassenger { get; }
        ICommand RemovePassenger { get; }
        ICommand SavePassenger { get; }
    }
}
