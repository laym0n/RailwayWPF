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
        IUnitOfWork db;
        ISearchWayStrategy searchWayStrategy;
        public void SetStrategySearch(ISearchWayStrategy searchWayStrategy)
        {
            this.searchWayStrategy = searchWayStrategy;
        }
        async Task FindingWays(InfoAboutSearchingWaysModel info)
        {
            List<ConcreteWayFromStationToStation> foundWays = new List<ConcreteWayFromStationToStation>();
            await Task.Run(() =>
            {
                foundWays = searchWayStrategy.FindWays(info);
            });
            
            if (foundWays.Count == 0)
                MessageBox.Show("Путь не найден!");
            else
            {
                pathsFound.Clear();
                foundWays.ForEach(i => pathsFound.Add(i));
            }
        }
        public SearcherWaysService(IUnitOfWork db, ISearchWayStrategy searchWayStrategy)
        {
            this.db = db;
            this.searchWayStrategy = searchWayStrategy;
        }
        public ICommand FindWays
        {
            get => new RelayCommand(async (obj) =>
            {
                if (!(obj is InfoAboutSearchingWaysModel))
                    return;
                await FindingWays(obj as InfoAboutSearchingWaysModel);
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
        public ICommand SetFilters
        {
            get => new RelayCommand((obj) =>
            {
                if (obj is InfoAboutFilters)
                {
                    FabricStrategySearchWay fabric = new FabricStrategySearchWay(db);
                    searchWayStrategy = fabric.GetNewStrategy(obj as InfoAboutFilters);
                }
            });
        }
        
    }
}
