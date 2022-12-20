using CourseProject.Model;
using CourseProject.Model.Collections;
using CourseProject.Model.ModelsForEditingInfo;
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
    /// Логика взаимодействия для FillPassengerForTickets.xaml
    /// </summary>
    public partial class FillPassengerForTickets : Page
    {
        IMediator mediator; 
        public FillPassengerForTickets(IMediator mediator)
        {
            InitializeComponent();
            this.mediator = mediator;
            PassengerItemsControl.ItemsSource = InfoForPassToFillPassengersPage.PassengersForTickets;
            ((PassengerModelObservableCollection)TryFindResource("LoadedPassengers")).Collection = InfoForPassToFillPassengersPage.PassengersInProfile;
            this.DataContext = mediator;
        }
    }
}
