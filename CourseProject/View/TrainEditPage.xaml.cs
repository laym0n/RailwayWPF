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
        IMediator ViewModelObject;
        public TrainEditPage(IMediator ViewModelObject)
        {
            InitializeComponent(); 
            this.ViewModelObject = ViewModelObject;
            var a = ViewModelObject.ShowerStructureVan.StructureVan;
            rad.ItemsSource = a;
            ((TypeOfVanModelCollection)TryFindResource("CollectionForComboBox")).TypeOfVanModels = ViewModelObject.EditorTrain.TypeOfVanModels;
            //((TypeOfVanModelCollection)TryFindResource("CollectionForComboBox")).TypeOfVanModelsForTrain = ViewModelObject.EditorTrain.Vans;
            ((VanModelCollection)TryFindResource("CollectionForVans")).VanCollection = ViewModelObject.EditorTrain.Vans;
            this.DataContext = ViewModelObject;
            //this.ViewModelObject = ViewModelObject;
        }
    }
}
