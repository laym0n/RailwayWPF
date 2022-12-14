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
        private int trackId;
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

        public int TrackId
        {
            get => trackId;
            set
            {
                trackId = value;
                OnPropertyChanged("TrackId");
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
            this.arrivalTime = item.ArrivalTime;
            this.departureTime = item.DepartureTime;
            this.id = item.Id;
            this.trackId = item.TrackId;
        }
        public bool loadedInDb = false;
        public TimesForStationModel() { }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
    public class TimesForStationModelComparer : IEqualityComparer<TimesForStationModel>
    {
        public bool Equals(TimesForStationModel x, TimesForStationModel y)
        {
            if (Object.ReferenceEquals(x, y)) return true;
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;
            return x.loadedInDb && y.loadedInDb && x.Id == y.Id;
        }
        public int GetHashCode(TimesForStationModel x)
        {
            if (Object.ReferenceEquals(x, null)) return 0;
            return x.GetHashCode();
        }
    }
}
