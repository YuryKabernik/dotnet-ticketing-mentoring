using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text.Json;
using Abstracta.JmeterDsl.Core;
using Abstracta.JmeterDsl.Core.ThreadGroups;
using Ticketing.DataAccess;
using Ticketing.Domain.Entities;
using Ticketing.Domain.Entities.Event;
using Ticketing.Tests.Core.DataSeeds;
using Ticketing.Tests.Core.DataSeeds.EventRelated;
using Ticketing.WebApi.IntegrationTests.Fixtures;
using Ticketing.WebApi.Models;
using static Abstracta.JmeterDsl.JmeterDsl;

namespace Ticketing.WebApi.LoadTests;

public class UnitTest1 : IClassFixture<WebApplicationFixture>
{
    private const string EndpointTemplate = "api/orders/carts/{0}";

    private readonly HttpClient _client;
    private readonly WebApplicationFixture _factory;

    public UnitTest1(WebApplicationFixture applicationFixture)
    {
        this._factory = applicationFixture;
        this._client = applicationFixture.CreateClient();
    }

    [Fact]
    public void PostCart_ValidSeat_AddsSeatToCart()
    {
        string selectedSeat = this.CreateSeatSerialized();
        IThreadGroupChild[] testPlanChildren = this.Build_AddSeatToCart_TestSamples(selectedSeat);
        
        TestPlanStats stats = Run(testPlanChildren);

        Assert.True(stats.Overall.SampleTimePercentile99 < TimeSpan.FromSeconds(5));
    }

    private string CreateSeatSerialized()
    {
        var seat = this.InitializeDatabaseWithSeat();

        return JsonSerializer.Serialize<SeatSelection>(new(
            seat.Row.Section.Event.Id,
            seat.Id,
            seat.Price.Id
        ));
    }

    private IThreadGroupChild[] Build_AddSeatToCart_TestSamples(string body)
    {
        var carts = this.InitializeDatabaseWithCarts();

        return carts.Select(
            cart => HttpSampler($"http://localhost:5000/{string.Format(EndpointTemplate, cart.Guid)}")
                .Method(HttpMethod.Post.Method)
                .ContentType(new MediaTypeHeaderValue(MediaTypeNames.Application.Json))
                .Body(body)
        )
        .Cast<IThreadGroupChild>()
            .Append(HttpCache().Disable())
            .Append(HttpCookies().Disable())
        .ToArray();
    }

    private static TestPlanStats Run(IThreadGroupChild[] testPlanChildren)
    {
        var testPlan = TestPlan(
            ThreadGroup("POST Carts with a new selectedSeat simultaneously", 1, 1, testPlanChildren),
            JtlWriter("jtls"),
            ResultsTreeVisualizer()
        );

        return testPlan.Run();
    }

    private IEnumerable<Cart> InitializeDatabaseWithCarts()
    {
        using DataContext dataContext = this._factory.GetDbContext();
        var carts = CartDataSeed.Seed(1000);

        dataContext.AddRange(carts);
        dataContext.SaveChanges();

        return carts;
    }

    private EventSeat InitializeDatabaseWithSeat()
    {
        using DataContext dataContext = this._factory.GetDbContext();
        var seat = EventSeatDataSeed.Seed();

        dataContext.Add(seat);
        dataContext.SaveChanges();

        return seat;
    }
}
