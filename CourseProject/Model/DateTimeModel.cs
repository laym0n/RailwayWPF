using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model
{
    public class DateTimeModel : INotifyPropertyChanged
    {
        private DateTime _date;
        public DateTime DateTime
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged("DateTime");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
