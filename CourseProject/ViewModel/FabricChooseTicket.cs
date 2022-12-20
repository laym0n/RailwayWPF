using CourseProject.Model.Enumerations;
using CourseProject.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.ViewModel
{
    public class FabricChooseTicket
    {
        public static IChooseTicketService GetChooseTicketService(TypeChooseTicket type)
        {
            return new ChooseTicketService();
        }
    }
}
