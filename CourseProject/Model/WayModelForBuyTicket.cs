using DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model
{
    public class WayModelForChooseTicket
    {
        public ObservableCollection<List<CellStrucureVanModel>> StructureVanModels { get; set; } = new ObservableCollection<List<CellStrucureVanModel>>();
        public List<Ticket> Tickets { get; set; } = new List<Ticket> ();
        public VanModel VanForShow { get; set; } = null;
        public ConcreteWayTrainModel Way { get; set; }
    }
}
