namespace CDTApi.DTOs
{
    public class DonationRequestDTO
    {
        public int UserId { get; set; }
        public int CharityId { get; set; }
        public decimal Amount { get; set; }
        public string Email {get; set;}
        public string CharityName { get; set; }
    }

}
