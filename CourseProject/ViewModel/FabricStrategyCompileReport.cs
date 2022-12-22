using CourseProject.Model;
using CourseProject.ViewModel.Interfaces;
using CourseProject.ViewModel.StrategyCompileReport;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.ViewModel
{
    public static class FabricStrategyCompileReport
    {
        public static IReportCompileStrategy GetStrateguy(FiltersForStrategyCompileReport filters, IUnitOfWork db)
        {
            IReportCompileStrategy strategy;
            if (filters.SearchPassengersMadeAllWay)
                strategy = new StrategyFindPassengersThatMadeAllWay(db);
            else
                strategy = new StrategyIncludesAnyTicketsInWay(db);
            return strategy;
        }
        public static IReportCompileStrategy GetDefault(IUnitOfWork db)
        {
            return new StrategyIncludesAnyTicketsInWay(db);
        }
    }
}
