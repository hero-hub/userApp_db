using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace userApp.Domain.Models
{
    public class DataUserModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public int Id { get; set; }

        private string userName = "";

        private string password = "";

        private string email = "";

        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }
        //Для LoadUser, возможно получится убрать после создания вьюмодели
        public DataUserModel(string username, string password, string email)
        {
            UserName = username;
            Password = password;
            Email = email;
        }
        public DataUserModel()
        {

        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}