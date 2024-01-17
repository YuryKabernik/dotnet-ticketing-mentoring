﻿using Microsoft.AspNetCore.Mvc.Testing;
using WebApiApplication = Ticketing.WebApi.Program;

namespace Ticketing.WebAPI.IntegrationTests.Fixtures
{
    public class WebApplicationFixture : WebApplicationFactory<WebApiApplication>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Development");
            builder.UseSetting("Database:ConnectionString", "Server=(localdb)\\mssqllocaldb;Database=Ticketing;Trusted_Connection=True;");
        }
    }
}
