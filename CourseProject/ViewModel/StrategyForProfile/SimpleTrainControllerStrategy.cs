using CourseProject.Model;
using CourseProject.ViewModel.Interfaces;
using DAL;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.ViewModel.StrategyForProfile
{
    public class SimpleTrainControllerStrategy : ITrainInProfileStrategy
    {
        IUnitOfWork db;
        UserModel user;
        public SimpleTrainControllerStrategy(IUnitOfWork db)
        {
            this.db = db;
        }
        public void SetUser(UserModel user)
        {
            this.user = user;
        }
        public List<TrainInProfileModel> LoadTrains()
        {
            return (
            from train in db.Train.GetList()
            where train.IdUserCreator == user.Id
            select new TrainInProfileModel
            {
                TrainModel = new TrainModel() { Id = train.Id, IdUserCreator = (int)train.IdUserCreator, LoadedInDB = true },
                Stations = (from stationtrainschedule in db.StationTrainSchedule.GetList()
                            where train.Id == stationtrainschedule.IdTrain
                            join station in db.Station.GetList()
                            on stationtrainschedule.IdStation equals station.Id
                            orderby stationtrainschedule.NumberInTrip
                            select station.Name).ToList()
            }).ToList();
        }
        public void RemoveTrain(ObservableCollection<TrainInProfileModel> collection, TrainInProfileModel train)
        {
            if (collection.FirstOrDefault(i => i == train) == null)
                return;
            collection.Remove(train);
            Train trainForRemove = db.Train.GetItem(train.TrainModel.Id);
            trainForRemove.IdUserCreator = null;
            db.Track.GetList().Where(i => i.TrainId == trainForRemove.Id).ToList().ForEach(i =>
            {
                List<TimesForStation> timesForStations = db.TimesForStation.GetList().Where(j => j.TrackId == i.Id).ToList();
                if(db.Ticket.GetList().All(j=> timesForStations.FirstOrDefault(k=> k.Id == j.IdTimesForStationSource) == null)){
                    db.Track.Delete(i.Id);
                }
            });
            db.Save();
        }
        public void EditTrain(TrainInProfileModel train)
        {

        }
    }
}
