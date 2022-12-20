using CourseProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseProject.ViewModel.Interfaces
{
    public interface IReportService
    {
        event Action ReportReady;
        void SetStrategy(IReportCompileStrategy strategy);
        ReportModel Report { get; }
        ICommand MakeReport { get; }
    }
}
