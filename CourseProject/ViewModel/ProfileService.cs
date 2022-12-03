using CourseProject.Model;
using CourseProject.ViewModel.Interfaces;
using DAL;
using DLL.Interfaces;
using Ninject.Infrastructure.Language;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CourseProject.ViewModel
{
    internal class ProfileService: IInfoProfile
    {
        private User currentUser;
        private bool FlagLoaded = false;
        private IUnitOfWork unityOfWork;
        public void SetCurrentUser(User user) => currentUser = user;
        private ObservableCollection<PassengerViewModel> passengers = new ObservableCollection<PassengerViewModel>(); 
        public event Action<TrainModel> EditExistTrain;
        public ProfileService(IUnitOfWork unityOfWork)
        {
            this.unityOfWork = unityOfWork;
        }
        public ICommand EditTrain
        {
            get => new RelayCommand(obj => EditExistTrain?.Invoke(new TrainModel())); 
        }
        public ICommand AddPassenger 
        {
            get => new RelayCommand((obj) =>
            {
                passengers.Add(new PassengerViewModel(currentUser.Id, false));
            });
        }
        public ICommand RemovePassenger 
        {
            get => new RelayCommand((obj) =>
            {
                if (obj is PassengerViewModel PassangerForRemove)
                {
                    passengers.Remove(PassangerForRemove);
                    //сделать запрос на проверку есть ли билеты на этого пассажира
                    
                    unityOfWork.Passengers.Delete(PassangerForRemove.Id);
                    unityOfWork.Save();
                }
            });
        }
        public ICommand SavePassenger
        {
            get => new RelayCommand((obj) =>
            {
                if (obj is PassengerViewModel PassangerForSave)
                {
                    if (PassangerForSave.LoadedInDB)
                        unityOfWork.Passengers.Update(PassangerForSave.GetPassanger());
                    else
                        unityOfWork.Passengers.Create(PassangerForSave.GetPassanger());
                    PassangerForSave.LoadedInDB = true;
                    unityOfWork.Save();
                }
            });
        }
        public ICommand ChangePassword
        {
            get => new RelayCommand((obj) =>
            {
                if (obj is PasswordChangeModel passwordChangeModel && currentUser.Password == passwordChangeModel.OldPassword)
                {
                    currentUser.Password = passwordChangeModel.NewPassword;
                    unityOfWork.Users.Update(currentUser);
                    unityOfWork.Save();
                    MessageBox.Show("Пароль успешно сменен!");

                }
                else
                    MessageBox.Show("Старый пароль неверный!");
            }, 
                (obj) => 
                (obj is PasswordChangeModel passwordChangeModel && passwordChangeModel.NewPassword != null && passwordChangeModel.OldPassword != null));
        }
        public void LoadPassengers()
        {
            FlagLoaded = true;
            passengers = new ObservableCollection<PassengerViewModel>(currentUser.Passenger.Select(i => new PassengerViewModel(i, true)));
        }
        public ObservableCollection<PassengerViewModel> PassengerViewModels 
        {
            get
            {
                //когда буду переходить на другую страницу не забыть удалить из PassengerViewModels не добавленные профии пассажиров
                if (!FlagLoaded)
                    LoadPassengers();
                return passengers;
            }
        }
        public void ClearPassengerCollection()
        {
            FlagLoaded = false;
            passengers.Clear();
            currentUser = null;
        }
    }
}
