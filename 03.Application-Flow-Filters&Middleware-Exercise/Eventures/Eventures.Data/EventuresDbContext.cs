using Eventures.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Eventures.Data
{
    public class EventuresDbContext : IdentityDbContext<EventureUser, IdentityRole, string>
    {
        public DbSet<Event> Events { get; set; }

        public EventuresDbContext(DbContextOptions<EventuresDbContext> options) : base(options)
        {
        } 
    }
}
