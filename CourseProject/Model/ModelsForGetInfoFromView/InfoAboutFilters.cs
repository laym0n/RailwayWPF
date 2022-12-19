using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model.ModelsForGetInfoFromView
{
    public class InfoAboutFilters : INotifyPropertyChanged
    {
        private int maxCountTransfers;
        public int MaxCountTransfers
        {
            get => maxCountTransfers;
            set
            {
                maxCountTransfers = value;
                OnPropertyChanged("MaxCountTransfers");
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
