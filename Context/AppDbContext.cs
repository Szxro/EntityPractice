using Context.Seed;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Reflection;

namespace Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            DataSeeding.SetData(modelBuilder); //Setting the default data

            //Loading the configuration from the Assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            //Setting the config globally for some properties
            configurationBuilder.Properties<string>().HaveMaxLength(256);
            configurationBuilder.Properties<DateTime>().HaveColumnType("date");
        }
        public DbSet<MovieTheater> MovieTheaters => Set<MovieTheater>();

        public DbSet<Cinema> Cinemas => Set<Cinema>();  

        public DbSet<Movie> Movies => Set<Movie>();

        public DbSet<MovieGenders> MovieGenders => Set<MovieGenders>();

        public DbSet<CinemaMovie> CinemaMovies => Set<CinemaMovie>();

        public DbSet<CinemaWithoutPK> CinemaWithoutPK => Set<CinemaWithoutPK>();

        public DbSet<CinemaView> CinemaView => Set<CinemaView>();
    }
}