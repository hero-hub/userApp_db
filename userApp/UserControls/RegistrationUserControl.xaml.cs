using System.Windows;
using System.Windows.Controls;
using userApp.Domain.Models;
using userApp.Login;
using userApp.Registration;


namespace userApp.UserControls
{
    public partial class RegistrationUserControl : UserControl
    {
        DataUserSQLite dataUserSQLite = new DataUserSQLite();
        public RegistrationUserControl(DataUserSQLite dataUserSQLite)
        {
            InitializeComponent();
            DataContext = new RegistrationViewModel(dataUserSQLite);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginWindow = new LoginView(dataUserSQLite);
            loginWindow.Show();
            Window.GetWindow(this)?.Close();
            //this.Close();
        }
    }
}