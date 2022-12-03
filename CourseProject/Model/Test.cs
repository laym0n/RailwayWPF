using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model
{
    public class Test:INotifyPropertyChanged
    {
        DateTime dateTime = DateTime.Now;
        public DateTime DateTimeTest
        {
            get => dateTime;
            set
            {
                dateTime = value;
                OnPropertyChanged("DateTimeTest");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
