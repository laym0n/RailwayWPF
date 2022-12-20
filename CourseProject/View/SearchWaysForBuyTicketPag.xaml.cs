using CourseProject.Model.Collections;
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

namespace CourseProject
{
    /// <summary>
    /// Логика взаимодействия для SearchWaysForBuyTicketPage.xaml
    /// </summary>
    public partial class SearchWaysForBuyTicketPage : Page
    {
        IMediator mediator1;
        public SearchWaysForBuyTicketPage(IMediator mediator)
        {
            InitializeComponent();
            this.mediator1 = mediator;
            ((StationModelCollection)TryFindResource("Stations")).Collection = mediator1.EditorTrainService.StationModels;
            ((ConcreteWayFromStationToStationObservableCollection)TryFindResource("Waysfound")).Collection = mediator1.SearcherWaysService.PathsFound;
            DataContext = mediator1;
        }
    }
}
