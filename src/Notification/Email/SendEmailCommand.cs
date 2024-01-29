using Ticketing.Notification.Service.Providers.Email;

namespace Ticketing.Notification.Service.Email;

public class SendEmailCommand
{
    public SendEmailCommand(EmailMessage message)
    {
        this.Method = HttpMethod.Post;
        this.Endpoint = "send";

        this.Messages = new List<EmailMessage> { message };
    }

    public HttpMethod Method { get; }

    public string Endpoint { get; }

    public IList<EmailMessage> Messages { get; }
}
