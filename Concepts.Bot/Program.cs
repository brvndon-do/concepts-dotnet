using Microsoft.Extensions.Hosting;
using NetCord.Hosting.Gateway;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddDiscordGateway();

IHost host = builder.Build();

await host.RunAsync();