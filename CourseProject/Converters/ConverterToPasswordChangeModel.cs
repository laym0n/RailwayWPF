using CourseProject.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Animation;

namespace CourseProject.Converters
{
    public class ConverterToPasswordChangeModel : IMultiValueConverter
    {
        public object Convert(object[] Values, Type Target_Type, object Parameter, CultureInfo culture)
        {
            var findCommandParameters = new PasswordChangeModel(Values[0] as BehaviourPasswordBox, Values[1] as BehaviourPasswordBox);
            return findCommandParameters;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
