using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CourseProject.Model
{
    public class BehaviourPasswordBox : Behavior<PasswordBox>, INotifyPropertyChanged
    {
        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        protected override void OnAttached()
        {
            AssociatedObject.PasswordChanged += ChangePass;
        }
        protected override void OnDetaching()
        {
            AssociatedObject.PasswordChanged -= ChangePass;
        }
        void ChangePass(object obj, RoutedEventArgs e)
        {
            Password = AssociatedObject.Password;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
