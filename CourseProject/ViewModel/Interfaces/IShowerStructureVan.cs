﻿using CourseProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.ViewModel.Interfaces
{
    public interface IShowerStructureVan
    {
        List<List<CellStrucureVanModel>> StructureVan { get; }
    }
}
