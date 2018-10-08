using ChatServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace ChatServer.EF
{
    public class ApplicationContext:IdentityDbContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<ApplicationUser> AppUsers { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {

        }
    }
}
