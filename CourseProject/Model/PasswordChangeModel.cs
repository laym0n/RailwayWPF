using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseProject.Model
{
    public class PasswordChangeModel
    {
        private BehaviourPasswordBox newPassword;
        private BehaviourPasswordBox oldPassword;
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        public string NewPassword
        {
            get => newPassword.Password;
        }
        public string OldPassword
        {
            get => oldPassword.Password;
        }
        public PasswordChangeModel(BehaviourPasswordBox newPassword, BehaviourPasswordBox oldPassword)
        {
            this.newPassword = newPassword;
            this.oldPassword = oldPassword;
        }
    }
}
