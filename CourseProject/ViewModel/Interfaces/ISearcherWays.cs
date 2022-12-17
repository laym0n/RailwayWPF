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
    public interface ISearcherWays
    {
        event Action<List<WayModelForBuyTicket>> UserChooseWay;
        ObservableCollection<ConcreteWayFromStationToStation> PathsFound { get; }
        ICommand EnterBuyPage { get; }
        ICommand FindWays { get; }
    }
}
