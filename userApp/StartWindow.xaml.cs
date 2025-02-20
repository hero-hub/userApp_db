using System.Windows;
using userApp.Registration;
using userApp.Login;

namespace userApp
{
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }
        //UserService userService = new UserService();
        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            //RegistrationViewModel registrationViewModel = new RegistrationViewModel();

            RegistrationView registrationView = new RegistrationView();
            registrationView.Show();
            this.Close();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            loginView.Show();
            this.Close();
        }
    }
}