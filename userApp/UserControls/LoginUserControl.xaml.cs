using System.Windows;
using System.Windows.Controls;
using userApp.Login;
using userApp.Registration;

namespace userApp.UserControls
{
    public partial class LoginUserControl : UserControl
    {
        public LoginUserControl()
        {
            InitializeComponent();
            DataContext =  new LoginViewModel();
        }
        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationView regWindow = new RegistrationView();
            regWindow.Show();
            Window.GetWindow(this)?.Close();
            //this.Close(); //not work
        }
    }
}
