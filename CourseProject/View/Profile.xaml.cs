using CourseProject.Model;
using CourseProject.Model.Collections;
using CourseProject.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        IMediator ViewModel;
        public Profile(IMediator viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = viewModel;
            ((PassengerProfileCollection)TryFindResource("Passengers")).PassengerCollection = ViewModel.InfoProfile.PassengerViewModels;
            ((TrainInProfileModelCollection)TryFindResource("Trains")).Collection = ViewModel.InfoProfile.TrainInProfileModels;
        }

    }
}
