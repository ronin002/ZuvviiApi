
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZuvviiAPI.Models;

namespace ZuvviiAPI.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            //Chave primária
            builder.HasKey(x => x.Id);

            //Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
                //.UseMySqlIdentityColumn();

            builder.Property(x => x.UserName)
                .IsRequired()
                .HasColumnName("Name");


            builder.Property(x => x.Password)
                .IsRequired();

            builder.Property(x => x.Email)
                .IsRequired();

            builder.Property(x => x.Avatar)
                .IsRequired(false)
                .HasColumnName("Avatar");

            builder
                .HasIndex(x => x.Email, "IX_User_Email")
                .IsUnique();
        }
    }
}
