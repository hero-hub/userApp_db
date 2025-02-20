﻿using userApp.Domain.Models;
using System.Text.RegularExpressions;
using userApp.Core;
using System.Windows;
using SQLitePCL;

namespace userApp
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly HashingService _hashingService = new HashingService();
        private string emailRegex = @"^[a-zA-Z0-9!#$%^&*()+=?{}|~`_/.-]+@(?:gmail|mail)\.(?:ru|com)$";
        //_context.Users.Load();

        //public UserService()
        //{
        //}

        // Регистрация
        public int Register(DataUserSQLite user, string RepeatPass)
        {
            // Пустые поля
            if (string.IsNullOrWhiteSpace(user.UserName) ||
                string.IsNullOrWhiteSpace(user.Email) ||
                string.IsNullOrWhiteSpace(user.Password) ||
                string.IsNullOrWhiteSpace(RepeatPass))
                return 0;
            //MessageBox.Show("not error");
            // Валидация почты
            if (!Regex.Match(user.Email, emailRegex).Success) return 1; 

            // Пароли не совпадают
            if (user.Password != RepeatPass)
                return 2;

            // Пользователь уже существует
            if (_context.Users.Any(u => u.UserName == user.UserName || u.Email == user.Email))
                return 3;

            // Хеширование пароля
            var hashUser = user;
            hashUser.Password = _hashingService.HashPassword(hashUser.Password);
            user.Password = hashUser.Password;

            // Успешная регистрация
            _context.Users.Add(user);
            _context.SaveChanges();
            return 4;
        }

        // Авторизация
        public int Signin(string login, string pass)
        {
            // Пустые поля
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(pass))
                return 0;

            var user = _context.Users.FirstOrDefault(u => u.UserName == login || u.Email == login);

            if (user != null)
            {
                if (_hashingService.VerifyPassword(pass, user.Password))
                    // Успешный вход
                    return 1;
                else
                    // Неверный пароль
                    return 2;
            }
            // Пользователь не найден
            return 3;
        }
    }
}