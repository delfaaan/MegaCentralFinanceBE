using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MegaCentralFinanceBE.Models
{
	public class UpdateBpkbDto
	{
		[Column("bpkb_no")]
		[StringLength(100)]
		public string? BpkbNo { get; set; }

		[Column("branch_id")]
		[StringLength(10)]
		public string? BranchId { get; set; }

		[Column("bpkb_date")]
		public DateTime BpkbDate { get; set; }

		[Column("faktur_no")]
		[StringLength(100)]
		public string? FakturNo { get; set; }

		[Column("faktur_date")]
		public DateTime FakturDate { get; set; }

		[Column("location_id")]
		[StringLength(10)]
		public string? LocationId { get; set; }

		[Column("police_no")]
		[StringLength(20)]
		public string? PoliceNo { get; set; }

		[Column("bpkb_date_in")]
		public DateTime BpkbDateIn { get; set; }

		[Column("last_updated_by")]
		[StringLength(10)]
		public string? LastUpdatedBy { get; set; }

		[Column("last_updated_on")]
		public DateTime LastUpdatedOn { get; set; }
	}
}