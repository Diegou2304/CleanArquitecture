using CleanArquitecture.Domain;
using CleanArquitecture.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArquitecture.Infrastructure.Persistence
{
    public class StreamerDbContext : DbContext
    {

        public StreamerDbContext(DbContextOptions<StreamerDbContext> options): base(options)
        {

            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=LAPTOP-UF8012AL\SQLEXPRESS; Database=Streamer; Trusted_Connection=True; Encrypt=False")
        //        .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, Microsoft.Extensions.Logging.LogLevel.Information)
        //        .EnableSensitiveDataLogging();
        //}

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach(var entry in ChangeTracker.Entries<BaseDomainModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreationDate = DateTime.Now;
                        entry.Entity.CreatedBy = "system";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "system";
                        break;
                }

            }

            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Se utiliza cuando no se siguen las convenciones de EF, pero pienso que es bueno ponerlo ahí para tener mejor legibilidad
            modelBuilder.Entity<Streamer>()
                 .HasMany(m => m.Videos)
                 .WithOne(m => m.Streamer)
                 .HasForeignKey(m => m.StreamerId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Video>()
                .HasMany(actor => actor.Actors)
                .WithMany(videos => videos.Videos)
                .UsingEntity<VideoActor>(
                    av => av.HasKey(e => new { e.ActorId, e.VideoId })
                );

        }
        public DbSet<Streamer> Streamers { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Director> Directors { get; set; }
    }
}
