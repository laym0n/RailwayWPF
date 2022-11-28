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

namespace CourseProject.ViewModel.Tests
{
    public class SignInService : ISignIn
    {
        IUnityOfWork unityOfWork;
        User user;
        public SignInService(IUnityOfWork unityOfWork)
        {
            this.unityOfWork = unityOfWork;
            menuShowButtons = new MenuShow()
            {
                VisibleBuyTicket = "Collapsed",
                VisibleCreateTrain = "Collapsed",
                VisibleProfile = "Collapsed",
                VisibleSignIn = "Visible",
                VisibleSignOut = "Collapsed",
                VisibleSignUp = "Visible",
            };
        }
        public ICommand SignIn
        {
            get => new RelayCommand((obj) =>
            {
                UserShow userForLoginWindow = new UserShow();
                bool? dialogResult;
                do
                {
                    userForLoginWindow .Password = "";
                    LoginWindow loginWindow = new LoginWindow(userForLoginWindow) { Title = "Вход в систему" };
                    dialogResult = loginWindow.ShowDialog();
                    if (dialogResult == true)
                    {
                        if ((user = unityOfWork.Users.GetItem(userForLoginWindow.Login)) != null)
                        {
                            menuShowButtons.VisibleBuyTicket = "Visible";
                            menuShowButtons.VisibleCreateTrain = "Visible";
                            menuShowButtons.VisibleProfile = "Visible";
                            menuShowButtons.VisibleSignIn = "Collapsed";
                            menuShowButtons.VisibleSignOut = "Visible";
                            menuShowButtons.VisibleSignUp = "Collapsed";
                            (obj as PackIcon).Kind = PackIconKind.Account;
                            break;
                        }
                        else
                            MessageBox.Show("Логин или паролль неверный!");
                    }
                } while (dialogResult == true);

            }, (obj) => user == null);
        }
        public ICommand SignUp
        {
            get => new RelayCommand((obj) =>
            {
                UserShow userForLoginWindow = new UserShow();
                bool? dialogResult;
                do
                {
                    userForLoginWindow.Password = "";
                    LoginWindow loginWindow = new LoginWindow(userForLoginWindow) { Title = "Регистрация" };
                    dialogResult = loginWindow.ShowDialog();
                    if (dialogResult == true)
                    {
                        if ((user = unityOfWork.Users.GetItem(userForLoginWindow.Login)) == null)
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

            }, (obj) => user == null);
        }
        public ICommand SignOut
        {
            get => new RelayCommand((obj) =>
            {

                menuShowButtons.VisibleBuyTicket = "Collapsed";
                menuShowButtons.VisibleCreateTrain = "Collapsed";
                menuShowButtons.VisibleProfile = "Collapsed";
                menuShowButtons.VisibleSignIn = "Visible";
                menuShowButtons.VisibleSignOut = "Collapsed";
                menuShowButtons.VisibleSignUp = "Visible";
                user = null;
                (obj as PackIcon).Kind = PackIconKind.AccountQuestion;

            }, (obj) => user != null);
        }
        public User SignInUser { get=> user;}
        private MenuShow menuShowButtons;
        public MenuShow MenuShowButtons { get => menuShowButtons; }
    }
}
