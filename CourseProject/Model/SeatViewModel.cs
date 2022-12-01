using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model
{
    public class SeatViewModel
    {
        public enum TypeSeat
        {
            free, occupied
        }
        public int Cost { get; set; }
        public TypeSeat Type { get; set; }
        public int? Number { get; set; }
        public int IdVan { get; set; }
        public SeatViewModel() { }
    }
}
