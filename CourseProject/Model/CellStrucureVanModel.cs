using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model
{
    public class CellStrucureVanModel
    {
        public int? CostPerStation { get; }
        public int? NumberOfSeatInVan { get; }
        public CellStrucureVanModel(int? costPerStation, int? numberOfSeatInVan)
        {
            this.CostPerStation = costPerStation;
            this.NumberOfSeatInVan = numberOfSeatInVan;
        }
    }
}
