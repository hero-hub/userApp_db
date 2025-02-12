using System.Windows;
using System.Windows.Input;
using userApp.Core;
using userApp.Helpers;

namespace userApp.Login
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly UserManager _userManager;
        public bool _isInputEnabled;

        public LoginViewModel()
        {
            _userManager = new UserManager();
            LoginCommand = new RelayCommand(Log);
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsInputEnabled
        {
            get => _isInputEnabled;
            set
            {
                _isInputEnabled = value;
                OnPropertyChanged(nameof(IsInputEnabled));
            }
        }


        public ICommand LoginCommand { get; }
        public ICommand NavigateToRegistrationCommand { get; }

        private void Log(object parameter)
        {
            int result = _userManager.Signin(Login, Password);

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
