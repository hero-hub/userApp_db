using Microsoft.EntityFrameworkCore;

namespace userApp.Domain.Models
{
    public class PgsqlDbContext : DbContext
    {

        public DbSet<DataUserModel> Users { get; set; } = null!; // таблица пользователей
        public PgsqlDbContext()
        {
            Database.EnsureCreated();//создаёт базу, если её нет. Если есть, ничего не делает
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=userAppDb;Username=postgres;Password=femto");
        }

    }
}