using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZuvviiAPI.Models;

namespace ZuvviiAPI.Data.Mapping
{
    public class CommentsMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");

            builder.HasIndex("Id");

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Commentary)
                .IsRequired()
                .HasColumnName("comment");

            builder.Property(x => x.CreateAt)
                .IsRequired()
                .HasColumnName("createat")
                .HasDefaultValue(DateTime.Now);

            builder.HasOne(x => x.Author)
                .WithMany(x => x.Comments)
                .HasConstraintName("CT_Comment_OwnerId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Video)
                .WithMany(x => x.Comments)
                .HasConstraintName("CT_Comment_VideoId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
