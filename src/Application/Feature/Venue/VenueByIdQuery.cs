﻿using Ticketing.Application.CQRS;
using Ticketing.Domain;
using Ticketing.Domain.Entities.Venue;

namespace Ticketing.Application;

public record VenueSectionsRequest(int VenueId);
public record VenueSectionsResponse(IList<Section> Sections);

public class VenueSectionsQuery : IQueryHandler<VenueSectionsRequest, VenueSectionsResponse>
{
    private readonly IRepository<Venue> repository;

    public VenueSectionsQuery(IRepository<Venue> repository)
    {
        this.repository = repository;
    }

    public async Task<VenueSectionsResponse> Execute(VenueSectionsRequest request)
    {
        var venue = await this.repository.FirstAsync(request.VenueId);

        return new VenueSectionsResponse(venue.Sections?.ToList()!);
    }
}
