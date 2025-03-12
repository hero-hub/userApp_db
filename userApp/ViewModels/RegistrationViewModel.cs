﻿using System.Windows.Input;
using userApp.Helpers;
using userApp.Core;
using userApp.Domain.Models;
using System.ComponentModel;

namespace userApp.ViewModels
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        private readonly UserService _userService;
        private string _errorMessage = "";
        public ICommand RegisterCommand { get; }

        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RepeatPass { get; set; } = string.Empty;

        private string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public RegistrationViewModel(MainWindowViewModel mainViewModel)
        {
            _userService = new UserService();
            RegisterCommand = new RelayCommand(Register);
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