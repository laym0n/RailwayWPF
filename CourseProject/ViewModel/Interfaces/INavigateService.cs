﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CourseProject.ViewModel.Interfaces
{
    public interface INavigateService
    {
        Frame PageFrame { get; }

        ICommand NavigateBuyTicket { get; }
        ICommand NavigateProfile { get; }
    }
}
