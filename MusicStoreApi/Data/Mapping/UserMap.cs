using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStoreApi.Models;

namespace MusicStoreApi.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(u => u.Id);

            entityTypeBuilder.Property(u => u.Name).IsRequired().HasColumnType("varchar(50)");
            entityTypeBuilder.Property(u => u.Email).IsRequired().HasColumnType("varchar(150)");
            entityTypeBuilder.Property(u => u.Password).IsRequired().HasColumnType("varchar(20)");
            entityTypeBuilder.Property(u => u.DateCreated).IsRequired().HasColumnType("datetime");
            entityTypeBuilder.Property(u => u.DateUpdated).IsRequired().HasColumnType("datetime");
        }
    }
}
