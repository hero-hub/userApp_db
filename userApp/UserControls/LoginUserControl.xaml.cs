using System.Windows;
using System.Windows.Controls;
using userApp.Domain.Models;
using userApp.Login;
using userApp.Registration;

namespace userApp.UserControls
{
    public partial class LoginUserControl : UserControl
    {
        DataUserSQLite dataUserSQLite = new DataUserSQLite();

        public LoginUserControl(DataUserSQLite dataUserSQLite)
        {
            InitializeComponent();
            DataContext =  new LoginViewModel(dataUserSQLite);
        }
        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationView regWindow = new RegistrationView(dataUserSQLite);
            regWindow.Show();
            Window.GetWindow(this)?.Close();
            //this.Close(); //not work
        }
    }
}
