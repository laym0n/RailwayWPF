using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProject.ViewModel.Interfaces;
using System.Windows.Input;
using CourseProject.Model;
using DAL;
using System.Windows.Controls;
using DLL.Interfaces;

namespace CourseProject.ViewModel.Tests
{
    public class SignInTest : ISignIn
    {
        IUnitOfWork _unityOfWork;
        public SignInTest(IUnitOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }
        public ICommand SignIn
        {
            get => new RelayCommand((obj) =>
            {
                User user = _unityOfWork.Users.GetItem("test");
                UserSignIn?.Invoke(new UserModel(user) { TypeUser = Model.Enumerations.TypeUser.Worker});
            });
        }
        public ICommand SignUp
        {
            get => null;
        }
        public ICommand SignOut
        {
            get => new RelayCommand((obj) =>
            {
                UserSignOut?.Invoke();
            });
        }
        public event Action UserSignOut;
        public event Action<UserModel> UserSignIn;
    }
}
