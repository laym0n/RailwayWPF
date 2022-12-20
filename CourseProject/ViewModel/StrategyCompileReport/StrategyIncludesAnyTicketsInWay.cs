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
            ReportModel reportModel = new ReportModel();
            db.Ticket.GetList().ForEach(ticket =>
            {
                PassengerViewModel Passenger = new PassengerViewModel(db.Passengers.GetItem(ticket.PassengerId), true);
                way.ConcreteWayTrainModels.ForEach(i =>
                {
                    if (i.StartTimesForStationModel.Id == ticket.IdTimesForStationSource && i.EndTimesForStationModel.Id == ticket.IdTimesForStationDestiny)
                        reportModel.Tickets.Add(new TicketViewModel()
                        {
                            Passenger = Passenger,
                            Way = i
                        });
                });
            });
            return reportModel;
        }
    }
}
