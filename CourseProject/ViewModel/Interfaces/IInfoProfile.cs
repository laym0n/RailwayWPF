using CourseProject.Model;
using DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CourseProject.ViewModel.Interfaces
{
    public interface IInfoProfile
    {
        void SetCurrentUser(UserModel user);
        ObservableCollection<PassengerViewModel> PassengerViewModels { get; }
        ObservableCollection<TrainInProfileModel> TrainInProfileModels { get; }
        ObservableCollection<TicketModelForProfile> Tickets { get; }
        void ClearDate();
        void LoadDataForProfile();
        ICommand EditTrain { get; }
        ICommand RemoveTrain { get; }
        ICommand ChangePassword { get; }
        ICommand AddPassenger { get; }
        ICommand RemovePassenger { get; }
        ICommand SavePassenger { get; }
        ICommand RemoveTicket { get; }
    }
}
