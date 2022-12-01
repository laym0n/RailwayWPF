using CourseProject.Model;
using CourseProject.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.ViewModel
{
    public class EditorTrainService : IEditorTrain
    {
        public List<List<SeatViewModel>> MapVan 
        {
            get
            {
                List<SeatViewModel> a = new List<SeatViewModel>() {
                    new SeatViewModel() { Cost = 100, IdVan = 1, Number = 100, Type = SeatViewModel.TypeSeat.free},
                new SeatViewModel() { Cost = 100, IdVan = 1, Number = 100, Type = SeatViewModel.TypeSeat.free},
                new SeatViewModel() { Cost = 100, IdVan = 1, Number = 100, Type = SeatViewModel.TypeSeat.occupied}};
                List<SeatViewModel> b = new List<SeatViewModel>() {
                    new SeatViewModel() { Cost = 100, IdVan = 1, Number = 100, Type = SeatViewModel.TypeSeat.free},
                new SeatViewModel() { Cost = 100, IdVan = 1, Number = 100, Type = SeatViewModel.TypeSeat.free},
                new SeatViewModel() { Cost = 100, IdVan = 1, Number = 100, Type = SeatViewModel.TypeSeat.occupied}};
                List<SeatViewModel> c = new List<SeatViewModel>() {
                    new SeatViewModel() { Cost = 100, IdVan = 1, Number = 100, Type = SeatViewModel.TypeSeat.free},
                new SeatViewModel() { Cost = 100, IdVan = 1, Number = 100, Type = SeatViewModel.TypeSeat.free},
                new SeatViewModel() { Cost = 100, IdVan = 1, Number = 100, Type = SeatViewModel.TypeSeat.occupied}};
                List<List<SeatViewModel>> list = new List<List<SeatViewModel>>();
                list.Add(a);
                list.Add(b);
                list.Add(c);
                return list;
            } 
        }
    }
}
