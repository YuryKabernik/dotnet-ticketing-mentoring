﻿namespace Ticketing.DataAccess.Entities.Venue;

public class Row
{
    public required int Id { get; set; }
    public required string Label { get; set; }
    public required int SectionId { get; set; }
}