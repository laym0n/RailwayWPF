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
        void ClearResults();
        void SetStrategySearch(ISearchWayStrategy searchWayStrategy);
        ObservableCollection<ConcreteWayFromStationToStation> PathsFound { get; }
        ICommand FindWays { get; }
        ICommand SetFilters { get; }
    }
}
