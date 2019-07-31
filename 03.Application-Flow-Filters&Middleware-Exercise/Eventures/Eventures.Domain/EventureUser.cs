using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Eventures.Domain
{
    public class EventureUser: IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        [MinLength(10)]
        public string UCN { get; set; }
    }
}
