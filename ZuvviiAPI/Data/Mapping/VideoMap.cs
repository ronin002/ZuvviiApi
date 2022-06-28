using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZuvviiAPI.Models;

namespace ZuvviiAPI.Data.Mapping
{
    public class VideoMap : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.ToTable("Video");

            builder.HasIndex("Id");

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnName("title");

            builder.Property(x => x.Url)
                .IsRequired()
                .HasColumnName("url");

            builder.Property(x => x.Thumb)
                .IsRequired()
                .HasColumnName("thumb");

            builder.Property(x => x.Like)
                .HasColumnName("like")
                .HasDefaultValue(0);

            builder.Property(x => x.Views)
                .IsRequired()
                .HasColumnName("view")
                .HasDefaultValue(0);

            builder.Property(x => x.CreateAt)
               .HasColumnName("createat")
               .HasDefaultValue(DateTime.Now);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Videos)
                .HasConstraintName("CT_User_OwnerId")
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
