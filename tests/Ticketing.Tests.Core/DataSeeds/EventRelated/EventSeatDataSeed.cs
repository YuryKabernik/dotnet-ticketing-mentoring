using Ticketing.Domain.Entities.Event;
using Ticketing.Domain.Enums;

namespace Ticketing.Tests.Core.DataSeeds.EventRelated
{
    public class EventSeatDataSeed
    {
        private static readonly Faker<EventSeat> faker = new Faker<EventSeat>()
            .RuleFor(venue => venue.Status, setter => SeatStatusOption.Available)
            .RuleFor(venue => venue.Price, setter => PriceDataSeed.Seed())
            .RuleFor(venue => venue.Row, setter => EventRowDataSeed.Seed());

        public static EventSeat Seed() => faker.Generate();

        public static List<EventSeat> Seed(int number) => faker.Generate(number);
    }
}
