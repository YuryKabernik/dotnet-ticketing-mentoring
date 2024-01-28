using Ticketing.Notification.Common;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddNotifications(builder.Configuration);

var host = builder.Build();
host.Run();
