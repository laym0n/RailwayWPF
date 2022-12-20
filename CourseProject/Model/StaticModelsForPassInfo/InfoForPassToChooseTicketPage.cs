using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model.StaticModelsForPassInfo
{
    public static class InfoForPassToChooseTicketPage
    {
        public static ObservableCollection<WayModelForChooseTicket> ways { get; } = new ObservableCollection<WayModelForChooseTicket>();
    }
}
