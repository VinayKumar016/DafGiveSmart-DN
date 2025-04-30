using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;

namespace CDTApi.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var apiKey = _configuration["SendGridSettings:ApiKey"];
                var client = new SendGridClient(apiKey);

                var from = new EmailAddress(
                    _configuration["SendGridSettings:SenderEmail"],
                    _configuration["SendGridSettings:SenderName"]
                );

                var to = new EmailAddress(toEmail);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, body, body);
                var response = await client.SendEmailAsync(msg);

                // Log the response for debugging
                Console.WriteLine($"SendGrid Response: {response.StatusCode}");
                if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.Accepted)
                {
                    Console.WriteLine($"Error sending email: {await response.Body.ReadAsStringAsync()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while sending email: {ex.Message}");
                throw;
            }
        }
    }
}