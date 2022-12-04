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
using System.Windows.Controls;
using System.Windows.Input;

namespace CourseProject.ViewModel
{
    internal class ProfileService: IInfoProfile
    {
        private User currentUser;
        private IUnitOfWork unitOfWork;
        public void SetCurrentUser(User user) => currentUser = user;
        public void ClearUnSavedDataWhenUserLeavePage(Page page)
        {
            if(page is Profile)
            {
                passengers?.Where(i => !i.LoadedInDB).ToList().All(i => passengers.Remove(i));
            }
        }
        private ObservableCollection<PassengerViewModel> passengers = null;
        //private ObservableCollection<PassengerViewModel> passengers = null;
        public event Action<TrainModel> EditExistTrain;
        public ProfileService(IUnitOfWork unityOfWork)
        {
            this.unitOfWork = unityOfWork;
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
                    
                    unitOfWork.Passengers.Delete(PassangerForRemove.Id);
                    unitOfWork.Save();
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
                        unitOfWork.Passengers.Update(PassangerForSave.GetPassanger());
                    else
                        unitOfWork.Passengers.Create(PassangerForSave.GetPassanger());
                    PassangerForSave.LoadedInDB = true;
                    unitOfWork.Save();
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
                    unitOfWork.Users.Update(currentUser);
                    unitOfWork.Save();
                    MessageBox.Show("Пароль успешно сменен!");

                }
                else
                    MessageBox.Show("Старый пароль неверный!");
            }, 
                (obj) => 
                (obj is PasswordChangeModel passwordChangeModel && passwordChangeModel.NewPassword != null && passwordChangeModel.OldPassword != null));
        }
        public ObservableCollection<PassengerViewModel> PassengerViewModels 
        {
            get
            {
                return passengers ?? (passengers = new ObservableCollection<PassengerViewModel>(unitOfWork.Passengers.GetList().Where(i=>i.UserId == currentUser.Id).Select(i => new PassengerViewModel(i, true))));
                //когда буду переходить на другую страницу не забыть удалить из PassengerViewModels не добавленные профии пассажиров
            }
        }
        public void CurrentUserSignOut()
        {
            passengers.Clear();
            passengers = null;
            currentUser = null;
        }
    }
}
