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
        public ViewModelUnit(ISignIn signIn, IInfoProfile infoProfile, INavigation navigationService, IEditorTrain editorTrain, IShowerStructureVan showerStructureVan, ISearcherWays searcherWays, IBuyTicket buyTicket)
        {
            this.SignIn = signIn;
            this.InfoProfile = infoProfile;
            this.NavigationService = navigationService;
            this.EditorTrain = editorTrain;
            this.ShowerStructureVan = showerStructureVan;
            this.SearcherWays = searcherWays;
            this.BuyTicket = buyTicket;

            #region SignIn

            #endregion

            #region BuyTicket

            #endregion

            #region ShowerStructureVan
            EditorTrain.VanChoosen += ShowerStructureVan.SetStructureVanWithoutSeats;
            SearcherWays.UserChooseWay += ShowerStructureVan.SetStrucureWithSeats;
            #endregion

            #region InfoProfile
            SignIn.UserSignOut += InfoProfile.CurrentUserSignOut;
            SignIn.UserSignIn += InfoProfile.SetCurrentUser;
            NavigationService.Leave += InfoProfile.ClearDataWhenLeavePage;
            NavigationService.Enter += InfoProfile.LoadDataWhenEnteringPage;
            #endregion

            #region NavigationService
            SearcherWays.UserChooseWay += (obj) => NavigationService.LoadPageBuyTicket();
            SignIn.UserSignOut += NavigationService.SetMainMenuWhenSignOut;
            SignIn.UserSignIn += (obj) => NavigationService.SetMainMenuWhenSignIn();
            InfoProfile.EditExistTrain += obj => NavigationService.LoadTrainEditPageForEditTrain();
            EditorTrain.TrainSaved += NavigationService.LoadPageAfterSaveTrain;
            NavigationService.ViewModel = this;
            #endregion

            #region EditorTrain
            InfoProfile.EditExistTrain += EditorTrain.EditTrain;
            SignIn.UserSignIn += EditorTrain.GetUser;
            NavigationService.Leave += EditorTrain.SetDataWhenUserLeavePage;
            NavigationService.Enter += EditorTrain.SetDataWhenUserEnterPage;
            #endregion
        }
        public IEditorTrain EditorTrain { get; }
        public ISignIn SignIn { get; private set; }
        public IInfoProfile InfoProfile { get; protected set; }
        public INavigation NavigationService { get; protected set; }
        public IShowerStructureVan ShowerStructureVan { get; }
        public ISearcherWays SearcherWays { get; }
        public IBuyTicket BuyTicket { get; }
    }
}
