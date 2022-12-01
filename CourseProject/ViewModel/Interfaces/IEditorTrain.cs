using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProject.Model;

namespace CourseProject.ViewModel.Interfaces
{
    public interface IEditorTrain
    {
        List<List<SeatViewModel>> MapVan { get; }
    }
}
