using System.Windows.Input;
using userApp.Helpers;
using userApp.Core;
using userApp.Domain.Models;
using System.ComponentModel;

namespace userApp.ViewModels
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        private readonly UserManager _userManager = new UserManager(); 
        private readonly UserService _userService;
        private readonly PostgreUserManager _pgsqlUserManager = new PostgreUserManager(); 

        private string _errorMessage = "";
        public ICommand RegisterCommand { get; }

        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RepeatPass { get; set; } = string.Empty;

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public RegistrationViewModel()
        {
            _userService = new UserService();
            RegisterCommand = new RelayCommand(p => { Register(); });
        }

        private void Register()
        {
            DataUserModel user = new DataUserModel
            {
                UserName = UserName,
                Email = Email,
                Password = Password,
            };

            
            int result = _userManager.Register(user, RepeatPass);
            //int result = _userService.Register(user, RepeatPass);
            //int result = _pgsqlUserManager.Register(user, RepeatPass);

            ErrorMessage = result switch
            {
                0 => "Пустые поля",
                1 => "Ваша почта не валидна",
                2 => "Пароли не совпадают",
                3 => "Email уже зарегистрирован",
                4 => "Успешная регистрация",
                _ => string.Empty
            };

            OnPropertyChanged(nameof(ErrorMessage));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}