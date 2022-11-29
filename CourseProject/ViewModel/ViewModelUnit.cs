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
        public ViewModelUnit(ISignIn signIn, IInfoProfile infoProfile, INavigation navigationService)
        {
            this.SignIn = signIn;
            this.InfoProfile = infoProfile;
            this.NavigationService = navigationService;

            #region SignIn

            #endregion

            #region InfoProfile
            SignIn.UserSignOut += InfoProfile.ClearPassengerCollection;
            #endregion

            #region NavigationService
            SignIn.UserSignOut += NavigationService.SetMainMenuWhenSignOut;
            SignIn.UserSignIn += NavigationService.SetMainMenuWhenSignIn;
            #endregion

        }
        public ISignIn SignIn { get; private set; }
        public IInfoProfile InfoProfile { get; protected set; }
        public INavigation NavigationService { get; protected set; }
    }
}
