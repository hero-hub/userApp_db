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
        private readonly UserManager _userManager;


        // Properties
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RepeatPass { get; set; }

        public string ErrorMessage { get; set; }

        public ICommand RegisterCommand { get; }

        public RegistrationViewModel()
        {
            _userManager = new UserManager();
            RegisterCommand = new RelayCommand(Register);
        }
        private void Register(object parameter)
        {
            AdvencedUserModel user = new AdvencedUserModel
            {
                UserName = UserName,
                Email = Email,
                Password = Password,
                RepeatPass = RepeatPass
            };

            int result = _userManager.Register(user);

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