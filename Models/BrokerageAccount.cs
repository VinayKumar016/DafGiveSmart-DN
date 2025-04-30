namespace CDTApi.Models
{
    public class BrokerageAccount
    {
        public int BrokerageAccountId { get; set; }
        public int UserId { get; set; }
        public string AccountNumber { get; set; }
        public decimal AvailableBalance { get; set; }
        public DateTime LinkedDate { get; set; }
    }

}
