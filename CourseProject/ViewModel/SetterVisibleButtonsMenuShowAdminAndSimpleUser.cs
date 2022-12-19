using CourseProject.Model;
using CourseProject.Model.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.ViewModel
{
    public class SetterVisibleButtonsMenuShowAdminAndSimpleUser : SetterVisibleButtonsMainMenu
    {
        public override void SetVisibleForButtons(MenuShow menuShow, TypeUser type)
        {
            if (type == TypeUser.Worker)
                SetWorkerUserSignIn(menuShow);
            else
                SetSimpleUserSignIn(menuShow);

        }
        protected void SetSimpleUserSignIn(MenuShow menuShow)
        {
            menuShow.VisibleBuyTicket = "Visible";
            menuShow.VisibleCreateTrain = "Visible";
            menuShow.VisibleProfile = "Visible";
            menuShow.VisibleSignIn = "Collapsed";
            menuShow.VisibleSignOut = "Visible";
            menuShow.VisibleSignUp = "Collapsed";
            menuShow.VisibleReport = "Collapsed";
        }
        protected void SetWorkerUserSignIn(MenuShow menuShow)
        {
            menuShow.VisibleBuyTicket = "Visible";
            menuShow.VisibleCreateTrain = "Visible";
            menuShow.VisibleProfile = "Visible";
            menuShow.VisibleSignIn = "Collapsed";
            menuShow.VisibleSignOut = "Visible";
            menuShow.VisibleSignUp = "Collapsed";
            menuShow.VisibleReport = "Visible";
        }
    }
}
