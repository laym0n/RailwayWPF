using CourseProject.Util;
using CourseProject.View;
using CourseProject.ViewModel.Interfaces;
using MaterialDesignThemes.Wpf;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly StandardKernel kernel;
        private readonly IMediator viewModel;
        public MainWindow()
        {
            InitializeComponent();
            kernel = new StandardKernel(new NinjectRegistrations(this));
            this.viewModel = kernel.Get<IMediator>();
            this.DataContext = viewModel;
            NavigableFrame.Navigate(new SearchWaysPage());
        }
    }
}
