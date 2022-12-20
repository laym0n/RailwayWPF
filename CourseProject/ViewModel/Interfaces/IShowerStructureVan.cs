using CourseProject.Model;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseProject.ViewModel.Interfaces
{
    public interface IShowerStructureVan
    {
        ICommand ShowNextVan { get; }
        ICommand ShowPreviousVan { get; }
        void SetStructureVanWithoutSeats(TypeOfVanModel van);
        void SetStrucureWithSeats(List<WayModelForChooseTicket> way);
        ObservableCollection<List<CellStrucureVanModel>> StructureVanWithoutSeats { get; }
        ObservableCollection<List<List<CellStrucureVanModel>>> StructureVansWithSeats { get; }
    }
}
