using CourseProject.Model;
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
    /// Логика взаимодействия для TrainEditPage.xaml
    /// </summary>
    public partial class TrainEditPage : Page
    {
        public TrainEditPage(IMediator ViewModelObject)
        {
            InitializeComponent();
            var a = ViewModelObject.ShowerStructureVan.StructureVan;
            rad.ItemsSource = a;
            ((TypeOfVanModelCollection)TryFindResource("CollectionForComboBox")).TypeOfVanModels = ViewModelObject.EditorTrain.TypeOfVanModels;
            ((VanModelCollection)TryFindResource("CollectionForVans")).VanCollection = ViewModelObject.EditorTrain.Vans;
            ((StationTrainScheduleCollection)TryFindResource("CollectionForAddingStationInTrain")).StationSchedule = ViewModelObject.EditorTrain.ModelForEditingScheduleCollection;
            ((StationCollection)TryFindResource("CollectionForStatioinComboBox")).Stations = ViewModelObject.EditorTrain.StationModels;
            ((TimesForStationModelCollection)TryFindResource("CollectionForAddingDateTimeDepartureInTrain")).Collection = ViewModelObject.EditorTrain.DateTimesForDeparture;
            this.DataContext = ViewModelObject;
        }
    }
}
