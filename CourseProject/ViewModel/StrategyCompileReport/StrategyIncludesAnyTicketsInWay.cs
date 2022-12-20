using CourseProject.Model;
using CourseProject.ViewModel.Interfaces;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.ViewModel.StrategyCompileReport
{
    public class StrategyIncludesAnyTicketsInWay : IReportCompileStrategy
    {
        IUnitOfWork db;
        public StrategyIncludesAnyTicketsInWay(IUnitOfWork db)
        {
            this.db = db;
        }

        public ReportModel CompileReport(ConcreteWayFromStationToStation way)
        {

        }
    }
}
