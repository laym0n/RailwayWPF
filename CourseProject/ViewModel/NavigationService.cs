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
    public class NavigationService : INavigateService
    {

        public NavigationService(Frame frame)
        {
            this.PageFrame = frame;
        }
        public Frame PageFrame { get; }
        public ICommand NavigateBuyTicket
        {
            get => new RelayCommand((obj) => {
                PageFrame.Navigate(new BuyTicketPage());
            });
        }
        public ICommand NavigateProfile
        {
            get => new RelayCommand((obj) => {
                PageFrame.Navigate(new Profile());
            });
        }
    }
}
