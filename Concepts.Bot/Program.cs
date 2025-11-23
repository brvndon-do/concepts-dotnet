using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using NetCord.Hosting.Gateway;
using NetCord.Hosting.Services.ApplicationCommands;
using Concepts.Bot.Services.NetCord;
using Concepts.Bot.Services.Query;
using NetCord.Hosting.Services.Commands;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

builder.Configuration.AddUserSecrets<Program>();

builder.Services.Configure<NetCordOptions>(builder.Configuration.GetSection("Discord"));
builder.Services.Configure<QueryOptions>(builder.Configuration.GetSection("Query"));

builder.Services.AddHttpClient<QueryService>();
builder.Services.AddSingleton<IQueryService, QueryService>();
builder.Services
    .AddDiscordGateway()
    .AddGatewayEventHandlers(typeof(Program).Assembly)
    .AddApplicationCommands();

IHost host = builder.Build();

host.AddApplicationCommandModule<QueryModule>();

host.UseGatewayEventHandlers();

await host.RunAsync();