using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eventures.Data.Seeding
{
    public class EventureUserRoleSeeder : ISeeder
    {
        public void Seed(EventuresDbContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.Add(new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                });
                context.Roles.Add(new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                });
            }

            context.SaveChanges();
        }
    }
}

