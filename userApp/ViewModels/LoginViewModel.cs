using System.Windows;
using System.Windows.Input;
using userApp.Helpers;

namespace userApp.ViewModels
{
    public class LoginViewModel : INotifyRelise
    {
        public bool _isInputEnabled;
        private readonly UserService _userService = new UserService();
        private readonly MainWindowViewModel _mainViewModel;
        public LoginViewModel(MainWindowViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            LoginCommand = new RelayCommand(Log);
            GoBackCommand = new RelayCommand(_ => _mainViewModel.ShowMainMenu());
        }
        public ICommand LoginCommand { get; }
        public ICommand GoBackCommand { get; }

        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;

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
