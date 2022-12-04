using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using CourseProject.Model;
using DAL;

namespace CourseProject.ViewModel.Interfaces
{
    public interface ISignIn
    {
        event Action UserSignOut;
        event Action<User> UserSignIn;
        ICommand SignIn { get; }
        ICommand SignUp { get; }
        ICommand SignOut { get; }
    }
}
