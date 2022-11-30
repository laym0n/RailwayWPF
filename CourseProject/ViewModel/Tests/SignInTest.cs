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

namespace CourseProject.ViewModel.Tests
{
    public class SignInTest : ISignIn
    {
        public SignInTest()
        {
        }
        public ICommand SignIn
        {
            get => new RelayCommand((obj) =>
            {
                UserSignIn?.Invoke(CurrentUser);
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
        public event Action<User> UserSignIn;
        public User CurrentUser { get => new User() { Id = 1, Password = "test"}; }
    }
}
