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
        private IUnityOfWork unityOfWork;
        public void SetCurrentUser(User user) => currentUser = user;
        private ObservableCollection<PassengerViewModel> passengers = new ObservableCollection<PassengerViewModel>();
        public ProfileService(IUnityOfWork unityOfWork)
        {
            this.unityOfWork = unityOfWork;
        }
        public ICommand AddPassenger 
        {
            get => new RelayCommand((obj) =>
            {
                passengers.Add(new PassengerViewModel());
            });
        }
        public ICommand RemovePassenger 
        {
            get => new RelayCommand((obj) =>
            {
                if (obj is PassengerViewModel PassangerForRemove)
                {
                    passengers.Remove(PassangerForRemove);
                    unityOfWork.Passengers.Delete(PassangerForRemove.Id);
                }
            });
        }
        public ICommand SavePassenger
        {
            get => new RelayCommand((obj) =>
            {
                unityOfWork.Passengers.Update((obj as PassengerViewModel).GetPassanger());
            });
        }
        public ICommand SetPassengerByUser
        {
            get => new RelayCommand((obj) =>
            {
                
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
                    //unityOfWork.Save();
                }
                else
                    MessageBox.Show("Старый пароль неверный!");
            }, (obj) => (obj is PasswordChangeModel passwordChangeModel && passwordChangeModel.NewPassword != null && passwordChangeModel.OldPassword != null));
        }
        public void LoadPassengers()
        {
            FlagLoaded = true;
            passengers.Add(new PassengerViewModel() { Id = 1 });
            passengers.Add(new PassengerViewModel() { Id = 1 });
        }
        public ObservableCollection<PassengerViewModel> PassengerViewModels 
        {
            get
            {
                ObservableCollection<PassengerViewModel> a = new ObservableCollection<PassengerViewModel>();
                a.Add(new PassengerViewModel() { Id = 1 });
                a.Add(new PassengerViewModel() { Id = 1 });
                return a;
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
