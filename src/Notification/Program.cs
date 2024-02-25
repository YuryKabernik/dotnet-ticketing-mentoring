using Microsoft.AspNetCore.Builder;
using Ticketing.Notification.Contracts;
using Ticketing.Notification.Service.HttpPolicies;
using Ticketing.Notification.Service.Settings;
using Ticketing.Notification.Startup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureOptions<EmailProviderConfigureOptions>();

builder.Services.AddNotifications();
builder.Services
    .AddEmailProviderHttpClient()
    .AddPolicyHandler(RetryHttpPolicy.Policy);

// Health checks
builder.Services
    .AddHealthChecks()
    .AddMessageQueueHealthCheck();

var app = builder.Build();

app.MapLivenessEndpoint();
app.MapReadinessEndpoint();

app.Run();
