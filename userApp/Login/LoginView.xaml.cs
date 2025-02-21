using System.Windows;
using userApp.Domain.Models;

namespace userApp.Login
{
    public partial class LoginView : Window
    {
        public DataUserSQLite DataUserSQLite { get; set; }
        public LoginView(DataUserSQLite dataUserSQLite)
        {
            InitializeComponent();
            DataUserSQLite = dataUserSQLite;
            DataContext = DataUserSQLite;
        }
    }
}
