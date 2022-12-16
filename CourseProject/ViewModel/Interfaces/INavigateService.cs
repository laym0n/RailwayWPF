using CourseProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using DAL;

namespace CourseProject.ViewModel.Interfaces
{
    public interface INavigation
    {
        IMediator ViewModel { get; set; }
        event Action<Page> Leave;
        event Action<Page> Enter;
        Frame PageFrame { get; }
        MenuShow VisibleButtons { get; }
        void LoadTrainEditPageForEditTrain();
        void LoadPageBuyTicket();
        void SetMainMenuWhenSignOut();
        void SetMainMenuWhenSignIn();
        void LoadPageAfterSaveTrain();
        ICommand NavigateBuyTicket { get; }
        ICommand NavigateProfile { get; }
        ICommand NavigateEditTrain { get; }
    }
}
