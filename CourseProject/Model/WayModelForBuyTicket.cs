using DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model
{
    public class WayModelForBuyTicket
    {
        public ObservableCollection<List<CellStrucureVanModel>> StrucureVanModels { get; set; }
        public List<Ticket> Tickets { get; set; }
        public VanModel VanForShow { get; set; }
        public ConcreteWayFromStationToStation Way { get; set; }
    }
}
