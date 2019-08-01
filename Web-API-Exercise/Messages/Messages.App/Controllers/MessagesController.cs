using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Messages.App.Models;
using Messages.Data;
using Messages.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Messages.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MessageDbContext context;

        public MessagesController(MessageDbContext context)
        {
            this.context = context;
        }

        [HttpGet(Name = "All")]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<Message>>> All()
        {
            return this.context.Messages
                .OrderBy(m => m.CreatedOn)
                .ToList();
        }

        [HttpPost(Name = "Create")]
        [Route("create")]
        public async Task<ActionResult> Create(MessageCreateBindingModel bindingModel)
        {
            var userFromDb = await this.context.Users
                    .SingleOrDefaultAsync(user => user.Username == bindingModel.User);

            Message message = new Message
            {
                Content = bindingModel.Content,
                User = userFromDb,
                CreatedOn = DateTime.UtcNow
            };

            await this.context.Messages.AddAsync(message);
            await this.context.SaveChangesAsync();

            return this.Ok();
        }

        [HttpGet("getme")]
        public async Task<ActionResult> GetUserId()
        {
            return this.Ok(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}