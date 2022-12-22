using CourseProject.Model.Enumerations;
using CourseProject.View;
using CourseProject.ViewModel.EditorsTrainDecorators;
using CourseProject.ViewModel.Fabrics;
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
            IMainMenuController mainMenuController,
            IReportService reportService,

        IGetCollectionsService GetCollectionsService,
        FabricEditTrain fabricEditTrain)
        {
            this.SignInService = signIn;
            this.InfoProfileService = infoProfile;
            this.NavigationService = navigationService;
            this.EditorTrainService = editorTrain;
            this.ShowerStructureVan = showerStructureVan;
            this.SearcherWaysService = searcherWays;
            this.BuyTicketService = buyTicket;
            this.MainMenuControllerService = mainMenuController;
            this.ReportService = reportService;
            this.GetCollectionsService = GetCollectionsService;

            EditorTrainService.SetCreatorTrain(fabricEditTrain.GetProcesser(TypeProcesser.EditAllTrain));

            #region SignIn

            #endregion

            #region ReportService
            #endregion

            #region SearcherWaysService
            NavigationService.Leave += (page) =>
            {
                if (page is SearchWaysForBuyTicketPage)
                    SearcherWaysService.ClearResults();
            };
            NavigationService.Leave += (page) =>
            {
                if (page is SearchWaysForReportPage)
                    SearcherWaysService.ClearResults();
            };
            #endregion

            #region BuyTicket
            SignInService.UserSignIn += BuyTicketService.SetUser;
            #endregion

            #region ShowerStructureVan
            EditorVan.SelectedVanChanged += ShowerStructureVan.SetStructureVanWithoutSeats;
            //EditorTrainService.VanChoosen += ShowerStructureVan.SetStructureVanWithoutSeats;
            ChooseTicketService.ShowNewWay += ShowerStructureVan.SetStrucureWithSeats;
            #endregion

            #region InfoProfile
            //SignInService.UserSignOut += InfoProfileService.CurrentUserSignOut;
            SignInService.UserSignIn += InfoProfileService.SetCurrentUser;
            NavigationService.Leave += (page) =>
            {
                if (page is Profile)
                    InfoProfileService.ClearDate();
            };
            NavigationService.Enter += (page)=> 
            {
                if (page is Profile)
                    InfoProfileService.LoadDataForProfile();
            };
            #endregion

            #region NavigationService
            EditorStationTrainSchedule.StartProcessVans += () => NavigationService.LoadNextPage(TypePage.EditScheduleTrain);
            EditorVan.StartProcessVans += () => NavigationService.LoadNextPage(TypePage.EditVanPage);
            EditorTrainService.CancelCurrentProcess += NavigationService.LoadPreviousPage;
            ReportService.ReportReady += () => NavigationService.LoadNextPage(TypePage.ReportPage);
            ReportService.ShowReportEnded += () => NavigationService.LoadPreviousPage();
            MainMenuControllerService.UserChoosePage += NavigationService.LoadPageWithNotify;
            //InfoProfileService.EditExistTrain += obj => NavigationService.LoadPage(TypePage.EditTrainPage);
            EditorTrainService.TrainSaved += () => NavigationService.LoadPageWithNotify(TypePage.ProfilePage);
            ChooseTicketService.ShowNewWay += (obj) => NavigationService.LoadNextPage(TypePage.ChooseTicketPage);
            FillPassengersForTicketService.UserStartFillPassenger += () => NavigationService.LoadNextPage(TypePage.FillPassengerForBuyTicketPage);
            BuyTicketService.TicketsPurchased += () =>
            {
                SearcherWaysService.ClearResults();
                NavigationService.ClearHistoryPage();
                NavigationService.LoadPage(TypePage.FinalPageBuyTicket);
            };
            BuyTicketService.CancelProcess += NavigationService.LoadPreviousPage;
            NavigationService.SetViewModel(this);
            #endregion

            #region EditorTrain
            //InfoProfileService.EditExistTrain += EditorTrainService.EditTrain;
            SignInService.UserSignIn += EditorTrainService.SetUser;
            //NavigationService.Leave += EditorTrainService.SetDataWhenUserLeavePage;
            //NavigationService.Enter += EditorTrainService.SetDataWhenUserEnterPage;
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
        public IReportService ReportService { get; }
        public IGetCollectionsService GetCollectionsService { get; }
    }
}
