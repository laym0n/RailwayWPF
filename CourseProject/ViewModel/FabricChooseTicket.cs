using CourseProject.Model;
using CourseProject.Model.Enumerations;
using CourseProject.ViewModel.Interfaces;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.ViewModel
{
    public class FabricChooseTicket
    {
        private UserModel user;
        private IUnitOfWork db;
        public FabricChooseTicket(UserModel user, IUnitOfWork db)
        {
            this.user = user;
            this.db = db;
        }

        public IChooseTicketService GetChooseTicketService()
        {
            IChooseTicketService first = new ChooseTicketService();
            IChooseTicketService second = new FillPassengersForTicketService(db, user, first);
            return second;
        }
    }
}
