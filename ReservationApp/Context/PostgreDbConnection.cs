using Microsoft.EntityFrameworkCore;
using ReservationApp.Model;

namespace ReservationApp.Context
{
    public class PostgreDbConnection : DbContext
    {
        private readonly IConfiguration _configuration;
        public PostgreDbConnection(DbContextOptions<PostgreDbConnection> options,IConfiguration configuration) : base(options) 
        { 
            _configuration = configuration;
        }
        public DbSet<City> Cities { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<CompanyPhoto> CompanyPhotos { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
   
}
