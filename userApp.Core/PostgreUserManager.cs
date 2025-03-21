using userApp.Domain.Models;
using System.Text.RegularExpressions;
using userApp.Core;
using Npgsql;
using System.Data;

namespace userApp
{
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

            // Пользователь уже существует
            if (db.Users.Any(u => u.UserName == user.UserName || u.Email == user.Email)) return 3;

            // Хеширование пароля
            user.Password = _hashingService.HashPassword(user.Password);

            // Успешная регистрация
            db.Users.Add(user);
            db.SaveChanges();

            //Сериализация Xml
            //UserToXml.SerializeXml(_context);

            return 4;
        }

        // Авторизация
        public int Signin(string login, string pass)
        {
            // Пустые поля
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(pass))
                return 0;

            var user = db.Users.FirstOrDefault(u => u.UserName == login || u.Email == login);

            // 1 - Успешный вход
            // 2 - Неверный пароль
            // 3 - Пользователь не найден
            return (user != null) ? (_hashingService.VerifyPassword(pass, user.Password) ? 1 : 2) : 3;
        }
    }
}