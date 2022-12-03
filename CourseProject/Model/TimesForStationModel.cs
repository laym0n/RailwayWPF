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
    public class TimesForStationModel : INotifyPropertyChanged
    {
        private int id;
        private DateTime departureTime;
        private DateTime arrivalTime;
        private int idStation;
        private int idStationTrainSchedule;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public DateTime DepartureTime
        {
            get => departureTime;
            set
            {
                departureTime = value;
                OnPropertyChanged("DepartureTime");
            }
        }

        public int IdStationTrainSchedule
        {
            get => idStationTrainSchedule;
            set
            {
                idStationTrainSchedule = value;
                OnPropertyChanged("IdStationTrainSchedule");
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
        public DateTime ArrivalTime
        {
            get => arrivalTime;
            set
            {
                arrivalTime = value;
                OnPropertyChanged("ArrivalTime");
            }
        }
        public TimesForStationModel(TimesForStation item)
        {
            this.idStation = item.IdStation;
            this.arrivalTime = item.ArrivalTime;
            this.departureTime = item.DepartureTime;
            this.id = item.Id;
            this.idStationTrainSchedule = item.IdStationTrainSchedule;
        }
        public TimesForStationModel() { }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
