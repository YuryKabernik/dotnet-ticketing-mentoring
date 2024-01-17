using Ticketing.Domain.Entities.Event;

namespace Ticketing.Tests.Core.DataSeeds.EventRelated
{
    internal class EventRowDataSeed
    {
        private static readonly Faker<EventRow> faker = new Faker<EventRow>()
            .RuleFor(venue => venue.Section, setter => EventSectionDataSeed.Seed());

        public static EventRow Seed() => faker.Generate();
    }
}
