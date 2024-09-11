using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MegaCentralFinanceBE.Models
{
	[Table("ms_storage_location")]
	public class StorageLocation
	{
		[Key]
		[Column("location_id")]
		[StringLength(10)]
		public string LocationId { get; set; } = default!;

		[Column("location_name")]
		[StringLength(100)]
		public string? LocationName { get; set; }
	}
}