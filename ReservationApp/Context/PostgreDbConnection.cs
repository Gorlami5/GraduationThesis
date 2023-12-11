using Microsoft.EntityFrameworkCore;
using ReservationApp.Model;

namespace ReservationApp.Context
{
    public class PostgreDbConnection : DbContext
    {
        public PostgreDbConnection(DbContextOptions<PostgreDbConnection> options) : base(options) 
        { 

        }
        public DbSet<City> Cities { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
