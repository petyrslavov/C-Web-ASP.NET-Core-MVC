 using System;

namespace Messages.Domain
{
    public class Message
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
