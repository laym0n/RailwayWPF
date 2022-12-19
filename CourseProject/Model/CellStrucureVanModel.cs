using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model
{
    public class CellStrucureVanModel : INotifyPropertyChanged
    {
        private TypeOccupied TypeOccupied;
        public int? CostPerStation { get; }
        public int? NumberOfSeatInVan { get; }
        public TypeOccupied typeOccupied
        {
            get => TypeOccupied;
            set
            {
                TypeOccupied = value;
                OnPropertyChanged("typeOccupied");
            }
        }
        public CellStrucureVanModel(int? costPerStation, int? numberOfSeatInVan, TypeOccupied typeOccupied)
        {
            this.CostPerStation = costPerStation;
            this.NumberOfSeatInVan = numberOfSeatInVan;
            this.typeOccupied = typeOccupied;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
    public enum TypeOccupied
    {
        Occupied,
        Free,
        ReserveForBuy,
        NotSeat
    }
}
