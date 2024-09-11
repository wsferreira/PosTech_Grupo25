using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PosTech.Entidades;

namespace PosTech.Repository
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString;

        public ApplicationDbContext()
        {          
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            _connectionString = configuration.GetConnectionString("ConnectionStringSql");
                      

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<Regiao> Regiao { get; set; }
        public DbSet<Contato> Contato { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
                optionsBuilder.EnableSensitiveDataLogging();
                //optionsBuilder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);


        }
    }
}
