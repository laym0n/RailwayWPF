using CourseProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using DAL;
using CourseProject.Model.Enumerations;

namespace CourseProject.ViewModel.Interfaces
{
    public interface INavigation
    {
        event Action<Page> Leave;
        event Action<Page> Enter;
        void SetPageFrame(Frame frame);
        void ClearHistoryPage();
        void LoadPreviousPage();
        void LoadNextPage(TypePage typePage);
        void LoadPreviousPageWithNotify();
        void LoadNextPageWithNotify(TypePage typePage);
        void LoadPage(TypePage typePage);
        void LoadPageWithNotify(TypePage typePage);
        void SetViewModel(IMediator mediator);
    }
}
