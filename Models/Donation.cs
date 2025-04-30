namespace CDTApi.Models
{
    public class Donation
    {
        public int DonationId { get; set; }
        public int UserId { get; set; }
        public int CharityId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }

}
