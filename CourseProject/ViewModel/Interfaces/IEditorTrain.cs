using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using CourseProject.Model;
using CourseProject.Model.ModelsForEditingViewStyle;
using DAL;

namespace CourseProject.ViewModel.Interfaces
{
    public interface IEditorTrain
    {
        void EditTrain(TrainModel trainModel);
        void GetUser(User user);
        void SetDataWhenUserEnterPage(Page page);
        void SetDataWhenUserLeavePage(Page page);
        void RemoveTrain(TrainModel trainModel);
        event Action TrainSaved;
        event Action<TypeOfVanModel> VanChoosen;
        ButtonInfoTrainEditPage ButtonInfo { get; }
        List<TypeOfVanModel> TypeOfVanModels { get; }
        ObservableCollection<VanModel> Vans { get; }
        List<StationModel> StationModels { get; }
        ObservableCollection<ModelForEditingSchedule> ModelForEditingScheduleCollection { get; }
        ObservableCollection<TimesForStationModel> DateTimesForDeparture { get; }
        ICommand AddStartTripDateTime { get; }
        ICommand RemoveStartTripDateTime { get; }
        ICommand AddVan { get; }
        ICommand RemoveVan { get; }
        ICommand AddTrain { get; }
    }
}
