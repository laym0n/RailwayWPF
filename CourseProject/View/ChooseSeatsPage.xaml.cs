using CourseProject.Model.StaticModelsForPassInfo;
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
    /// Логика взаимодействия для ChooseSeatsPage.xaml
    /// </summary>
    public partial class ChooseSeatsPage : Page
    {
        IMediator ViewModel;
        public ChooseSeatsPage(IMediator ViewModelObject)
        {
            InitializeComponent();
            this.ViewModel = ViewModelObject;
            var a = ViewModel.ShowerStructureVan.StructureVansWithSeats;
            rad.ItemsSource = a;
            DataContext = ViewModelObject;
        }
    }
}
