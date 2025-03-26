using System.ComponentModel;
using System.Windows.Input;
using userApp.Core;

namespace userApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly UserManager _userManager = new UserManager();
        private readonly UserService _userService = new UserService();
        private readonly PostgreUserManager _pgsqlUserManager = new PostgreUserManager();

        private string _errorMessage = "";
        private bool _isInputEnabled;

        public ICommand LoginCommand { get; }

        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public bool IsInputEnabled
        {
            get => _isInputEnabled;
            set
            {
                _isInputEnabled = value;
                OnPropertyChanged(nameof(IsInputEnabled));
            }
        }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(p => { Log(); });
        }
        private void Log()
        {
            int result = _userManager.Signin(Login, Password); // есть сериализация
            //int result = _userService.Signin(Login, Password);
            //int result = _pgsqlUserManager.Signin(Login, Password);

            ErrorMessage = result switch
            {
                0 => "Пустые поля",
                1 => "Успешный вход",
                2 => "Пароль неверный",
                3 => "Пользователь не найден",
                _ => string.Empty
            };
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
