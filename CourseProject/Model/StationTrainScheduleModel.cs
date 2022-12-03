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
    public class StationTrainScheduleModel : INotifyPropertyChanged
    {
        private int id;
        private int idStation;
        private int idTrain;
        private int numberInTrip;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public int IdStation
        {
            get => idStation;
            set
            {
                idStation = value;
                OnPropertyChanged("IdStation");
            }
        }

        public int IdTrain
        {
            get => idTrain;
            set
            {
                idTrain = value;
                OnPropertyChanged("IdTrain");
            }
        }

        public int NumberInTrip
        {
            get => numberInTrip;
            set
            {
                numberInTrip = value;
                OnPropertyChanged("NumberInTrip");
            }
        }
        public StationTrainScheduleModel(StationTrainSchedule item)
        {
            this.id = item.Id;
            this.idStation = item.IdStation;
            this.idTrain = item.IdTrain;
            this.numberInTrip = item.NumberInTrip;
        }
        public StationTrainScheduleModel() { }
        public StationTrainSchedule GetStationTrainSchedule() => new StationTrainSchedule() { Id = this.Id, IdTrain = this.idTrain, IdStation = this.idStation, NumberInTrip = this.numberInTrip };
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
