using Ticketing.Domain.Entities.Event;

namespace Ticketing.DataAccess.UnitTests.DataSeeds
{
    internal class PriceDataSeed
    {
        private static readonly Faker<Price> faker = new Faker<Price>()
            .RuleFor(price => price.Name, setter => "Standard")
            .RuleFor(price => price.Amount, setter => decimal.Parse(setter.Commerce.Price()));

        public static Price Seed() => faker.Generate();
    }
}
