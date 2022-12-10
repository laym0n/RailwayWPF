using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model
{
    public class TrainInProfileModel
    {
        public TrainModel TrainModel { get; set; }
        public List<string> Stations { get; set; }
    }
}
