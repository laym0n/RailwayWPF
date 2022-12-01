using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model
{
    public class SeatViewCollection
    {
        private ObservableCollection<SeatViewModel> _items = new ObservableCollection<SeatViewModel>();
        public ObservableCollection<SeatViewModel> Items { get => _items; }
    }
}
