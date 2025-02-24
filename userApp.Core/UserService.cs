using userApp.Domain.Models;
using System.Text.RegularExpressions;
using userApp.Core;
using System.Windows;
using SQLitePCL;
using Microsoft.EntityFrameworkCore;

namespace userApp
{
    public class UserService
    {
        private readonly HashingService _hashingService = new HashingService();
        private string emailRegex = @"^[a-zA-Z0-9!#$%^&*()+=?{}|~`_/.-]+@(?:gmail|mail)\.(?:ru|com)$";
        private readonly AppDbContext _context = new AppDbContext();
        
        public UserService()
        {
            _context.Users.Load();
        }
        
        // Регистрация
        public int Register(DataUserModel user, string RepeatPass)
        {
            // Пустые поля
            if (string.IsNullOrWhiteSpace(user.UserName) ||
                string.IsNullOrWhiteSpace(user.Email) ||
                string.IsNullOrWhiteSpace(user.Password) ||
                string.IsNullOrWhiteSpace(RepeatPass))
                return 0;

            // Валидация почты
            if (!Regex.Match(user.Email, emailRegex).Success) return 1; 

            // Пароли не совпадают
            if (user.Password != RepeatPass)
                return 2;

            // Пользователь уже существует
            if (_context.Users.Any(u => u.UserName == user.UserName || u.Email == user.Email))
                return 3;

            // Хеширование пароля
            //DataUserModel hashUser = user;
            user.Password = _hashingService.HashPassword(user.Password);
            //user.Password = hashUser.Password;

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