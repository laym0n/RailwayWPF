using CourseProject.Model;
using CourseProject.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CourseProject.ViewModel
{
    public class NavigationService : INavigation
    {
        public event Func<IMediator> GetMediator;
        public MenuShow VisibleButtons { get; }
        public NavigationService(Frame frame)
        {
            this.PageFrame = frame;
            VisibleButtons = new MenuShow()
            {
                VisibleBuyTicket = "Collapsed",
                VisibleCreateTrain = "Collapsed",
                VisibleProfile = "Collapsed",
                VisibleSignIn = "Visible",
                VisibleSignOut = "Collapsed",
                VisibleSignUp = "Visible",
            };
        }
        public Frame PageFrame { get; }
        public void SetMainMenuWhenSignOut()
        {
            VisibleButtons.VisibleBuyTicket = "Collapsed";
            VisibleButtons.VisibleCreateTrain = "Collapsed";
            VisibleButtons.VisibleProfile = "Collapsed";
            VisibleButtons.VisibleSignIn = "Visible";
            VisibleButtons.VisibleSignOut = "Collapsed";
            VisibleButtons.VisibleSignUp = "Visible";
        }
        public void SetMainMenuWhenSignIn()
        {
            VisibleButtons.VisibleBuyTicket = "Visible";
            VisibleButtons.VisibleCreateTrain = "Visible";
            VisibleButtons.VisibleProfile = "Visible";
            VisibleButtons.VisibleSignIn = "Collapsed";
            VisibleButtons.VisibleSignOut = "Visible";
            VisibleButtons.VisibleSignUp = "Collapsed";
        }
        public ICommand NavigateBuyTicket
        {
            get => new RelayCommand((obj) => {
                PageFrame.Navigate(new BuyTicketPage());
            });
        }
        public ICommand NavigateProfile
        {
            get => new RelayCommand((obj) => {
                PageFrame.Navigate(new Profile(GetMediator()));
            });
        }
    }
}
