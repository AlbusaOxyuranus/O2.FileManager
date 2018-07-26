using System;
using System.Windows;
using O2.FileManager.Helpers;
using O2.FileManager.ViewModels;

namespace O2.FileManager.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            
        }
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            DataContext.As<MainViewModel>().LeftDiskViewModel.Items = DiskViewModel.GetDisks();
            DataContext.As<MainViewModel>().RigthDiskViewModel.Items = DiskViewModel.GetDisks();
        }

       
    }
}