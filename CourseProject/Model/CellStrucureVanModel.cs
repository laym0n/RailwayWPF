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
        public TypeOccupied typeOccupied { get; }
        public CellStrucureVanModel(int? costPerStation, int? numberOfSeatInVan, TypeOccupied typeOccupied)
        {
            this.CostPerStation = costPerStation;
            this.NumberOfSeatInVan = numberOfSeatInVan;
            this.typeOccupied = typeOccupied;
        }
    }
    public enum TypeOccupied
    {
        Occupied,
        Free,
        ReserveForBuy,
        NotSeat
    }
}
