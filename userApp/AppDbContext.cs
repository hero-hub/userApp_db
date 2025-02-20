using Microsoft.EntityFrameworkCore;
using userApp.Domain.Models;
using userApp.Core;

namespace userApp
{
    public class AppDbContext : DbContext
    {
        public DbSet<DataUserSQLite> Users { get; set; } // таблица пользователей

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=users.db");
        }
    }
}