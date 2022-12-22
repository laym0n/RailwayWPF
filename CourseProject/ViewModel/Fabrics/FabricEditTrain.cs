using CourseProject.Model.Enumerations;
using CourseProject.View;
using CourseProject.ViewModel.EditorsTrainDecorators;
using CourseProject.ViewModel.Interfaces;
using DAL;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.ViewModel.Fabrics
{
    public class FabricEditTrain
    {
        IUnitOfWork UnitOfWork;
        public FabricEditTrain(IUnitOfWork db)
        {
            this.UnitOfWork = db;
        }
        public IProcesserDoUndo<Train> GetProcesser(TypeProcesser type)
        {
            IProcesserDoUndo<Train> editVans = new EditorVan(UnitOfWork, null);
            IProcesserDoUndo<Train> editSchedule = new EditorStationTrainSchedule(UnitOfWork, editVans);
            return editSchedule;
        }
    }
}
