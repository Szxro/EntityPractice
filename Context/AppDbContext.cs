using Context.Seed;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           // DataSeeding.SetData(modelBuilder); //Setting the default data
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            //Setting the string globally a max length 
            configurationBuilder.Properties<string>().HaveMaxLength(256);
        }
        public DbSet<MovieTheater> MovieTheaters => Set<MovieTheater>();

        public DbSet<Cinema> Cinemas => Set<Cinema>();  
    }
}