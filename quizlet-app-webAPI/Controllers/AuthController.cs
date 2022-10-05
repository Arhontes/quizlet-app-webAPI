using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using quizlet_app_webAPI.Data;
using quizlet_app_webAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace quizlet_app_webAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        public static ApplicationUser newUser = new ApplicationUser();
        private readonly IConfiguration configuration;
        private readonly WordsModuleAPIDbContext _dbContext;
        public AuthController(IConfiguration configuration, WordsModuleAPIDbContext dbContext)
        {
            this.configuration = configuration;
            this._dbContext = dbContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            newUser.Id = Guid.NewGuid();
            newUser.UserName = request.UserName;
            newUser.Email = request.Email;
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            await _dbContext.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();

            return Ok(newUser);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto request)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.UserName == request.UserName);
            if (user == null)
            {
                return NotFound();
            }
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("inccorect password");
            }

            string token = CreateToken(newUser);
            return Ok(token);
        }

        private string CreateToken(ApplicationUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);
            var jwt  = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
