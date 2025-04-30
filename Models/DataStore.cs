namespace CDTApi.Models
{
    public class DataStore
    {
        public List<User> Users { get; set; } = new();
        public List<DAFAccount> DAFAccounts { get; set; } = new();
        public List<BrokerageAccount> BrokerageAccounts { get; set; } = new();
        public List<Transfer> Transfers { get; set; } = new();
        public List<Charity> Charities { get; set; } = new();
        public List<Donation> Donations { get; set; } = new();
    }

}
