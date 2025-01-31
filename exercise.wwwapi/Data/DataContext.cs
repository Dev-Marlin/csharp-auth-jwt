using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Data
{
    public class DataContext : DbContext
    {
        private string _connectionString;
        public DataContext(DbContextOptions<DataContext> options):base(options) 
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnectionString")!;
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder); 
            modelBuilder.Entity<BlogPost>().HasKey(bp => new { bp.Id, bp.UserId });
            //modelBuilder.Entity<User>().HasKey(u => u.Id);
            //modelBuilder.Entity<User>().HasMany(u => u.Posts).WithOne(p => p.User).HasPrincipalKey(p => {p.Id, p.UserId });//HasForeignKey(p => p.UserId);

            modelBuilder.Entity<User>().HasMany(u => u.Posts).WithOne(u => u.User).HasForeignKey(p => p.UserId);

            //modelBuilder.Entity<BlogPost>().HasOne(bp => bp.User).WithMany(u => u.Posts).HasPrincipalKey(a => new {a.Id });

            //modelBuilder.Entity<BlogPost>().HasKey(u => {u.UserId, u.Id});//u.Id);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<BlogPost> Blogs { get; set; }
    }
}
