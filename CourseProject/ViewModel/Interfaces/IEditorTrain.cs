using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CourseProject.Model;
using DAL;

namespace CourseProject.ViewModel.Interfaces
{
    public interface IEditorTrain
    {
        List<TypeOfVanModel> TypeOfVanModels { get; }
        ObservableCollection<VanModel> Vans { get; }
        List<StationModel> StationModels { get; }
        ObservableCollection<ModelForEditingSchedule> StationTrainScheduleModels { get; }
        ObservableCollection<DateTimeModel> DateTimesForDeparture { get; }
        ICommand AddStartTripDateTime { get; }
        ICommand RemoveStartTripDateTime { get; }
        ICommand AddVan { get; }
        ICommand RemoveVan { get; }
        ICommand AddTrain { get; }
    }
}
