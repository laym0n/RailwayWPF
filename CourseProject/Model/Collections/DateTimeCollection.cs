using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model.Collections
{
    public class DateTimeCollection : INotifyPropertyChanged
    {
        private ObservableCollection<DateTimeModel> dateTimes = new ObservableCollection<DateTimeModel>();
        public ObservableCollection<DateTimeModel> DateTimes
        {
            get => dateTimes;
            set
            {
                dateTimes = value;
                OnPropertyChanged("DateTimes");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
