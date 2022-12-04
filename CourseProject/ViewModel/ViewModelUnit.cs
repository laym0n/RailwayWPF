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
        public ViewModelUnit(ISignIn signIn, IInfoProfile infoProfile, INavigation navigationService, IEditorTrain editorTrain, IShowerStructureVan showerStructureVan)
        {
            this.SignIn = signIn;
            this.InfoProfile = infoProfile;
            this.NavigationService = navigationService;
            this.EditorTrain = editorTrain;
            this.ShowerStructureVan = showerStructureVan;

            #region SignIn

            #endregion

            #region InfoProfile
            SignIn.UserSignOut += InfoProfile.CurrentUserSignOut;
            SignIn.UserSignIn += InfoProfile.SetCurrentUser;
            NavigationService.Leave += InfoProfile.ClearUnSavedDataWhenUserLeavePage;
            #endregion

            #region NavigationService
            SignIn.UserSignOut += NavigationService.SetMainMenuWhenSignOut;
            SignIn.UserSignIn += (obj) => NavigationService.SetMainMenuWhenSignIn();
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
    }
}
