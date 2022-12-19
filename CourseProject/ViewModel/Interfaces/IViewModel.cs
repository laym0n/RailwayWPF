using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.ViewModel.Interfaces
{
    public interface IMediator
    {
        INavigation NavigationService { get; }
        ISignIn SignInService { get; }
        IInfoProfile InfoProfileService { get; }
        IEditorTrain EditorTrainService { get; }
        IShowerStructureVan ShowerStructureVan { get; }
        ISearcherWays SearcherWaysService { get; }
        IBuyTicket BuyTicketService { get; }
        IMainMenuController MainMenuControllerService { get; }
    }
}
