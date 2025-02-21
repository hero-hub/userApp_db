using System.Windows;
using userApp.Domain.Models;

namespace userApp.Registration
{
    public partial class RegistrationView : Window
    {
        DataUserSQLite dataUserSQLite = new DataUserSQLite();
        public RegistrationView(dataUserSQLite)
        {
            InitializeComponent();
            DataUserSQLite = dataUserSQLite;
            DataContext = dataUserSQLite;
        }
    }
}
