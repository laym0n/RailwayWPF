using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProject.ViewModel.Interfaces;
using CourseProject.ViewModel.Tests;
using Ninject.Modules;

namespace CourseProject.Util
{
    internal class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<ISignIn>().To<SignInTest>();
        }
    }
}
