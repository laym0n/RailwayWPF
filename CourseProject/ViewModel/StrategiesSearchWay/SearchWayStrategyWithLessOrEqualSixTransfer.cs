using CourseProject.Model.ModelsForGetInfoFromView;
using CourseProject.Model;
using CourseProject.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.ViewModel.StrategiesSearchWay
{
    public class SearchWayStrategyWithLessOrEqualSixTransfer : ISearchWayStrategy
    {

        public List<ConcreteWayFromStationToStation> FindWays(InfoAboutSearchingWaysModel info)
        {
            return null;
        }
    }
}
