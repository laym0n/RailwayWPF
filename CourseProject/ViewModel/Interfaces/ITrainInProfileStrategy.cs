using CourseProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.ViewModel.Interfaces
{
    public interface ITrainInProfileStrategy
    {
        void SetUser(UserModel user);
        List<TrainInProfileModel> LoadTrains();
        void RemoveTrain(ObservableCollection<TrainInProfileModel> collection, TrainInProfileModel train);
        void EditTrain(TrainInProfileModel train);
    }
}
