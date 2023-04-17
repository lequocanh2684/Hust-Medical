using Microsoft.EntityFrameworkCore;

namespace Patient_Health_Management_System.Data
{
    public class PatietHealthDbContext : DbContext
    {
        public PatietHealthDbContext()
        {
        }

        public PatietHealthDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string directory = Directory.GetCurrentDirectory();
                IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                    .SetBasePath(directory)
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectingString = configurationRoot.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectingString);
            }
        }
    }
}