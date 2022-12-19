using CourseProject.Model;
using CourseProject.Model.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.ViewModel
{
    public abstract class SetterVisibleButtonsMainMenu
    {
        public abstract void SetVisibleForButtons(MenuShow menuShow, TypeUser type);
    }
}
