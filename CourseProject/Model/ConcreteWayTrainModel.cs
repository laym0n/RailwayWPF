using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model
{
    public class ConcreteWayTrainModel
    {
        public StationTrainScheduleModel StartStationTrainScheduleModel { get; set; }
        public TimesForStationModel StartTimesForStationModel { get; set; }
        public StationTrainScheduleModel EndStationTrainScheduleModel { get; set; }
        public TimesForStationModel EndTimesForStationModel { get; set; }
        public ConcreteWayTrainModel() { }
        public ConcreteWayTrainModel(StationTrainScheduleModel startStationTrainScheduleModel, TimesForStationModel startTimesForStationModel, StationTrainScheduleModel endStationTrainScheduleModel, TimesForStationModel endTimesForStationModel)
        {
            StartStationTrainScheduleModel = startStationTrainScheduleModel;
            StartTimesForStationModel = startTimesForStationModel;
            EndStationTrainScheduleModel = endStationTrainScheduleModel;
            EndTimesForStationModel = endTimesForStationModel;
        }
    }
}
