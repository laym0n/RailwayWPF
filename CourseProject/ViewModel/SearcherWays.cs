using CourseProject.Model;
using CourseProject.Model.ModelsForGetInfoFromView;
using CourseProject.ViewModel.Interfaces;
using DAL;
using DAL.Entities;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CourseProject.ViewModel
{
    public class SearcherWaysService : ISearcherWaysService
    {
        ObservableCollection<ConcreteWayFromStationToStation> pathsFound = new ObservableCollection<ConcreteWayFromStationToStation>();

        public ObservableCollection<ConcreteWayFromStationToStation> PathsFound 
        {
            get => pathsFound; 
        }
        public event Action<List<WayModelForBuyTicket>> UserChooseWay;
        IUnitOfWork db;
        public SearcherWaysService(IUnitOfWork db)
        {
            this.db = db;
        }
        const int MaxCountOfTrainTransfers = 6;
        public ICommand FindWays
        {
            get => new RelayCommand((obj) =>
            {
                if (!(obj is InfoAboutSearchingWaysModel))
                    return;
                //InfoAboutSearchingWaysModel obj = new InfoAboutSearchingWaysModel()
                //{
                //    IdEndStation = 5,
                //    IdStartStation = 1,
                //    DateTimeArriving = new DateTime(2025, 1, 1)
                //};
                pathsFound.Clear();
                NodeForSearchWay NodeForSearch = new NodeForSearchWay()
                {
                    StationEnd = new StationModel() 
                    {
                        Id = (obj as InfoAboutSearchingWaysModel).IdEndStation 
                    },
                    PathsForSearching = new Queue<List<TrainWayWithoutTime>>(),
                    DateTimeArriving = (obj as InfoAboutSearchingWaysModel).DateTimeArriving,
                    CurrentStationAndPathNode = new NodeCurrentStation()
                    {
                        CurrentStation = new StationModel()
                        {
                            Id = (obj as InfoAboutSearchingWaysModel).IdStartStation
                        },
                        CurrentPath = new List<TrainWayWithoutTime>()
                    }
                };
                CheckPathesForStationAndAddFinded(NodeForSearch);
                for (int i = 0;i <= MaxCountOfTrainTransfers; i++)
                {
                    for(int j = NodeForSearch.PathsForSearching.Count - 1; j >= 0 ; j--)
                    {
                        NodeForSearch.CurrentStationAndPathNode = new NodeCurrentStation()
                        {
                            CurrentStation = new StationModel() { Id = NodeForSearch.PathsForSearching.Peek().Last().ToStationSchedule.IdStation },
                            CurrentPath = NodeForSearch.PathsForSearching.Peek()
                        };
                        NodeForSearch.PathsForSearching.Dequeue();
                        CheckPathesForStationAndAddFinded(NodeForSearch);

                    }
                }
                if (pathsFound.Count == 0)
                    MessageBox.Show("Путь не найден!");
            }, (obj) =>
            {
                if (!(obj is InfoAboutSearchingWaysModel))
                    return false;
                InfoAboutSearchingWaysModel model = obj as InfoAboutSearchingWaysModel;
                if (model.IdEndStation == 0 || model.IdStartStation == 0 || model.DateTimeArriving <= DateTime.Now)
                    return false;
                return true;
            });
        }
        public ICommand EnterBuyPage
        {
            get => new RelayCommand((obj) =>
            {
                if(obj is ConcreteWayFromStationToStation concreteWayFromStationToStation)
                {
                    List<WayModelForBuyTicket> parametr = concreteWayFromStationToStation.ConcreteWayTrainModels.Select(i=> new WayModelForBuyTicket() { Way = i}).ToList();
                    UserChooseWay?.Invoke(parametr);
                }
            });
        }
        struct NodeCurrentStation
        {
            public StationModel CurrentStation { get; set; }
            public List<TrainWayWithoutTime> CurrentPath { get; set; }
        }
        class NodeForSearchWay
        {
            public DateTime DateTimeArriving { get; set; }
            public StationModel StationEnd { get; set; }
            public NodeCurrentStation CurrentStationAndPathNode { get; set; }
            public Queue<List<TrainWayWithoutTime>> PathsForSearching { get; set; }
        }
        class TrainWayWithoutTime
        {
            public StationTrainSchedule FromStationSchedule { get; set; }
            public StationTrainSchedule ToStationSchedule { get; set; }
        }
        void CheckPathesForStationAndAddFinded(NodeForSearchWay nodeForSearchWay)
        {
            var WaysFromCurrentStation = (from StationTrainSchedule in db.StationTrainSchedule.GetList()
             where !nodeForSearchWay.CurrentStationAndPathNode.CurrentPath.Any(i => i.ToStationSchedule.IdTrain == StationTrainSchedule.IdTrain)
             group StationTrainSchedule by StationTrainSchedule.IdTrain into AllWaysForCurrentStation
             where AllWaysForCurrentStation.FirstOrDefault(i => i.IdStation == nodeForSearchWay.CurrentStationAndPathNode.CurrentStation.Id) != null
             select AllWaysForCurrentStation into WaysWithCurrentStation
             from StationTrainSchedule in WaysWithCurrentStation
             where StationTrainSchedule.NumberInTrip >= WaysWithCurrentStation.First(i => i.IdStation == nodeForSearchWay.CurrentStationAndPathNode.CurrentStation.Id).NumberInTrip
             orderby StationTrainSchedule.NumberInTrip
             group StationTrainSchedule by StationTrainSchedule.IdTrain
             ).ToList();
            WaysFromCurrentStation.ForEach(Way =>
            {
                CheckWayForFindEndOrNot(nodeForSearchWay, Way);
            });
        }
        void CheckWayForFindEndOrNot(NodeForSearchWay nodeForSearchWay, IGrouping<int, StationTrainSchedule> Way)
        {
            if(Way.FirstOrDefault(i => i.IdStation == nodeForSearchWay.StationEnd.Id) != null)
            {
                List<TrainWayWithoutTime> CurrentPath = new List<TrainWayWithoutTime>(nodeForSearchWay.CurrentStationAndPathNode.CurrentPath);
                CurrentPath.Add(new TrainWayWithoutTime()
                {
                    FromStationSchedule = Way.First(i=>i.IdStation == nodeForSearchWay.CurrentStationAndPathNode.CurrentStation.Id),
                    ToStationSchedule = Way.First(i => i.IdStation == nodeForSearchWay.StationEnd.Id)
                });
                CheckAddFoundPathAndAdd(CurrentPath, nodeForSearchWay.DateTimeArriving);
            }
            else
                AddWaysForSearching(nodeForSearchWay, Way);
        }
        void AddWaysForSearching(NodeForSearchWay nodeForSearchWay, IGrouping<int, StationTrainSchedule> Way)
        {
            List<StationTrainSchedule> ways = Way.ToList();
            for(int i = 1; i < ways.Count; i++)
            {
                List<TrainWayWithoutTime> WayForAdd = new List<TrainWayWithoutTime>(nodeForSearchWay.CurrentStationAndPathNode.CurrentPath);
                WayForAdd.Add(new TrainWayWithoutTime()
                {
                    FromStationSchedule = ways[0],
                    ToStationSchedule = ways[i]
                });
                nodeForSearchWay.PathsForSearching.Enqueue(WayForAdd);
            }
        }
        class TimeForWay
        {
            public Track Track { get; set; }
            public TimesForStation StartTimesForStation { get; set; }
            public TimesForStation EndTimesForStation { get; set; }
            public TimeForWay() { }
            public TimeForWay(Track track, TimesForStation StartTimesForStation, TimesForStation EndTimesForStation)
            {
                Track = track;
                this.StartTimesForStation = StartTimesForStation;
                this.EndTimesForStation = EndTimesForStation;
            }
        }
        void CheckAddFoundPathAndAdd(List<TrainWayWithoutTime> Way, DateTime DateTimeArriving)
        {
            List<TimeForWay> timesForStationsForWay = (
                from tracks in db.Track.GetList()
                where Way.FirstOrDefault(i => i.FromStationSchedule.IdTrain == tracks.TrainId) != null
                join timesForStation in db.TimesForStation.GetList()
                on tracks.Id equals timesForStation.TrackId
                orderby timesForStation.DepartureTime
                group timesForStation by tracks into timesForTrack
                where db.Ticket.GetList().Where(i=>i.IdTimesForStationDestiny == timesForTrack.Key.Id).Count() < 
                db.Van.GetList().Where(i=>i.TrainId == timesForTrack.Key.TrainId).Sum(j=> db.Seat.GetList().Where(i=>i.TypeOfVanId == j.TypeOfVanId).Count())
                select new TimeForWay(timesForTrack.Key,
                timesForTrack.Skip(Way.
                Where(i => i.ToStationSchedule.IdTrain == timesForTrack.Key.TrainId).First().FromStationSchedule.NumberInTrip - 1).First(),
                timesForTrack.Skip(Way.
                Where(i => i.ToStationSchedule.IdTrain == timesForTrack.Key.TrainId).First().ToStationSchedule.NumberInTrip - 1).First()) into TimesBetweenStations
                where TimesBetweenStations.StartTimesForStation.DepartureTime > DateTime.Now && TimesBetweenStations.EndTimesForStation.ArrivalTime <= DateTimeArriving
                orderby TimesBetweenStations.StartTimesForStation.DepartureTime descending
                select TimesBetweenStations
                ).ToList();
            ConcreteWayFromStationToStation FindedWay = FindConcreteWayFromStationToStation(Way, timesForStationsForWay, Way.Count - 1, DateTimeArriving);
            if(FindedWay != null)
            {
                FindedWay.NameStartStation = FindedWay.ConcreteWayTrainModels.First().StartStationModel.Name;
                FindedWay.NameEndStation = FindedWay.ConcreteWayTrainModels.Last().EndStationModel.Name;
                FindedWay.DepartureTimeStartStation = FindedWay.ConcreteWayTrainModels.First().StartTimesForStationModel.DepartureTime;
                FindedWay.ArrivingTimeEndStation = FindedWay.ConcreteWayTrainModels.Last().EndTimesForStationModel.ArrivalTime;
                PathsFound.Add(FindedWay);
            }
        }
        
        ConcreteWayFromStationToStation FindConcreteWayFromStationToStation(List<TrainWayWithoutTime> Way, List<TimeForWay> timeForWays, int IndexInList, DateTime DepartureTimeNextStation)
        {
            if (IndexInList == -1)
                return new ConcreteWayFromStationToStation() { ConcreteWayTrainModels = new List<ConcreteWayTrainModel>() };
            ConcreteWayFromStationToStation FindedWay = null;
            bool needContinueSearching = true;
            timeForWays.Where(i => i.Track.TrainId == Way[IndexInList].ToStationSchedule.IdTrain && i.EndTimesForStation.ArrivalTime < DepartureTimeNextStation).ToList().ForEach(i =>
            {
                if (!needContinueSearching)
                    return;
                FindedWay = FindConcreteWayFromStationToStation(Way, timeForWays, IndexInList - 1, i.StartTimesForStation.DepartureTime);
                if (FindedWay != null)
                {
                    FindedWay.ConcreteWayTrainModels.Add(new ConcreteWayTrainModel()
                    {
                        EndStationModel = new StationModel(db.Station.GetItem(Way[IndexInList].ToStationSchedule.IdStation)),
                        StartStationModel = new StationModel(db.Station.GetItem(Way[IndexInList].FromStationSchedule.IdStation)),
                        StartStationTrainScheduleModel = new StationTrainScheduleModel( Way[IndexInList].FromStationSchedule),
                        EndStationTrainScheduleModel = new StationTrainScheduleModel(Way[IndexInList].ToStationSchedule),
                        StartTimesForStationModel = new TimesForStationModel(i.StartTimesForStation),
                        EndTimesForStationModel = new TimesForStationModel(i.EndTimesForStation)
                    });
                    needContinueSearching = false;
                    return;
                }
            });
            return FindedWay;
        }
    }
}
