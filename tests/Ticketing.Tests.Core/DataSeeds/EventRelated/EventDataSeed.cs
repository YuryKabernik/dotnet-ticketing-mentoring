using Ticketing.Domain.Entities.Event;
using Ticketing.Tests.Core.DataSeeds.VenueRelated;

namespace Ticketing.Tests.Core.DataSeeds.EventRelated
{
    internal class EventDataSeed
    {
        private static readonly Faker<Event> faker = new Faker<Event>()
            .RuleFor(e => e.Name, setter => setter.Random.Word())
            .RuleFor(e => e.DateTime, setter => setter.Date.Soon())
            .RuleFor(e => e.Venue, setter => VenueDataSeed.Seed());

        public static Event Seed() => faker.Generate();
    }
}
