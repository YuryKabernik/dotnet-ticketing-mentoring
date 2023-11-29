namespace Ticketing.WebApi.Events.Models;

/// <summary>
/// An event details contract.
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="DateTime"></param>
/// <param name="Place"></param>
public record OrganizedEvent(int Id, string Name, DateTime DateTime, string Place);
