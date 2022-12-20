using CourseProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseProject.ViewModel.Interfaces
{
    public interface ISearcherWaysService
    {
        //event Action<List<WayModelForChooseTicket>> UserChooseWay;
        void SetStrategySearch(ISearchWayStrategy searchWayStrategy);
        ObservableCollection<ConcreteWayFromStationToStation> PathsFound { get; }
        ICommand FindWays { get; }
        ICommand SetFilters { get; }
    }
}
