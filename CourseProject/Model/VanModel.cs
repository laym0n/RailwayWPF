using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model
{
    public class VanModel : INotifyPropertyChanged
    {
        private int id;
        private int trainId;
        private int typeOfVanId;
        private int numberInTrain;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public int TypeOfVanId
        {
            get => typeOfVanId;
            set
            {
                typeOfVanId = value;
                OnPropertyChanged("TypeOfVanId");
            }
        }

        public int TrainId
        {
            get => trainId;
            set
            {
                trainId = value;
                OnPropertyChanged("TrainId");
            }
        }

        public int NumberInTrain
        {
            get => numberInTrain;
            set
            {
                numberInTrain = value;
                OnPropertyChanged("NumberInTrain");
            }
        }
        public Van GetVan() => new Van() { Id = this.id, NumberInTrain = this.numberInTrain, TrainId = this.id, TypeOfVanId = this.typeOfVanId };
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
