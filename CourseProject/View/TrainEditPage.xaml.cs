using CourseProject.Model;
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
        public TrainEditPage(/*IMediator mediator*/)
        {
            InitializeComponent(); 
            List<SeatViewModel> a = new List<SeatViewModel>() {
                    new SeatViewModel() { Cost = 100, IdVan = 1, Number = 1, Type = SeatViewModel.TypeSeat.free},
                new SeatViewModel() { Cost = 100, IdVan = 1, Number = 1, Type = SeatViewModel.TypeSeat.free},
                new SeatViewModel() { Cost = 100, IdVan = 1, Number = 1, Type = SeatViewModel.TypeSeat.occupied}};
            List<SeatViewModel> b = new List<SeatViewModel>() {
                    new SeatViewModel() { Cost = 100, IdVan = 1, Number = 1, Type = SeatViewModel.TypeSeat.free},
                new SeatViewModel() { Cost = 100, IdVan = 1, Number = 1, Type = SeatViewModel.TypeSeat.free},
                new SeatViewModel() { Cost = 100, IdVan = 1, Number = 1, Type = SeatViewModel.TypeSeat.occupied}};
            List<SeatViewModel> c = new List<SeatViewModel>() {
                    new SeatViewModel() { Cost = 100, IdVan = 1, Number = 1, Type = SeatViewModel.TypeSeat.free},
                new SeatViewModel() { Cost = 100, IdVan = 1, Number = 1, Type = SeatViewModel.TypeSeat.free},
                new SeatViewModel() { Cost = 100, IdVan = 1, Number = 1, Type = SeatViewModel.TypeSeat.occupied}};
            List<List<SeatViewModel>> list = new List<List<SeatViewModel>>();
            List<SeatViewModel> d = new List<SeatViewModel>() {
                    new SeatViewModel() { Cost = 100, IdVan = 1, Number = 1, Type = SeatViewModel.TypeSeat.free},
                new SeatViewModel() { Cost = 100, IdVan = 1, Number = 1, Type = SeatViewModel.TypeSeat.free},
                new SeatViewModel() { Cost = 100, IdVan = 1, Number = 1, Type = SeatViewModel.TypeSeat.occupied}};
            List<SeatViewModel> e = new List<SeatViewModel>() {
                    new SeatViewModel() { Cost = 100, IdVan = 1, Number = 1, Type = SeatViewModel.TypeSeat.free},
                new SeatViewModel() { Cost = 100, IdVan = 1, Number = 1, Type = SeatViewModel.TypeSeat.free},
                new SeatViewModel() { Cost = 100, IdVan = 1, Number = 1, Type = SeatViewModel.TypeSeat.occupied}};
            list.Add(a);
            list.Add(b);
            list.Add(c);
            list.Add(d);
            list.Add(e);
            rad.ItemsSource = list;
            //this.ViewModelObject = mediator;
        }
    }
}
