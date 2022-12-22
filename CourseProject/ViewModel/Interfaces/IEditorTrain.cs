using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using CourseProject.Model;
using CourseProject.Model.ModelsForEditingViewStyle;
using DAL;

namespace CourseProject.ViewModel.Interfaces
{
    public interface IEditorTrain
    {
        void SetCreatorTrain(IProcesserDoUndo<Train> processer);
        void SetUser(UserModel user);
        event Action TrainSaved;
        IProcesserDoUndo<Train> ProcesserTrain { get; }
        ICommand StartProcess { get; }
    }
}
