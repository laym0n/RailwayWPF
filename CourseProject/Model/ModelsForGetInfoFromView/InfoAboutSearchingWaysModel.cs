using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model.ModelsForGetInfoFromView
{
    public class InfoAboutSearchingWaysModel : INotifyPropertyChanged
    {
        int idStartStation;
        int idEndStation;
        DateTime dateTimeStartStation;
        DateTime dateTimeEndStation;
        public int IdStartStation
        {
            get => idStartStation;
            set
            {
                idStartStation = value;
                OnPropertyChanged("IdStartStation");
            }
        }
        public int IdEndStation
        {
            get => idEndStation;
            set
            {
                idEndStation = value;
                OnPropertyChanged("IdEndStation");
            }
        }
        public DateTime DateTimeStartStation
        {
            get => dateTimeStartStation;
            set
            {
                dateTimeStartStation = value;
                OnPropertyChanged("DateTimeStartStation");
            }
        }
        public DateTime DateTimeEndStation
        {
            get => dateTimeEndStation;
            set
            {
                dateTimeEndStation = value;
                OnPropertyChanged("DateTimeEndStation");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
