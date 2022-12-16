using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model
{
    public class ConcreteWayFromStationToStation
    {
        public List<ConcreteWayTrainModel> ConcreteWayTrainModels { get; set; }
        public string NameStartStation { get; set; }
        public string NameEndStation { get; set; }
        public DateTime DepartureTimeStartStation { get; set; }
        public DateTime ArrivingTimeEndStation { get; set; }

    }
}
