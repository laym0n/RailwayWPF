using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseProject.Model.ModelsForEditingViewStyle
{
    public class EnableButtonsOnTrainEditPage : DependencyObject
    {
        public static readonly DependencyProperty IsEnableProperty =
        DependencyProperty.Register("IsEnable", typeof(bool),
        typeof(EnableButtonsOnTrainEditPage));
        public bool IsEnable
        {
            get { return (bool)GetValue(IsEnableProperty); }
            set { SetValue(IsEnableProperty, value); }
        }

        public static EnableButtonsOnTrainEditPage Instance { get; private set; }

        static EnableButtonsOnTrainEditPage()
        {
            Instance = new EnableButtonsOnTrainEditPage();
        }
    }
}
