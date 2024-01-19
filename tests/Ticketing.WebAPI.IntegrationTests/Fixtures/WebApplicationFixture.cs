using Microsoft.AspNetCore.Mvc.Testing;
using Ticketing.DataAccess;
using WebApiApplication = Ticketing.WebApi.Program;

namespace Ticketing.WebApi.IntegrationTests.Fixtures
{
    public class WebApplicationFixture : WebApplicationFactory<WebApiApplication>
    {
        public WebApplicationFixture()
        {
            this.InitializeDatabase();
        }

        public DataContext GetDbContext()
        {
            IServiceScope scope = this.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;

            return scopedServices.GetRequiredService<DataContext>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Local-QA");
            builder.UseSetting("Database:ConnectionString", "Server=(localdb)\\mssqllocaldb;Database=Ticketing;Trusted_Connection=True;");
        }

        protected void InitializeDatabase()
        {
            using DataContext dataContext = this.GetDbContext();

            dataContext.Database.EnsureDeleted();
            dataContext.Database.EnsureCreated();
        }
    }
}
