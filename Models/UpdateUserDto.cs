using System.ComponentModel.DataAnnotations.Schema;

namespace MegaCentralFinanceBE.Models
{
	[Table("ms_user")]
	public class UpdateUserDto
	{
		[Column("user_name")]
		public string? UserName { get; set; }
		[Column("password")]
		public string? Password { get; set; }
		[Column("is_active")]
		public bool IsActive { get; set; }
	}
}