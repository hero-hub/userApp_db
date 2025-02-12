using Microsoft.EntityFrameworkCore;

namespace userApp.Domain.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<DataUserSQLite> Users { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=helloapp.db");
        }
    }
}
