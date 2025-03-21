using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace userApp.Domain.Models
{
    //public class DataUserModel
    //{
    //    public List<DataUserModel> UserList { get; set; } = new List<DataUserModel>();
    //}
    [Serializable]
    public class DataUserModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [Key]
        public int ID { get; set; }
        [Required]
        private string userName = "";
        [Required]
        private string password = "";
        [Required]
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
        public DataUserModel()
        {
        }
        public DataUserModel(string username, string password, string email)
        {
            UserName = username;
            Password = password;
            Email = email;
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }


}