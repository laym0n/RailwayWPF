using CourseProject.Model;
using CourseProject.ViewModel.Interfaces;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DAL;
using System.Windows;
using MaterialDesignThemes.Wpf;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace CourseProject.ViewModel.Tests
{
    public class SignInService : ISignIn
    {
        private readonly IUnitOfWork unityOfWork;
        public SignInService(IUnitOfWork unityOfWork)
        {
            this.unityOfWork = unityOfWork;
        }
        public event Action UserSignOut;
        public event Action<User> UserSignIn;
        public ICommand SignIn
        {
            get => new RelayCommand((obj) =>
            {
                User currentUser;
                UserShow userForLoginWindow = new UserShow();
                bool? dialogResult;
                do
                {
                    userForLoginWindow.Password = "";
                    LoginWindow loginWindow = new LoginWindow(userForLoginWindow) { Title = "Вход в систему" };
                    dialogResult = loginWindow.ShowDialog();
                    if (dialogResult == true)
                    {
                        if ((currentUser = unityOfWork.Users.GetItem(userForLoginWindow.Login)) != null && currentUser.Password == userForLoginWindow.Password)
                        {
                            dialogResult = false;
                            UserSignIn?.Invoke(currentUser);
                        }
                        else
                            MessageBox.Show("Логин или паролль неверный!");
                    }
                } while (dialogResult == true);

            });
        }
        public ICommand SignUp
        {
            get => new RelayCommand((obj) =>
            {
                User currentUser;
                UserShow userForLoginWindow = new UserShow();
                bool? dialogResult;
                do
                {
                    userForLoginWindow.Password = "";
                    LoginWindow loginWindow = new LoginWindow(userForLoginWindow) { Title = "Регистрация" };
                    dialogResult = loginWindow.ShowDialog();
                    if (dialogResult == true)
                    {
                        if ((currentUser = unityOfWork.Users.GetItem(userForLoginWindow.Login)) == null)
                        {
                            User UserForAdd = new User()
                            {
                                Login = userForLoginWindow.Login,
                                Password = userForLoginWindow.Password,
                                TypeOfUserId = 1
                            };
                            unityOfWork.Users.Create(UserForAdd);
                            unityOfWork.Save();
                            MessageBox.Show("Пользователль успешно зарегестрирован!");
                            break;
                        }
                        else
                            MessageBox.Show("Логин уже занят!");
                    }
                } while (dialogResult == true);

            });
        }
        public ICommand SignOut
        {
            get => new RelayCommand((obj) =>
            {
                UserSignOut?.Invoke();
            });
        }
    }
}
