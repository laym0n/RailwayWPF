using DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model.StaticModelsForPassInfo
{
    public class InfoForPassToEditStartTimesPage
    {
        public ObservableCollection<TimesForStationModel> timesForStationModels { get; set; }
    }
}
