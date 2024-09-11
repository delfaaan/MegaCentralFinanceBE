using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MegaCentralFinanceBE.Data;
using MegaCentralFinanceBE.Models.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MegaCentralFinanceBE.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly ApplicationDbContext _db;
		private readonly IConfiguration _configuration;

		public AuthController(ApplicationDbContext db, IConfiguration configuration)
		{
			_db = db;
			_configuration = configuration;
		}

		[HttpPost("login")]
		public IActionResult Login(LoginDto data)
		{
			var user = _db.Users.FirstOrDefault(e => e.UserName == data.UserName && e.Password == data.Password);

			if (user == null)
			{
				return Unauthorized("Invalid credentials.");
			}

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.Name, user.UserName!),
					new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
				}),
				Expires = DateTime.UtcNow.AddHours(1),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
				Issuer = _configuration["Jwt:Issuer"],
				Audience = _configuration["Jwt:Audience"]
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			var jwtToken = tokenHandler.WriteToken(token);

			return Ok(new { Token = jwtToken });
		}
	}
}