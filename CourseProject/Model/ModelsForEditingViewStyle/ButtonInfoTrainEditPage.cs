using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model.ModelsForEditingViewStyle
{
    public class ButtonInfoTrainEditPage
    {
        public bool IsEnabledButtonForAddVan { get; set; }
        public bool IsEnabledButtonForAddStationInSchedule { get; set; }
        public bool IsEnabledButtonForSave { get; set; }
        public string ToolTipButtonForSave { get; set; }
    }
}
