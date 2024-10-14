
using System.Net;
using System.Net.Mail;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class MailService: IMailService
{
    
    private readonly IConfiguration _configuration;
    private readonly SmtpClient _smtpClient;
    private readonly ILoggingService _log;

    public MailService(IConfiguration configuration, ILoggingService log)
    {
        _configuration = configuration;
        _log = log;
        _smtpClient = new SmtpClient
        {
            Host = _configuration["SMTP_HOST"],
            Port = int.Parse(_configuration["SMTP_PORT"]),
            Credentials = new NetworkCredential(
                _configuration["SMTP_USERNAME"],
                _configuration["SMTP_PASSWORD"]
            ),
            EnableSsl = true
        };
    }
    
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        try
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["FROM"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(to);

            await _smtpClient.SendMailAsync(mailMessage);
            _log.LogInformation($"Email sent to {to}, subject: {subject}, body: {body}");
        }
        catch (Exception e)
        {
            _log.LogError("Error in MailService", e);
            throw;
        }
    }
}