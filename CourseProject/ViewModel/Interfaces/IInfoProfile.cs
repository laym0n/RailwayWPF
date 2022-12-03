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
        void SetCurrentUser(User user);
        ObservableCollection<PassengerViewModel> PassengerViewModels { get; }
        event Action<TrainModel> EditExistTrain;
        void ClearPassengerCollection();
        ICommand EditTrain { get; }
        ICommand ChangePassword { get; }
        ICommand AddPassenger { get; }
        ICommand RemovePassenger { get; }
        ICommand SavePassenger { get; }
    }
}
