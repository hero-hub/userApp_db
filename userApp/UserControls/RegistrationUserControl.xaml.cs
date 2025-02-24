using System.Windows;
using System.Windows.Controls;
using userApp.Domain.Models;
using userApp.Login;
using userApp.Registration;
using userApp.ViewModels;


namespace userApp.UserControls
{
    public partial class RegistrationUserControl : UserControl
    {
        public RegistrationUserControl(MainWindowViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = new RegistrationViewModel(mainViewModel);
        }
    }
}