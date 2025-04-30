using CDTApi.Models;
using CDTApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CDTApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DAFController : ControllerBase
    {
        private readonly IDAFRepository _dafRepo;

        public DAFController(IDAFRepository dafRepo)
        {
            _dafRepo = dafRepo;
        }

        [HttpGet("{userId}")]
        public IActionResult GetByUserId(int userId)
        {
            var daf = _dafRepo.GetByUserId(userId);
            if (daf == null)
                return NotFound("DAF account not found.");

            return Ok(daf);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] DAFAccount newAccount)
        {
            var existing = _dafRepo.GetByUserId(newAccount.UserId);
            if (existing != null)
                return BadRequest("DAF account already exists.");

            _dafRepo.AddDAFAccount(newAccount);
            return Ok("DAF account created.");
        }
    }
}
