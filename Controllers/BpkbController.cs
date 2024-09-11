using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MegaCentralFinanceBE.Data;
using MegaCentralFinanceBE.Models;

namespace MegaCentralFinanceBE.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BpkbController : ControllerBase
	{
		private readonly ApplicationDbContext _db;

		public BpkbController(ApplicationDbContext db)
		{
			_db = db;
		}

		[Authorize]
		[HttpGet("all")]
		public IActionResult GetAllBpkbs()
		{
			var bpkbs = _db.Bpkbs.ToList();

			if (bpkbs == null || !bpkbs.Any())
			{
				return NotFound("No data found.");
			}

			return Ok(bpkbs);
		}

		[Authorize]
		[HttpGet("{agreementNumber}")]
		public IActionResult GetBpkbById(string agreementNumber)
		{
			var bpkbs = _db.Bpkbs.FirstOrDefault(e => e.AgreementNumber == agreementNumber);

			if (bpkbs == null)
			{
				return NotFound($"Data with ID {agreementNumber} not found.");
			}

			return Ok(bpkbs);
		}

		[HttpPost("create")]
		public IActionResult CreateBpkb([FromBody] Bpkb bpkb)
		{
			if (_db.Bpkbs.Any(e => e.BpkbNo == bpkb.BpkbNo))
			{
				return Conflict($"Data with BPKB number '{bpkb.BpkbNo}' already exists.");
			}

			_db.Bpkbs.Add(bpkb);
			_db.SaveChanges();

			return CreatedAtAction(nameof(GetBpkbById), new { agreementNumber = bpkb.AgreementNumber }, bpkb);
		}

		[Authorize]
		[HttpDelete("{agreementNumber}")]
		public IActionResult DeleteBpkb(string agreementNumber)
		{
			var bpkb = _db.Bpkbs.FirstOrDefault(e => e.AgreementNumber == agreementNumber);

			if (bpkb == null)
			{
				return NotFound($"Data with Agreement Number {agreementNumber} not found.");
			}

			_db.Bpkbs.Remove(bpkb);
			_db.SaveChanges();

			return NoContent();
		}

		[Authorize]
		[HttpPut("{agreementNumber}")]
		public IActionResult UpdateBpkb(string agreementNumber, [FromBody] UpdateBpkbDto updatedBpkb)
		{
			var bpkb = _db.Bpkbs.FirstOrDefault(e => e.AgreementNumber == agreementNumber);
			var locId = _db.StorageLocations.FirstOrDefault(e => e.LocationId == updatedBpkb.LocationId);

			if (bpkb == null)
			{
				return NotFound($"Data with Agreement Number {agreementNumber} not found.");
			}
			else if (locId == null)
			{
				return NotFound($"Location ID {updatedBpkb.LocationId} not found.");
			}

			bpkb.BpkbNo = updatedBpkb.BpkbNo;
			bpkb.BranchId = updatedBpkb.BranchId;
			bpkb.BpkbDate = updatedBpkb.BpkbDate;
			bpkb.FakturNo = updatedBpkb.FakturNo;
			bpkb.FakturDate = updatedBpkb.FakturDate;
			bpkb.LocationId = updatedBpkb.LocationId;
			bpkb.PoliceNo = updatedBpkb.PoliceNo;
			bpkb.BpkbDateIn = updatedBpkb.BpkbDateIn;
			bpkb.LastUpdatedBy = updatedBpkb.LastUpdatedBy;
			bpkb.LastUpdatedOn = DateTime.Now;

			_db.SaveChanges();

			return Ok(bpkb);
		}
	}
}