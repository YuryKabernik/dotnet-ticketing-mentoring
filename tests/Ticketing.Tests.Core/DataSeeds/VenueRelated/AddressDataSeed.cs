using VenueAddress = Ticketing.Domain.Entities.Venue.Address;

namespace Ticketing.Tests.Core.DataSeeds.VenueRelated
{
    internal class AddressDataSeed
    {
        private static readonly Faker<VenueAddress> faker = new Faker<VenueAddress>()
            .RuleFor(address => address.Country, setter => setter.Address.Country())
            .RuleFor(address => address.City, setter => setter.Address.City())
            .RuleFor(address => address.Street, setter => setter.Address.StreetAddress())
            .RuleFor(address => address.Building, setter => setter.Address.BuildingNumber());

        public static VenueAddress Seed() => faker.Generate();
    }
}
