using Ticketing.Domain.Entities.Event;

namespace Ticketing.Tests.Core.DataSeeds.EventRelated
{
    internal class EventSectionDataSeed
    {
        private static readonly Faker<EventSection> faker = new Faker<EventSection>()
            .RuleFor(venue => venue.Event, setter => EventDataSeed.Seed());

        public static EventSection Seed() => faker.Generate();
    }
}
