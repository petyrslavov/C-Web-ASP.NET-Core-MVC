using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Messages.App.Jwt;
using Messages.App.Models;
using Messages.Data;
using Messages.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Messages.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly MessageDbContext context;
        private readonly JwtSettings jwtSettings;

        public UsersController(MessageDbContext context, IOptions<JwtSettings> jwtSettings)
        {
            this.context = context;
            this.jwtSettings = jwtSettings.Value;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UsersBindingModel bindingModel)
        {
            this.context.Users.Add(new User
            {
                Username = bindingModel.Username,
                Password = bindingModel.Password
            });

            await this.context.SaveChangesAsync();
            return this.Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsersBindingModel bindingModel)
        {
            var userFromDb = await this.context.Users
                .SingleOrDefaultAsync(user => user.Username == bindingModel.Username && user.Password == bindingModel.Password);

            if (userFromDb == null)
            {
                return this.BadRequest("Username or password is invalid.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userFromDb.Username)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return this.Ok(token);
        }
    }
}