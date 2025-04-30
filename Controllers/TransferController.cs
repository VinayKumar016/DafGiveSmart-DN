using CDTApi.DTOs;
using CDTApi.Models;
using CDTApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CDTApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransfersController : ControllerBase
    {
        private readonly ITransferRepository _transferRepo;
        private readonly IBrokerageRepository _brokerageRepo;
        private readonly IDAFRepository _dafRepo;

        public TransfersController(
            ITransferRepository transferRepo,
            IBrokerageRepository brokerageRepo,
            IDAFRepository dafRepo)
        {
            _transferRepo = transferRepo;
            _brokerageRepo = brokerageRepo;
            _dafRepo = dafRepo;
        }

        [HttpPost("fund-daf")]
        public IActionResult TransferToDAF([FromBody] TransferRequestDTO dto)
        {
            var brokerage = _brokerageRepo.GetByUserId(dto.UserId);
            var daf = _dafRepo.GetByUserId(dto.UserId);

            if (brokerage == null || daf == null)
                return BadRequest("Account not found.");

            if (dto.Amount <= 0 || dto.Amount > brokerage.AvailableBalance)
                return BadRequest("Invalid or insufficient amount.");

            var transfer = new Transfer
            {
                UserId = dto.UserId,
                BrokerageAccountId = dto.BrokerageAccountId,
                DAFAccountId = dto.DAFAccountId,
                Amount = dto.Amount,
                Date = DateTime.Now,
                Status = "Success",
                ReferenceNote = dto.ReferenceNote
            };

            _transferRepo.AddTransfer(transfer);
            _brokerageRepo.UpdateBalance(brokerage.BrokerageAccountId, dto.Amount);
            _dafRepo.UpdateBalance(daf.DAFAccountId, dto.Amount, isDonation: false);

            return Ok(new { message = "Transfer successful", transferId = transfer.TransferId });
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetTransfersForUser(int userId)
        {
            var transfers = _transferRepo.GetTransfersByUserId(userId);
            return Ok(transfers);
        }
    }
}
