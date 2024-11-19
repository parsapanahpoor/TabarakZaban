using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using Framework.Infrastructure.Shared.MessageOptions;
using Identity.Application.Services;

namespace Identity.Infrastructure.Services;

public class EmailService(IOptions<SmtpOptions> options) : IMessageService
{
    public async Task SendActivationLink(string? senderAddress, string? targetAddress, string? subject, string message)
    {
        var mailMessage = new MailMessage(senderAddress!, targetAddress!, subject, message);
        using (var client = new SmtpClient(options.Value.Host, options.Value.Port)
        {
            Credentials = new NetworkCredential(options.Value.Username, options.Value.Password)
        })
        {
            await client.SendMailAsync(mailMessage);
        }
    }
}
