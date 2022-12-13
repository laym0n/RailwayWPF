﻿using CourseProject.Model;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.ViewModel.Interfaces
{
    public interface IShowerStructureVan
    {
        void SetStructureVanWithoutSeats(TypeOfVanModel van);
        ObservableCollection<List<CellStrucureVanModel>> StructureVanWithoutSeats { get; }
    }
}
