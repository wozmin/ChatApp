using ChatServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class ChatEfEntityConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.ToTable(nameof(Chat));

            builder.HasKey(chat => chat.Id);

            builder.HasMany(chat => chat.UserChats)
                .WithOne(userChat => userChat.Chat)
                .HasForeignKey(userChat => userChat.ChatId);

            builder.HasMany(chat => chat.Messages)
                .WithOne(message => message.Chat)
                .HasForeignKey(message => message.ChatId);

        }
    }
}
