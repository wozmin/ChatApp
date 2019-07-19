using System;

namespace ChatServer.Models
{
    public class UserProfile
    {
        public Guid Id { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public DateTime LastVisit { get; set; }
        public string AvatarUrl { get; set; }

        public ApplicationUser User { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
