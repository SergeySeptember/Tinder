using Microsoft.EntityFrameworkCore;
using Tinder.Models;

namespace Tinder.Services
{
    public class Context : DbContext
    {
        public Context()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string filePath = @"C:\Users\September\source\repos\Tinder\Tinder\.env";
            string[] connectionString = File.ReadAllLines(filePath);

            optionsBuilder.UseNpgsql(connectionString[0]);
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Matches> Matches { get; set; }

    }
}
