using System.Windows;
using System.Windows.Input;
using userApp.Core;
using userApp.Helpers;

namespace userApp.Login
{
    public class LoginViewModel : BaseViewModel
    {
        //private readonly UserManager _userManager;
        private readonly UserService _userService;
        public bool _isInputEnabled;
        public ICommand LoginCommand { get; }
        //public ICommand NavigateToRegistrationCommand { get; }

        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;

        public LoginViewModel()
        {
            _userService = new UserService();
            LoginCommand = new RelayCommand(Log);
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

        private void Log(object parameter)
        {
            int result = _userService.Signin(Login, Password);

            ErrorMessage = string.Empty;
            switch (result)
            {
                case 0:
                    ErrorMessage = "Пустые поля";
                    break;
                case 1:
                    ErrorMessage = "Успешний вход";
                    break;
                case 2:
                    ErrorMessage = "Пароль не верный";
                    break;
                case 3:
                    ErrorMessage = "Пользователь не найден";
                    break;
            }

            OnPropertyChanged(nameof(ErrorMessage));
        }
    }
}
