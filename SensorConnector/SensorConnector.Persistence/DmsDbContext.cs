using Microsoft.EntityFrameworkCore;
using SensorConnector.Persistence.Entities;
using SensorConnector.Persistence.EntitiesConfigurations;

namespace SensorConnector.Persistence
{
    public class DmsDbContext : DbContext
    {
        public DmsDbContext()
        {
        }

        public DmsDbContext(DbContextOptions<DmsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CommunicationProtocol> CommunicationProtocols { get; set; }
        public virtual DbSet<Datatype> Datatypes { get; set; }
        public virtual DbSet<Sensor> Sensors { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=MyDb;Username=postgres;Password=kurepin");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CommunicationProtocolConfiguration());
            modelBuilder.ApplyConfiguration(new DatatypeConfiguration());
            modelBuilder.ApplyConfiguration(new SensorConfiguration());
        }
    }
}
