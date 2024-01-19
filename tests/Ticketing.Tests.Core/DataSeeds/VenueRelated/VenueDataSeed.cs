using Ticketing.Domain.Entities.Venue;

namespace Ticketing.Tests.Core.DataSeeds.VenueRelated
{
    internal class VenueDataSeed
    {
        private static readonly Faker<Venue> faker = new Faker<Venue>()
            .RuleFor(venue => venue.Name, setter => setter.Random.Word())
            .RuleFor(venue => venue.Address, setter => AddressDataSeed.Seed());

        public static Venue Seed() => faker.Generate();
    }
}
