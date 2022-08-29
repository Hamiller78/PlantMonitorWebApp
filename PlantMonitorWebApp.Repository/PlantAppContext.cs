using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore;
using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Repository
{
    public class PlantAppContext : DbContext
    {
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Sensor> Sensors { get; set; }

        public PlantAppContext(DbContextOptions<PlantAppContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.UseIdentityColumns();
            modelbuilder.Entity<Plant>().ToTable("Plant");
            modelbuilder.Entity<Sensor>().ToTable("Sensor");
        }
    }
}