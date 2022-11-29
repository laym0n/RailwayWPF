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
        private readonly IUnityOfWork unityOfWork;
        private readonly Frame frame;
        private readonly MenuShow menuShowButtons;
        User user;
        public SignInService(IUnityOfWork unityOfWork, Frame frame)
        {
            this.unityOfWork = unityOfWork;
            this.frame = frame;
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
                if (!(frame.Content is BuyTicketPage))
                    frame.Navigate(new BuyTicketPage());

                user = null;

            });
        }
        public ICommand EnterProfile
        {
            get => new RelayCommand((obj) =>
            {
                if (!(frame.Content is Profile))
                    frame.Navigate(new Profile());
            });
        }
        public User SignInUser { get=> user;}
        public MenuShow MenuShowButtons { get => menuShowButtons; }
    }
}
