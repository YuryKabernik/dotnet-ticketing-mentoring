using Ticketing.Tests.Core.DataSeeds.EventRelated;
using Ticketing.Domain.Entities;

namespace Ticketing.Tests.Core.DataSeeds
{
    public class CartDataSeed
    {
        private static readonly Faker<Cart> faker = new Faker<Cart>()
            .RuleFor(venue => venue.Guid, setter => setter.Random.Uuid())
            .RuleFor(venue => venue.CreatedOn, setter => setter.Date.Soon())
            .RuleFor(venue => venue.Seats, setter => EventSeatDataSeed.Seed(3))
            .RuleFor(venue => venue.User, setter => UserDataSeed.Seed());

        public static Cart Seed() => faker.Generate();

        public static IEnumerable<Cart> Seed(int number) => faker.Generate(number);
    }
}
