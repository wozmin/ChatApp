using ChatServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    internal class UserEfEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("User");

            builder.HasKey(user => user.Id);

            builder.Property(u => u.LastVisit)
                .HasDefaultValueSql("getdate()");

            builder.HasMany(user => user.UserChats)
                .WithOne(userChat => userChat.ApplicationUser)
                .HasForeignKey(userChat => userChat.ApplicationUserId);

            builder.HasMany(user => user.ChatMessages)
                .WithOne(message => message.User)
                .HasForeignKey(message => message.UserId);

            builder.HasOne(user => user.UserProfile)
                .WithOne(profile => profile.User);
        }
    }
}
