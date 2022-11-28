using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProject.ViewModel.Interfaces;
using System.Windows.Input;
using CourseProject.Model;
using DAL;

namespace CourseProject.ViewModel.Tests
{
    public class SignInTest : ISignIn
    {
        bool registr = false;
        public SignInTest()
        {
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
                registr = !registr;
            }, (obj) => !registr);
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
                registr = !registr;
            }, (obj) => registr);
        }
        public User SignInUser { get => null; }
        private MenuShow menuShowButtons;
        public MenuShow MenuShowButtons { get => menuShowButtons; }
    }
}
