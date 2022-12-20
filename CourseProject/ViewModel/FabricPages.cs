using CourseProject.Model.Enumerations;
using CourseProject.View;
using CourseProject.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CourseProject.ViewModel
{
    public class FabricPages
    {
        IMediator mediator;
        public FabricPages(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public Page GetPage(TypePage typePage)
        {
            Page PageForCreate;
            if (typePage == TypePage.ProfilePage)
                PageForCreate = new Profile(mediator);
            else if (typePage == TypePage.EditTrainPage)
                PageForCreate = new TrainEditPage(mediator);
            else if (typePage == TypePage.ChooseTicketPage)
                PageForCreate = new ChooseSeatsPage(mediator);
            else if (typePage == TypePage.SearchWayPage)
                PageForCreate = new SearchWaysForBuyTicketPage(mediator);
            else if (typePage == TypePage.FillPassengerForBuyTicketPage)
                PageForCreate = new FillPassengerForTickets(mediator);
            else
                PageForCreate = new FinalPageBuyTicket(mediator);
            return PageForCreate;
        }
    }
}
