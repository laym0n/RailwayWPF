using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model
{
    public class FiltersForStrategyCompileReport
    {
        public bool SearchAllTickets { get; set; } = true;
        public bool SearchPassengersMadeAllWay { get; set; } = false;
    }
}
