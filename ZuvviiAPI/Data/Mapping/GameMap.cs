using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZuvviiAPI.Models;

namespace ZuvviiAPI.Data.Mapping
{
    public class GameMap : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("Game");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnName("title");

            builder.Property(x => x.Type)
                .IsRequired()
                .HasColumnName("type");

            builder.Property(x => x.Tags)
                .HasColumnName("tags");

             builder.HasIndex(x => x.Id, "IX_Game_Index")
                .IsUnique();

        }
    }
}
