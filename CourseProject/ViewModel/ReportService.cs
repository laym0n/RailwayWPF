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
        public ReportService(IUnitOfWork db)
        {
            this.db = db;
        }
        public void SetStrategy(IReportCompileStrategy strategy)
        {
            this.strategy = strategy;
        }
        ReportModel report;
        public ReportModel Report { get => report; }
        public ICommand MakeReport
        {
            get => new RelayCommand((obj) =>
            {
                if(obj is ConcreteWayFromStationToStation way)
                {
                    report = strategy.CompileReport(way);
                    ReportReady?.Invoke();
                }
            });
        }
    }
}
