using CourseProject.Model;
using CourseProject.Model.Enumerations;
using CourseProject.View;
using CourseProject.ViewModel.Interfaces;
using DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;

namespace CourseProject.ViewModel
{
    public class NavigationService : INavigation
    {
        Stack<Page> HistoryPages = new Stack<Page>();
        FabricPages fabricPages;
        private Frame PageFrame;

        public NavigationService(Frame frame)
        {
            this.PageFrame = frame;
        }

        public void SetPageFrame(Frame frame)
        {
            PageFrame = frame;
        }
        public void SetViewModel(IMediator mediator)
        {
            fabricPages = new FabricPages(mediator);
        }
        public event Action<Page> Leave;
        public event Action<Page> Enter;
        public void LoadPageWithNotify(TypePage typePage)
        {
            Leave?.Invoke(PageFrame.Content as Page);
            LoadPage(typePage);
            Enter?.Invoke(PageFrame.Content as Page);
        }
        public void LoadPage(TypePage typePage)
        {
            Page NewPage = fabricPages.GetPage(typePage);
            PageFrame.Navigate(NewPage);
        }
        public void LoadNextPageWithNotify(TypePage typePage)
        {

            Page CurrentPage = PageFrame.Content as Page;
            HistoryPages.Push(CurrentPage);
            LoadPageWithNotify(typePage);
        }
        public void LoadPreviousPageWithNotify()
        {
            Leave?.Invoke(PageFrame.Content as Page);
            LoadPreviousPage();
            Enter?.Invoke(PageFrame.Content as Page);
        }
        public void LoadNextPage(TypePage typePage)
        {
            Page CurrentPage = PageFrame.Content as Page;
            HistoryPages.Push(CurrentPage);
            LoadPage(typePage);
        }
        public void LoadPreviousPage()
        {
            if (HistoryPages.Count == 0)
            {
                return;
            }
            Page NewPage = HistoryPages.Peek();
            HistoryPages.Pop();
            PageFrame.Navigate(NewPage);
        }
        public void ClearHistoryPage()
        {
            HistoryPages.Clear();
        }
        public ICommand GoToPreviousPageWithNotify 
        {
            get => new RelayCommand((obj) => LoadPreviousPageWithNotify());
        }
        public ICommand GoToPreviousPage
        {
            get => new RelayCommand((obj) => LoadPreviousPage());
        }
    }
}
