using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Entities;

namespace CourseProject.ViewModel.Interfaces
{
    public interface IMediator
    {
        INavigation NavigationService { get; }
        ISignIn SignInService { get; }
        IInfoProfile InfoProfileService { get; }
        IEditorTrain EditorTrainService { get; }
        IShowerStructureVan ShowerStructureVan { get; }
        ISearcherWaysService SearcherWaysService { get; }
        IBuyTicket BuyTicketService { get; }
        IMainMenuController MainMenuControllerService { get; }
        IReportService ReportService { get; }
        IGetCollectionsService GetCollectionsService { get; }
        IProcesserDoUndo<Train> EditorStrategy { get; }
    }
}
