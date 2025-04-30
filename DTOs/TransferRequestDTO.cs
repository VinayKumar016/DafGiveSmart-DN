namespace CDTApi.DTOs
{
    public class TransferRequestDTO
    {
        public int UserId { get; set; }
        public int BrokerageAccountId { get; set; }
        public int DAFAccountId { get; set; }
        public decimal Amount { get; set; }
        public string ReferenceNote { get; set; }
    }


}
