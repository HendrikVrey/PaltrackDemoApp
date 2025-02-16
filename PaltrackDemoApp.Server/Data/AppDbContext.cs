using Microsoft.EntityFrameworkCore;
using PaltrackDemoApp.Server.Models;

namespace PaltrackDemoApp.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
    }
}
