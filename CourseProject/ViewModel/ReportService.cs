using CourseProject.Model;
using CourseProject.ViewModel.Interfaces;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseProject.ViewModel
{
    public class ReportService : IReportService
    {
        IReportCompileStrategy strategy;
        IUnitOfWork db;
        public event Action ReportReady;
        public event Action ShowReportEnded;
        public ReportService(IUnitOfWork db)
        {
            this.db = db;
            this.strategy = FabricStrategyCompileReport.GetDefault(db);
        }
        public ICommand SetStrategy
        {
            get => new RelayCommand((obj) =>
            {
                if (obj is FiltersForStrategyCompileReport filters)
                {
                    strategy = FabricStrategyCompileReport.GetStrateguy(filters, db);
                    if(wayForReport != null)
                    {
                        ReportModel newReport = strategy.CompileReport(wayForReport);
                        report.Tickets.Clear();
                        newReport.Tickets.ToList().ForEach(i => report.Tickets.Add(i));
                    }
                }
            });
        }
        ConcreteWayFromStationToStation wayForReport;
        ReportModel report;
        public ReportModel Report { get => report; }
        public ICommand MakeReport
        {
            get => new RelayCommand((obj) =>
            {
                if(obj is ConcreteWayFromStationToStation way)
                {
                    wayForReport = way;
                    report = strategy.CompileReport(way);
                    ReportReady?.Invoke();
                }
            });
        }
        public ICommand GoBack
        {
            get => new RelayCommand((obj) =>
            {
                ShowReportEnded?.Invoke();
                report.Tickets.Clear();
                wayForReport = null;
            });
        }
    }
}
