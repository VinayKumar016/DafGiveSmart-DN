namespace CDTApi.DTOs
{
    public class RegisterUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RegistrationSource { get; set; } // "DAF" or "BROKERAGE"

        // Optional fields (used when RegistrationSource == "DAF")
        public string Mobile { get; set; }
        public string Location { get; set; }
    }


}
