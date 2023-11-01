using ChatOn.Models;
using ChatOn.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChatOn.Api.Controllers
{
    public class UserController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;

        public UserController(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("users"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<IList<IUserApiData>>> GetAll()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpGet("users/{id}"), Authorize]
        public async Task<ActionResult<IUserApiData?>> GetById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user == null ? NotFound("User not found") : Ok(user);
        }

        [HttpPost("users/create"), AllowAnonymous]
        public async Task<IActionResult> Create(string name, string password)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            if (name.Length <= 3)
            {
                return BadRequest("The name should be longer than 3 characters");
            }


            var roleController = new RoleController(_context);
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Member");

            if (role == null)
            {
                return StatusCode(500, "The insertion script hasn't been executed, the role \"Member\" is missing");
            }

            _context.Users.Add(new User { Name = name, PasswordHash = passwordHash, Role = role! });
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("users/login"), AllowAnonymous]
        public async Task<ActionResult<string>> Login(string name, string password)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Name == name);
            var isPasswordCorrect = user == null ? false : BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            return user != null && isPasswordCorrect ? Ok(CreateToken(user)) : BadRequest("Informations are incorrect");
        }


        [HttpPut("users/{id}/role"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeRole(int id, string roleName)
        {
            var user = await _context.Users.FindAsync(id);

            if(user == null)
            {
                return NotFound("User not found");
            }

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);

            if (user == null)
            {
                return NotFound("Role not found");
            }

            return Ok();
        }


        [HttpGet("users/{id}/sent-messages"), Authorize]
        public ActionResult<IList<IMessageApiData>> GetSentMessages(int id)
        {
            var sentMessages = _context.Users
                .Where(u => u.Id == id)
                .SelectMany(u => u.SentMessages)
                .Select(m => new Message { Id = m.Id, Content = m.Content } as IMessageApiData)
                .ToList();

            return sentMessages == null ? NotFound() : Ok(sentMessages);
        }

        [HttpGet("users/{id}/received-messages"), Authorize]
        public ActionResult<IList<IMessageApiData>> GetReceivedMessages(int id)
        {
            var receivedMessages = _context.Users
                .Where(u => u.Id == id)
                .SelectMany(u => u.ReceivedMessages)
                .Select(m => new Message { Id = m.Id, Content = m.Content } as IMessageApiData)
                .ToList();

            return receivedMessages == null ? NotFound() : Ok(receivedMessages);
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Tokens")["JWT"]!));

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token); ;
        }
    }
}
