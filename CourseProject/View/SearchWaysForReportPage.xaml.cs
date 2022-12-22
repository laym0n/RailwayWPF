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

namespace CourseProject.View
{
    /// <summary>
    /// Логика взаимодействия для SearchWaysForReportPage.xaml
    /// </summary>
    public partial class SearchWaysForReportPage : Page
    {
        IMediator mediator;
        public SearchWaysForReportPage(IMediator mediator1)
        {
            InitializeComponent();
            this.mediator = mediator1;
            ((StationModelCollection)TryFindResource("Stations")).Collection = mediator1.GetCollectionsService.StationModels;
            ((ConcreteWayFromStationToStationObservableCollection)TryFindResource("Waysfound")).Collection = mediator1.SearcherWaysService.PathsFound;
            DataContext = mediator1;
        }
    }
}
