using Microsoft.EntityFrameworkCore;
using Tinder.Models;

namespace Tinder.Services
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Matches> Matches { get; set; }
    }
}
