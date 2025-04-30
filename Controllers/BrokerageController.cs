using CDTApi.Models;
using CDTApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CDTApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrokerageController : ControllerBase
    {
        private readonly IBrokerageRepository _brokerageRepo;

        public BrokerageController(IBrokerageRepository brokerageRepo)
        {
            _brokerageRepo = brokerageRepo;
        }

        [HttpPost("create")]
        public IActionResult CreateBrokerageAccount([FromBody] BrokerageAccount account)
        {
            var existing = _brokerageRepo.GetByUserId(account.UserId);
            if (existing != null)
                return BadRequest("Brokerage account already exists for this user.");

            _brokerageRepo.AddBrokerageAccount(account);
            return Ok(new { message = "Brokerage account created." });
        }

        [HttpGet("{userId}")]
        public IActionResult GetBrokerageAccount(int userId)
        {
            var account = _brokerageRepo.GetByUserId(userId);
            if (account == null)
                return NotFound("No brokerage account found.");

            return Ok(account);
        }
    }
}
