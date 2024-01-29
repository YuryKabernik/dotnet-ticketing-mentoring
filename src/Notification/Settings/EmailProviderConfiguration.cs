using System.Net.Http.Headers;
using System.Text;

namespace Ticketing.Notification.Service.Settings;

public class EmailProviderConfiguration
{
    public const string SectionName = "MailingProvider";

    public AuthenticationHeaderValue AuthenticationHeader
    {
        get
        {
            var byteArray = Encoding.UTF8.GetBytes(this.APIKey + ":" + this.SecretKey);

            return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }
    }

    public required string APIKey { get; set; }

    public required string SecretKey { get; set; }

    public required string BaseAddress { get; set; }

    public required SenderSettings Sender { get; set; }
}

public class SenderSettings
{
    public required string Name { get; set; }

    public required string Email { get; set; }
}
