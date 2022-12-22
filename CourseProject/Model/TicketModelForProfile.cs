using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model
{
    public class TicketModelForProfile
    {
        public TicketViewModel TicketViewModel { get; set; }
        public Ticket Ticket { get; set; }
        public TicketModelForProfile() { }
        public TicketModelForProfile(TicketViewModel ticketViewModel, Ticket ticket)
        {
            TicketViewModel = ticketViewModel;
            Ticket = ticket;
        }
    }
}
