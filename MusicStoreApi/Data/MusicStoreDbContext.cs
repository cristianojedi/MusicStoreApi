using Microsoft.EntityFrameworkCore;
using MusicStoreApi.Data.Mapping;
using MusicStoreApi.Models;

namespace MusicStoreApi.Data
{
    public class MusicStoreDbContext : DbContext
    {
        public MusicStoreDbContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(new UserMap().Configure);
        }
    }
}
