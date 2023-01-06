using System;
using System.Threading.Tasks;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Repositories
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options)
            : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<Camera> Cameras { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Lane> Lanes { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> b)
        {
            b.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("int8");

            b.HasKey(x => x.Id);
        }
    }
}
