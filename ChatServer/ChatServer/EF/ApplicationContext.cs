using ChatServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace ChatServer.EF
{
    public class ApplicationContext:IdentityDbContext<ApplicationUser>
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<ApplicationUser> AppUsers { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<UserChat> UserChats { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserChat>().HasKey(t => new { t.ChatId, t.ApplicationUserId });

            builder.Entity<UserChat>().HasOne(uc => uc.Chat).WithMany(c => c.UserChats).HasForeignKey(uc => uc.ChatId);
            builder.Entity<UserChat>().HasOne(uc => uc.ApplicationUser).WithMany(c => c.UserChats).HasForeignKey(uc => uc.ApplicationUserId);
            base.OnModelCreating(builder);
        }
    }
}
