using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CourseProject.ViewModel.Interfaces;
using CourseProject.ViewModel.Tests;
using DAL.Realizations;
using DLL.Interfaces;
using Ninject.Modules;


namespace CourseProject.Util
{
    internal class NinjectRegistrations : NinjectModule
    {
        private readonly Window window;
        public NinjectRegistrations(Window window)
        {
            this.window = window;
        }
        public override void Load()
        {
            //Bind<ISignIn>().To<SignInTest>();
            Bind<ISignIn>().To<SignInService>();
            Bind<IUnityOfWork>().To<DBReposSQLServer>().InSingletonScope();
        }
    }
}
