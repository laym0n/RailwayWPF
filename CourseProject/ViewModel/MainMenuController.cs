using CourseProject.Model;
using CourseProject.Model.Enumerations;
using CourseProject.ViewModel.Interfaces;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseProject.ViewModel
{
    public class MainMenuController : IMainMenuController
    {
        SetterVisibleButtonsMainMenu setterVisibleButtonsMainMenu;
        public event Action<TypePage> UserChoosePage;
        public MenuShow VisibleButtons { get; } = new MenuShow();
        public MainMenuController(SetterVisibleButtonsMainMenu setterVisibleButtonsMainMenu)
        {
            this.setterVisibleButtonsMainMenu = setterVisibleButtonsMainMenu;
            SetUserSignOut();
        }
        public void SetUserSignIn(UserModel user)
        {
            setterVisibleButtonsMainMenu.SetVisibleForButtons(VisibleButtons, user.TypeUser);
        }
        public void SetUserSignOut()
        {
            VisibleButtons.VisibleBuyTicket = "Collapsed";
            VisibleButtons.VisibleCreateTrain = "Collapsed";
            VisibleButtons.VisibleProfile = "Collapsed";
            VisibleButtons.VisibleSignIn = "Visible";
            VisibleButtons.VisibleSignOut = "Collapsed";
            VisibleButtons.VisibleSignUp = "Visible";
            VisibleButtons.VisibleReport = "Collapsed";
        }
        public ICommand LoadProfile 
        {
            get => new RelayCommand((obj) => UserChoosePage(TypePage.ProfilePage));
        }
        public ICommand LoadSearchWay
        {
            get => new RelayCommand((obj) => UserChoosePage(TypePage.SearchWayPage));
        }
        public ICommand LoadEditTrain
        {
            get => new RelayCommand((obj) => UserChoosePage(TypePage.EditTrainPage));
        }
        public ICommand LoadReports
        {
            get => new RelayCommand((obj) => UserChoosePage(TypePage.ReportPage));
        }
    }
}
