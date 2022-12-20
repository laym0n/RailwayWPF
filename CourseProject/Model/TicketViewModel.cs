using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model
{
    public class TicketViewModel
    {
        public PassengerViewModel Passenger { get; set; }
        public ConcreteWayTrainModel Way { get; set; }
    }
}
