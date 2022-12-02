﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CourseProject.ViewModel;
using CourseProject.ViewModel.Interfaces;
using CourseProject.ViewModel.Tests;
using DAL.Realizations;
using DLL.Interfaces;
using Ninject.Modules;


namespace CourseProject.Util
{
    internal class NinjectRegistrations : NinjectModule
    {
        private readonly MainWindow window;
        public NinjectRegistrations(MainWindow window)
        {
            this.window = window;
        }
        public override void Load()
        {
            Bind<IShowerStructureVan>().To<ShowStrucureVanService>();
            Bind<ISignIn>().To<SignInTest>().InSingletonScope();
            Bind<IEditorTrain>().To<EditorTrainService>().InSingletonScope();
            //Bind<ISignIn>().To<SignInService>().InSingletonScope();
            Bind<INavigation>().To<NavigationService>().InSingletonScope().WithConstructorArgument<Frame>(window.NavigableFrame);
            Bind<IInfoProfile>().To<ProfileService>().InSingletonScope();
            Bind<IUnitOfWork>().To<DBReposSQLServer>().InSingletonScope();
            Bind<IMediator>().To<ViewModelUnit>().InSingletonScope();
        }
    }
}
