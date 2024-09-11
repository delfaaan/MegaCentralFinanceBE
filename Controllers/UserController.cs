using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MegaCentralFinanceBE.Data;
using MegaCentralFinanceBE.Models;

namespace MegaCentralFinanceBE.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
	{
		private readonly ApplicationDbContext _db;

		public UserController(ApplicationDbContext db)
		{
			_db = db;
		}

		[Authorize]
		[HttpGet("all")]
		public IActionResult GetAllUsers()
		{
			var users = _db.Users.ToList();

			if (users == null || !users.Any())
			{
				return NotFound("No users found.");
			}

			return Ok(users);
		}

		[Authorize]
		[HttpGet("{id}")]
		public IActionResult GetUserById(long id)
		{
			var user = _db.Users.FirstOrDefault(e => e.UserId == id);

			if (user == null)
			{
				return NotFound($"User with ID {id} not found.");
			}

			return Ok(user);
		}

		[HttpPost("create")]
		public IActionResult CreateUser([FromBody] User user)
		{
			if (_db.Users.Any(e => e.UserName == user.UserName))
			{
				return Conflict($"User with username '{user.UserName}' already exists.");
			}

			_db.Users.Add(user);
			_db.SaveChanges();

			return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
		}

		[Authorize]
		[HttpDelete("{id}")]
		public IActionResult DeleteUser(long id)
		{
			var user = _db.Users.FirstOrDefault(e => e.UserId == id);

			if (user == null)
			{
				return NotFound($"User with ID {id} not found.");
			}

			_db.Users.Remove(user);
			_db.SaveChanges();

			return NoContent();
		}

		[Authorize]
		[HttpPut("{id}")]
		public IActionResult UpdateUser(long id, [FromBody] UpdateUserDto updatedUser)
		{
			var user = _db.Users.FirstOrDefault(e => e.UserId == id);

			if (user == null)
			{
				return NotFound($"User with ID {id} not found.");
			}

			user.UserName = updatedUser.UserName;
			user.Password = updatedUser.Password;
			user.IsActive = updatedUser.IsActive;

			_db.SaveChanges();

			return Ok(user);
		}
	}
}