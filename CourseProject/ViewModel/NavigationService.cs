using CourseProject.Model;
using CourseProject.View;
using CourseProject.ViewModel.Interfaces;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CourseProject.ViewModel
{
    public class NavigationService : INavigation
    {
        public IMediator ViewModel { get; set; }
        public event Action<Page> Leave;
        public event Action<Page> Enter;
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
        public Frame PageFrame { get; set; }
        public void LoadTrainEditPageForEditTrain()
        {
            PageFrame.Navigate(new TrainEditPage(ViewModel));
        }
        public void SetMainMenuWhenSignOut()
        {
            VisibleButtons.VisibleBuyTicket = "Collapsed";
            VisibleButtons.VisibleCreateTrain = "Collapsed";
            VisibleButtons.VisibleProfile = "Collapsed";
            VisibleButtons.VisibleSignIn = "Visible";
            VisibleButtons.VisibleSignOut = "Collapsed";
            VisibleButtons.VisibleSignUp = "Visible";
            NavigateBuyTicket.Execute(null);
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
        private void Navigate(Page page)
        {
            Leave?.Invoke(PageFrame.Content as Page);
            PageFrame.Navigate(page);
            Enter?.Invoke(page);
        }
        public ICommand NavigateBuyTicket
        {
            get => new RelayCommand((obj) => {
                Navigate(new SearchWaysPage(ViewModel));
            });
        }
        public ICommand NavigateProfile
        {
            get => new RelayCommand((obj) => {
                Navigate(new Profile(ViewModel));
            });
        }
        public ICommand NavigateEditTrain
        {
            get => new RelayCommand((obj) => {
                Navigate(new TrainEditPage(ViewModel));
            });
        }
    }
}
