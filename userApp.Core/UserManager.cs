using System.Collections.Generic;
using System.IO;
using System.Linq;
using userApp.Domain.Models;
using System.Text.RegularExpressions;
using userApp.Core;
using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;
using System;
using System.Diagnostics;

namespace userApp.Core
{
    //Класс для работы с файлом .txt
    public class UserManager
    {
        private List<DataUserModel> _users = new List<DataUserModel>(); // Лист пользователей
        private readonly HashingService _hashingService = new HashingService(); // Объект класса HashingService
        private SerializeMethods serializeMethods = new SerializeMethods();
        private string? filePath = "users.txt";
        private string emailRegex = @"^[a-zA-Z0-9!#$%^&*()+=?{}|~`_/.-]+@(?:gmail|mail)\.(?:ru|com)$"; //регулярное выражение для валидации Email

        public void SaveUsers()
        {
            using (StreamWriter writer = new StreamWriter(filePath!))
            {
                foreach (DataUserModel user in _users)
                    writer.WriteLine(user.UserName + "," + user.Password + "," + user.Email);
            }
        }

        public bool LoadUsers()
        {
            if (!File.Exists(filePath)) return false;

            _users.Clear();
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');

                if (parts.Length != 3)
                    return false;

                DataUserModel user = new DataUserModel(parts[0], parts[1], parts[2]);
                _users.Add(user);
            }

            return true;
        }

        // Регистрация
        public int Register(DataUserModel user, string repeatPass)
        {
            if (string.IsNullOrWhiteSpace(user.UserName) ||
                string.IsNullOrWhiteSpace(user.Email) ||
                string.IsNullOrWhiteSpace(user.Password) ||
                string.IsNullOrWhiteSpace(repeatPass))
                return 0; // Пустые поля

            if (!Regex.Match(user.Email, emailRegex).Success) return 1; // Валидация почты

            LoadUsers();

            // Пароли не совпадают
            if (user.Password != repeatPass) return 2;

            // Пользователь уже существует
            if (_users.Exists(u => u.UserName == user.UserName || u.Email == user.Email)) return 3; 

            // Хеширование пароля
            var hashUser = user;
            hashUser.Password = _hashingService.HashPassword(hashUser.Password);
            user.Password = hashUser.Password;

            //Сохранение с подсчётом времени
            var stopwatch = Stopwatch.StartNew();
            _users.Add(user);
            SaveUsers();
            stopwatch.Stop();
            Debug.WriteLine($"Сохранение пользователя заняло: {stopwatch.ElapsedMilliseconds} мс");

            //Сериализация Xml
            //serializeMethods.SerializeXml(_users);

            //Десериализация Xml
            //serializeMethods.DeserializeXml();

            //Бинарная сериализация
            //serializeMethods.SerializeBinary(_users);

            //Бинарная десериализация
            //serializeMethods.DeserializeBinary();

            // Успешная регистрация
            return 4; 
        }

        // Авторизация
        public int Signin(string login, string pass)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(pass))
                return 0; // Пустые поля

            LoadUsers();

            var stopwatch = Stopwatch.StartNew();
            var user = _users.FirstOrDefault(u => u.UserName == login || u.Email == login);
            stopwatch.Stop();
            Debug.WriteLine($"Поиск пользователя занял: {stopwatch.ElapsedMilliseconds} мс");

            if (user != null)
            {
                if (_hashingService.VerifyPassword(pass, user.Password))
                    return 1; // Успешный вход
                else
                    return 2; // Неверный пароль
            }

            return 3; // Пользователь не найден
        }
    }

}