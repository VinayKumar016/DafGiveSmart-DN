namespace CDTApi.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string RegistrationSource { get; set; }
        public string Mobile { get; set; }
        public string Location { get; set; }
        public string? Token { get; set; } // Optional token for JWT authentication
    }



}
