using CourseProject.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CourseProject.Converters
{
    public class ConverterForColorSeat : IValueConverter
    {
        public object Convert(object value, Type Target_Type, object Parameter, CultureInfo culture)
        {
            TypeOccupied typeOccupied = (TypeOccupied)value;
            if (typeOccupied == TypeOccupied.NotSeat)
                return "White";
            else if (typeOccupied == TypeOccupied.Occupied)
                return "Crimson";
            else if (typeOccupied == TypeOccupied.Free)
                return "Green";
            else
                return "Yellow";
        }
        public object ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return parameter;
        }
    }
}
