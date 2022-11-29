using CourseProject.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.ViewModel
{
    public class ViewModelUnit : IViewModel
    {
        private ISignIn signIn;
        private IInfoProfile infoProfile;
        public ViewModelUnit(ISignIn signIn, IInfoProfile infoProfile)
        {
            this.signIn = signIn;
            this.infoProfile = infoProfile;
        }
        public ISignIn SignIn 
        {
            get => signIn;
        }
        public IInfoProfile InfoProfile { get => infoProfile; }
    }
}
