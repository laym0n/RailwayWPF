﻿using CourseProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CourseProject.ViewModel.Interfaces
{
    public interface INavigation
    {
        Frame PageFrame { get; }
        MenuShow VisibleButtons { get; }
        void SetMainMenuWhenSignOut();
        void SetMainMenuWhenSignIn();
        ICommand NavigateBuyTicket { get; }
        ICommand NavigateProfile { get; }
    }
}