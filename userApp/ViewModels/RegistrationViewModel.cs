using System;
using System.Windows;
using System.Windows.Input;
using userApp.Helpers;
using userApp.Core;
using userApp.Domain.Models;

namespace userApp.ViewModels
{
    public class RegistrationViewModel : INotifyRelise
    {
        private readonly UserService _userService = new UserService();
        private readonly MainWindowViewModel _mainViewModel;

        // Properties
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RepeatPass { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;

        public ICommand RegisterCommand { get; }
        public ICommand GoBackCommand { get; }
        public RegistrationViewModel(MainWindowViewModel mainViewModel)
        {
            _userService = new UserService();
            _mainViewModel = mainViewModel;
            RegisterCommand = new RelayCommand(Register);
            GoBackCommand = new RelayCommand(_ => _mainViewModel.ShowMainMenu());
        }

        private void Register(object parameter)
        {
            DataUserModel user = new DataUserModel
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