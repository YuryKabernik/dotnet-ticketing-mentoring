using Ticketing.Domain.Entities;

namespace Ticketing.Tests.Core.DataSeeds
{
    internal class UserDataSeed
    {
        private static readonly Faker<User> faker = new Faker<User>()
            .RuleFor(user => user.Name, setter => setter.Person.FirstName)
            .RuleFor(user => user.Surname, setter => setter.Person.LastName)
            .RuleFor(user => user.Email, setter => setter.Person.Email)
            .RuleFor(user => user.Phone, setter => setter.Person.Phone);

        public static User Seed() => faker.Generate();
    }
}
