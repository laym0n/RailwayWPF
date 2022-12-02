using CourseProject.Model;
using CourseProject.ViewModel.Interfaces;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.ViewModel
{
    public class EditorTrainService : IEditorTrain
    {
        private IUnitOfWork db;
        public EditorTrainService(IUnitOfWork db)
        {
            this.db = db;
        }
        
    }
}
