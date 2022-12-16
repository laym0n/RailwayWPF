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
    /// Логика взаимодействия для SearchWaysPage.xaml
    /// </summary>
    public partial class SearchWaysPage : Page
    {
        IMediator mediator1;
        public SearchWaysPage(IMediator mediator)
        {
            InitializeComponent();
            this.mediator1 = mediator;
            ((StationModelCollection)TryFindResource("Stations")).Collection = mediator1.EditorTrain.StationModels;
            DataContext = mediator1;
        }
    }
}
