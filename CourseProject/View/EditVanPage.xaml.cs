using CourseProject.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseProject.View
{
    /// <summary>
    /// Логика взаимодействия для EditVanPage.xaml
    /// </summary>
    public partial class EditVanPage : Page
    {
        public EditVanPage(IMediator mediator)
        {
            InitializeComponent();
            rad.ItemsSource = mediator.ShowerStructureVan.StructureVanWithoutSeats;
            DataContext = mediator;
        }
    }
}
