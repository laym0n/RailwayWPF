﻿using CourseProject.Model;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseProject.ViewModel.Interfaces
{
    public interface IChooseTicketService
    {
        event Action<List<Ticket>> UserChooseTicket;
        void SetConcreteWayFromStationToStation(List<WayModelForChooseTicket> way);
        ICommand ChooseTicket { get; }
        ICommand СompleteChoose { get; }
    }
}
