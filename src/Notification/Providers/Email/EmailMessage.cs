namespace Ticketing.Notification.Service.Providers.Email;

public class EmailMessage
{
    public required EmailFromSection From { get; set; }

    public required IList<EmailToSection> To { get; set; }

    public string Subject { get; set; } = string.Empty;

    public string TextPart { get; set; } = string.Empty;

    public string HTMLPart { get; set; } = string.Empty;
}

public class EmailFromSection
{
    public required string Email { get; set; }

    public required string Name { get; set; }
}

public class EmailToSection
{
    public required string Email { get; set; }

    public required string Name { get; set; }
}
