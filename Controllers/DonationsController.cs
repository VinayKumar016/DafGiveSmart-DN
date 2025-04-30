using CDTApi.DTOs;
using CDTApi.Models;
using CDTApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CDTApi.Services; 
namespace CDTApi.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class DonationsController : ControllerBase
    {
        private readonly IDonationRepository _donationRepo;
        private readonly IDAFRepository _dafRepo;
        private readonly EmailService _emailService;

        public DonationsController(IDonationRepository donationRepo, IDAFRepository dafRepo, EmailService emailService)
        {
            _donationRepo = donationRepo;
            _dafRepo = dafRepo;
            _emailService = emailService;
        }

        [HttpPost("add")]
        public IActionResult AddDonation([FromBody] DonationRequestDTO dto)
        {
            var daf = _dafRepo.GetByUserId(dto.UserId);
            if (daf == null)
                return NotFound("DAF account not found.");

            if (dto.Amount <= 0 || dto.Amount > daf.DAFBalance)
                return BadRequest("Invalid or insufficient donation amount.");

            var donation = new Donation
            {
                UserId = dto.UserId,
                CharityId = dto.CharityId,
                Amount = dto.Amount
            };

            string subject = "Thank you for your donation!";
            string body = $@"
                            <h2>Dear Donor,</h2>
                            <p>Thank you for your generous donation of <b>₹ {dto.Amount} </b> to <b>{dto.CharityName}</b>.</p>
                            
                            <p>Your support is greatly appreciated and will make a significant impact.</p>
                            <br />
                            <p><i>Donor Advisor Fund Team</i></p>.";
            _emailService.SendEmailAsync(dto.Email, subject, body);

            _donationRepo.AddDonation(donation);
            _dafRepo.UpdateBalance(daf.DAFAccountId, dto.Amount, isDonation: true);

            return Ok(new { message = "Donation successful", donationId = donation.DonationId });
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetUserDonations(int userId)
        {
            var list = _donationRepo.GetDonationsByUserId(userId);
            return Ok(list);
        }
    }
}
