using userApp.Domain.Models;
using System.Text.RegularExpressions;
using userApp.Core;
using Npgsql;
using System.Data;
using System.Diagnostics;

namespace userApp
{
    //Класс для работы с PostgreSQL
    public class PostgreUserManager
    {
        private readonly HashingService _hashingService = new HashingService();
        private string emailRegex = @"^[a-zA-Z0-9!#$%^&*()+=?{}|~`_/.-]+@(?:gmail|mail)\.(?:ru|com)$";
        private readonly PgsqlDbContext db = new PgsqlDbContext();

        string connectionString = "Server=localhost;Port=5432;Database=userAppDb; User Id = postgres; Password=femto;"; //строка подключения к БД
        
        public PostgreUserManager()
        {
            NpgsqlConnection pgsqlConnection = new NpgsqlConnection(connectionString); // инициализация класса подключения к БД
            pgsqlConnection.Open(); //открываем соединение

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = pgsqlConnection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM public.\"Users\"\r\n";
            NpgsqlDataReader dataReader = command.ExecuteReader();//класс для получения данных из БД

            command.Dispose();
            pgsqlConnection.Close();
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
            if (user.Password != RepeatPass) return 2;

            // Измерение времени
            // Пользователь уже существует
            if (db.Users.Any(u => u.UserName == user.UserName || u.Email == user.Email)) return 3;


            // Хеширование пароля
            user.Password = _hashingService.HashPassword(user.Password);

            //Сохранение с подсчётом времени
            var stopwatch = Stopwatch.StartNew();
            db.Users.Add(user);
            db.SaveChanges();
            stopwatch.Stop();
            Debug.WriteLine($"Сохранение пользователя заняло: {stopwatch.ElapsedMilliseconds} мс");
            
            // Успешная регистрация
            return 4;
        }

        // Авторизация
        public int Signin(string login, string pass)
        {
            // Пустые поля
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(pass))
                return 0;

            var stopwatch = Stopwatch.StartNew();
            var user = db.Users.FirstOrDefault(u => u.UserName == login || u.Email == login);
            stopwatch.Stop();
            Debug.WriteLine($"Поиск пользователя занял: {stopwatch.ElapsedMilliseconds} мс");

            // 1 - Успешный вход
            // 2 - Неверный пароль
            // 3 - Пользователь не найден
            return (user != null) ? (_hashingService.VerifyPassword(pass, user.Password) ? 1 : 2) : 3;
        }
    }
}