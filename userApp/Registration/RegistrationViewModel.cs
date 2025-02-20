using System;
using System.Windows;
using System.Windows.Input;
using userApp.Helpers;
using userApp.Core;
using userApp.Domain.Models;

namespace userApp.Registration
{
    public class RegistrationViewModel : BaseViewModel
    {
        private readonly UserService _userService;//

        // Properties
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RepeatPass { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;

        public ICommand RegisterCommand { get; }

        public RegistrationViewModel()
        {
            _userService = new UserService();//
            RegisterCommand = new RelayCommand(Register);
        }
        private void Register(object parameter)
        {
            DataUserSQLite user = new DataUserSQLite // 
            {
                UserName = UserName,
                Email = Email,
                Password = Password,
            };

            int result = _userService.Register(user, RepeatPass);

            ErrorMessage = string.Empty;
            switch (result)
            {
                case 0:
                    ErrorMessage = "Пустые поля";
                    break;
                case 1:
                    ErrorMessage = "Ваша почта не валидна";
                    break;
                case 2:
                    ErrorMessage = "Пароли не совпадают";
                    break;
                case 3:
                    ErrorMessage = "Email уже зарегистрирован";
                    break;
                case 4:
                    ErrorMessage = "Успешная регистрация";
                    break;
            }

            OnPropertyChanged(nameof(ErrorMessage));

        }

    }
}