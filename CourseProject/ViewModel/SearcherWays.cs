using CourseProject.Model;
using CourseProject.Model.ModelsForGetInfoFromView;
using CourseProject.ViewModel.Interfaces;
using DAL;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CourseProject.ViewModel
{
    public class SearcherWays : ISearcherWays
    {
        ObservableCollection<ConcreteWayFromStationToStation> pathsFound = new ObservableCollection<ConcreteWayFromStationToStation>();

        public ObservableCollection<ConcreteWayFromStationToStation> PathsFound 
        {
            get => pathsFound; 
        }
        IUnitOfWork db;
        public SearcherWays(IUnitOfWork db)
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
                NodeForSearchWay NodeForSearch = new NodeForSearchWay()
                {
                    StationEnd = new StationTrainScheduleModel(db.StationTrainSchedule.GetItem((obj as InfoAboutSearchingWaysModel).IdEndStation)),
                    PathsForSearching = new Queue<ConcreteWayFromStationToStation>(),
                    PathsFound = new List<ConcreteWayFromStationToStation>(),
                    CurrentStationAndPathNode = new NodeCurrentStation()
                    {
                        CurrentStation = new StationModel()
                        {
                            Id = new StationTrainScheduleModel(db.StationTrainSchedule.GetItem((obj as InfoAboutSearchingWaysModel).IdStartStation)).IdStation
                        },
                        CurrentPath = null
                    }
                };
                FindPath(NodeForSearch);
                for (int i = 0;i <= MaxCountOfTrainTransfers; i++)
                {
                    for(int j = NodeForSearch.PathsForSearching.Count - 1; j >= 0 ; j--)
                    {
                        NodeForSearch.CurrentStationAndPathNode = new NodeCurrentStation()
                        {
                            CurrentStation = new StationModel() { Id = NodeForSearch.PathsForSearching.Peek().ConcreteWayTrainModels.Last().EndStationTrainScheduleModel.IdStation },
                            CurrentPath = NodeForSearch.PathsForSearching.Peek()
                        };
                        NodeForSearch.PathsForSearching.Dequeue();
                        FindPath(NodeForSearch);

                    }
                }
                if (NodeForSearch.PathsFound.Count == 0)
                    MessageBox.Show("Путь не найден!");
                else
                {
                    pathsFound.Clear();
                    NodeForSearch.PathsFound.ForEach(i=>pathsFound.Add(i));
                }
            });
        }
        struct NodeCurrentStation
        {
            public StationModel CurrentStation { get; set; }
            public ConcreteWayFromStationToStation CurrentPath { get; set; }
        }
        struct NodeForSearchWay
        {
            public StationTrainScheduleModel StationEnd { get; set; }
            public NodeCurrentStation CurrentStationAndPathNode { get; set; }
            public List<ConcreteWayFromStationToStation> PathsFound { get; set; }
            public Queue<ConcreteWayFromStationToStation> PathsForSearching { get; set; }
        }
        void FindPath(NodeForSearchWay nodeForSearchWay)
        {
            var Ways = (from StationTrainSchedule in db.StationTrainSchedule.GetList()
             where nodeForSearchWay.CurrentStationAndPathNode.CurrentPath.ConcreteWayTrainModels.FirstOrDefault(i => i.StartStationTrainScheduleModel.IdTrain == StationTrainSchedule.IdTrain) == null
             group StationTrainSchedule by StationTrainSchedule.IdTrain into AllWaysForcurrentStation
             where AllWaysForcurrentStation.FirstOrDefault(i => i.IdStation == nodeForSearchWay.CurrentStationAndPathNode.CurrentStation.Id) != null
             select AllWaysForcurrentStation into WaysWithCurrentStation
             from StationTrainSchedule in WaysWithCurrentStation
             where StationTrainSchedule.NumberInTrip > WaysWithCurrentStation.First(i => i.IdStation == nodeForSearchWay.CurrentStationAndPathNode.CurrentStation.Id).NumberInTrip
             group StationTrainSchedule by StationTrainSchedule.IdTrain
             //select Ways.Where(i=> i.stationTrainSchedule.NumberInTrip > 
             //Ways.FirstOrDefault(j => j.stationTrainSchedule.IdStation == nodeForSearchWay.CurrentStationAndPathNode.CurrentStation.Id).stationTrainSchedule.NumberInTrip).ToList()
             ).ToList();
            
        }
    }
}
