using System.Net.Http.Json;
using Ticketing.Notification.Service.Email;
using Ticketing.Notification.Service.Providers.Email.Interfaces;

namespace Ticketing.Notification.Service.Providers.Email;

public class EmailProvider : IEmailProvider
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<EmailProvider> _logger;

    public EmailProvider(HttpClient httpClient, ILogger<EmailProvider> logger)
    {
        this._httpClient = httpClient;
        this._logger = logger;
    }

    public async Task SendAsync(SendEmailCommand command, CancellationToken cancellationToken)
    {
        var body = new { command.Messages };
        var response = await this._httpClient.PostAsJsonAsync(command.Endpoint, body, cancellationToken);

        await this.ValidateResponse(response, command.Messages.First().To.First());
    }

    private async Task ValidateResponse(HttpResponseMessage response, EmailToSection toSection)
    {
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();

            string message = string.Format(
                "Can't send email to the recipient {0} at email {1}, 3-rd party HTTP response {2}",
                toSection.Name,
                toSection.Email,
                error
            );

            this._logger.LogCritical(message);
            throw new InvalidOperationException(message);
        }
    }
}
