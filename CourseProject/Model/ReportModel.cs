using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model
{
    public class ReportModel : INotifyPropertyChanged
    {
        ObservableCollection<ConcreteWayFromStationToStation> ways = new ObservableCollection<ConcreteWayFromStationToStation>();
        private int countTickets;
        public ObservableCollection<ConcreteWayFromStationToStation> Ways
        {
            get => ways;
            set
            {
                ways = value;
                OnPropertyChanged("Ways");
            }
        }
        public int CountTickets
        {
            get => countTickets;
            set
            {
                countTickets = value;
                OnPropertyChanged("CountTickets");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
