using Messages.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Messages.Data
{
    public class MessageDbContext: DbContext
    {
        public DbSet<Message> Messages { get; set; }

        public DbSet<User> Users { get; set; }

        public MessageDbContext(DbContextOptions options): base(options)
        {
        }
    }
}
 