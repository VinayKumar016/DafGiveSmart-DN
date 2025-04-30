namespace CDTApi.Models
{
    public class Transfer
    {
        public int TransferId { get; set; }
        public int UserId { get; set; }
        public int BrokerageAccountId { get; set; }
        public int DAFAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } // "Success", "Failed", "Pending"
        public string ReferenceNote { get; set; }
    }

}
