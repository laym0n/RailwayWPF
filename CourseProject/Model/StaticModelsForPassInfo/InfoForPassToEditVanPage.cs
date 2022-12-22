﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model.StaticModelsForPassInfo
{
    public class InfoForPassToEditVanPage
    {
        public List<TypeOfVanModel> TypeOfvanModels { get; set; }
        public ObservableCollection<VanModel> VanModels { get; set; }
    }
}
