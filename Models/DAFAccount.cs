namespace CDTApi.Models
{
    public class DAFAccount
    {
        public int DAFAccountId { get; set; }       // existing
        public int UserId { get; set; }             // existing
        public string AccountNumber { get; set; }   // existing
        public decimal DAFBalance { get; set; }     // existing
        public decimal TotalDonated { get; set; }   // ✅ already in your flow and important
    }


}
