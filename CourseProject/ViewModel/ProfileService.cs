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
    internal class ProfileService: DependencyObject, IInfoProfile
    {
        private IUnityOfWork unityOfWork;
        public static readonly DependencyProperty passengersProperty;
        private ObservableCollection<PassengerViewModel> passengers;
        private ISignIn signIn;
        public ProfileService(IUnityOfWork unityOfWork, ISignIn signIn)
        {
            this.unityOfWork = unityOfWork;
            this.signIn = signIn;
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
                if (obj is PasswordChangeModel passwordChangeModel && signIn.SignInUser.Password == passwordChangeModel.OldPassword)
                {
                    signIn.SignInUser.Password = passwordChangeModel.NewPassword;
                    unityOfWork.Users.Update(signIn.SignInUser);
                    //unityOfWork.Save();
                }
                else
                    MessageBox.Show("Старый пароль неверный!");
            }, (obj) => (obj is PasswordChangeModel passwordChangeModel && passwordChangeModel.NewPassword != null && passwordChangeModel.OldPassword != null));
        }
        public void LoadPassengers(PassengerProfileCollection passengers)
        {
            //ObservableCollection<PassengerViewModel> a = new ObservableCollection<PassengerViewModel>();
            //a.Add(new PassengerViewModel() { Id = 1 });
            //a.Add(new PassengerViewModel() { Id = 1 });
            //passengers.PassengerCollection = a;
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
    }
}
