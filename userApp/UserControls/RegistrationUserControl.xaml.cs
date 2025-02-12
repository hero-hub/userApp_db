using System.Windows;
using System.Windows.Controls;
using userApp.Login;
using userApp.Registration;

namespace userApp.UserControls
{
    public partial class RegistrationUserControl : UserControl
    {
        public RegistrationUserControl()
        {
            InitializeComponent();
            DataContext = new RegistrationViewModel();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginWindow = new LoginView();
            loginWindow.Show();
            Window.GetWindow(this)?.Close();
            //this.Close();
        }
    }
}