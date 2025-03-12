﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using userApp.Core;
using userApp.Helpers;

namespace userApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public bool _isInputEnabled;
        private readonly UserService _userService = new UserService();
        private string _errorMessage = "";

        public LoginViewModel(MainWindowViewModel mainViewModel)
        {
            LoginCommand = new RelayCommand(Log);
        }
        public ICommand LoginCommand { get; }

        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        private string ErrorMessage { 
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

        private void Log(object parameter)
        {
            int result = _userService.Signin(Login, Password);

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
