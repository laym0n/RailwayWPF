using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProject.Model;
using CourseProject.Model.ModelsForGetInfoFromView;

namespace CourseProject.ViewModel.Interfaces
{
    public interface ISearchWayStrategy
    {
        List<ConcreteWayFromStationToStation> FindWays(InfoAboutSearchingWaysModel info);
    }
}
