using System.Windows;
using userApp.Registration;
using userApp.Login;
using userApp.Domain.Models;

namespace userApp
{
    public partial class StartWindow : Window
    {
        DataUserSQLite dataUserSQLite = new DataUserSQLite();
        public StartWindow()
        {
            InitializeComponent();
        }
        //UserService userService = new UserService();
        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationView registrationView = new RegistrationView(dataUserSQLite);
            registrationView.Show();
            this.Close();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView(dataUserSQLite);
            loginView.Show();
            this.Close();
        }
    }
}