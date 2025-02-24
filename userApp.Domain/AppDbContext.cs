using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace userApp.Domain.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            Database.EnsureCreated();//создаёт базу, если её нет. Если есть, ничего не делает
        }

        public DbSet<DataUserModel> Users { get; set; } // таблица пользователей

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=users.db");
        }
    }
}