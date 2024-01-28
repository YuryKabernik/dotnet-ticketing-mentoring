using Ticketing.Notification.Common;
using Ticketing.Notification.Service;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();
builder.Services.AddNotifications(builder.Configuration);

var host = builder.Build();
host.Run();
