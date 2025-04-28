using Application.Domain.Entities;
using AuthoAPI.OTPService.Interface;
using MailKit.Net.Smtp;
using MimeKit;

namespace AuthoAPI.OTPService
{
	public class EmailService : IEmailService
	{
		private readonly EmailSettings _settings;

		public EmailService(IConfiguration configuration)
		{
			_settings = configuration.GetSection("EmailSettings").Get<EmailSettings>();
		}

		public async Task SendEmailAsync(string email, string subject, string message)
		{
			var emailMessage = new MimeMessage();
			emailMessage.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
			emailMessage.To.Add(new MailboxAddress("", email));
			emailMessage.Subject = subject;
			emailMessage.Body = new TextPart("plain") { Text = message };

			using var client = new SmtpClient();
			await client.ConnectAsync(_settings.SmtpServer, _settings.SmtpPort, true);
			await client.AuthenticateAsync(_settings.SmtpUsername, _settings.SmtpPassword);
			await client.SendAsync(emailMessage);
			await client.DisconnectAsync(true);
		}
	}


}
