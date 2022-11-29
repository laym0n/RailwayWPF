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
        Frame frame;
        public SignInTest(Frame frame)
        {
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
                menuShowButtons.VisibleBuyTicket = "Visible";
                menuShowButtons.VisibleCreateTrain = "Visible";
                menuShowButtons.VisibleProfile = "Visible";
                menuShowButtons.VisibleSignIn = "Collapsed";
                menuShowButtons.VisibleSignOut = "Visible";
                menuShowButtons.VisibleSignUp = "Collapsed";
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
                menuShowButtons.VisibleBuyTicket = "Collapsed";
                menuShowButtons.VisibleCreateTrain = "Collapsed";
                menuShowButtons.VisibleProfile = "Collapsed";
                menuShowButtons.VisibleSignIn = "Visible";
                menuShowButtons.VisibleSignOut = "Collapsed";
                menuShowButtons.VisibleSignUp = "Visible";
            });
        }
        public ICommand EnterProfile
        {
            get => new RelayCommand((obj) => {
                frame.Navigate(new Profile()); });
        }
        public User SignInUser { get => new User() { Id = 1, Password = "test"}; }
        private MenuShow menuShowButtons;
        public MenuShow MenuShowButtons { get => menuShowButtons; }
    }
}
