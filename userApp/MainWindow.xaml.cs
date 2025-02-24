using System.Windows;
using userApp.Registration;
using userApp.Login;
using userApp.Domain.Models;
using userApp.ViewModels;

namespace userApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}