using CourseProject.Model.Enumerations;
using CourseProject.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.ViewModel
{
    public class ViewModelUnit : IMediator
    {
        public ViewModelUnit
            (ISignIn signIn, 
            IInfoProfile infoProfile, 
            INavigation navigationService, 
            IEditorTrain editorTrain, 
            IShowerStructureVan showerStructureVan, 
            ISearcherWaysService searcherWays, 
            IBuyTicket buyTicket, 
            IMainMenuController mainMenuController)
        {
            this.SignInService = signIn;
            this.InfoProfileService = infoProfile;
            this.NavigationService = navigationService;
            this.EditorTrainService = editorTrain;
            this.ShowerStructureVan = showerStructureVan;
            this.SearcherWaysService = searcherWays;
            this.BuyTicketService = buyTicket;
            this.MainMenuControllerService = mainMenuController;

            #region SignIn

            #endregion


            #region BuyTicket
            //SearcherWaysService.UserChooseWay += BuyTicketService.GetWayForBuyticket;
            SignInService.UserSignIn += BuyTicketService.SetUser;
            #endregion

            #region ShowerStructureVan
            EditorTrainService.VanChoosen += ShowerStructureVan.SetStructureVanWithoutSeats;
            ChooseTicketService.ShowNewWay += ShowerStructureVan.SetStrucureWithSeats;
            //SearcherWaysService.UserChooseWay += ShowerStructureVan.SetStrucureWithSeats;
            #endregion

            #region InfoProfile
            SignInService.UserSignOut += InfoProfileService.CurrentUserSignOut;
            SignInService.UserSignIn += InfoProfileService.SetCurrentUser;
            NavigationService.Leave += InfoProfileService.ClearDataWhenLeavePage;
            NavigationService.Enter += InfoProfileService.LoadDataWhenEnteringPage;
            #endregion

            #region NavigationService
            MainMenuControllerService.UserChoosePage += NavigationService.LoadPageWithNotify;
            //SearcherWaysService.UserChooseWay += (obj) => NavigationService.LoadNextPage(Model.Enumerations.TypePage.ChooseTicketPage);
            InfoProfileService.EditExistTrain += obj => NavigationService.LoadPage(TypePage.EditTrainPage);
            EditorTrainService.TrainSaved += () => NavigationService.LoadPageWithNotify(TypePage.ProfilePage);
            ChooseTicketService.ShowNewWay += (obj) => NavigationService.LoadNextPage(TypePage.ChooseTicketPage);
            FillPassengersForTicketService.UserStartFillPassenger += () => NavigationService.LoadNextPage(TypePage.FillPassengerForBuyTicketPage);
            //BuyTicketService.TicketsPurchased += () =>
            //{
            //    NavigationService.ClearHistoryPage();
            //    NavigationService.LoadNextPageWithNotify(TypePage.ProfilePage);
            //};
            NavigationService.SetViewModel(this);
            #endregion

            #region EditorTrain
            InfoProfileService.EditExistTrain += EditorTrainService.EditTrain;
            SignInService.UserSignIn += EditorTrainService.GetUser;
            NavigationService.Leave += EditorTrainService.SetDataWhenUserLeavePage;
            NavigationService.Enter += EditorTrainService.SetDataWhenUserEnterPage;
            #endregion

            #region MainMenuController
            SignInService.UserSignOut += MainMenuControllerService.SetUserSignOut;
            SignInService.UserSignIn += MainMenuControllerService.SetUserSignIn;
            #endregion
        }
        public IEditorTrain EditorTrainService { get; }
        public ISignIn SignInService { get; private set; }
        public IInfoProfile InfoProfileService { get; protected set; }
        public INavigation NavigationService { get; protected set; }
        public IShowerStructureVan ShowerStructureVan { get; }
        public ISearcherWaysService SearcherWaysService { get; }
        public IBuyTicket BuyTicketService { get; }
        public IMainMenuController MainMenuControllerService { get; }
    }
}
