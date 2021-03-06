﻿using System;

namespace ChatServer.Models
{
    public class UserChat
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public Guid ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}
