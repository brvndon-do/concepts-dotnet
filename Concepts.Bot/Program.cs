using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using NetCord.Hosting.Gateway;
using NetCord.Hosting.Services.ApplicationCommands;
using Concepts.Bot.Services.NetCord;
using Concepts.Bot.Services.Query;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

builder.Services.Configure<NetCordOptions>(builder.Configuration.GetSection("Discord"));
builder.Services.Configure<QueryOptions>(builder.Configuration.GetSection("Query"));

builder.Services.AddHttpClient();
builder.Services.AddScoped<IQueryService, QueryService>();
builder.Services
    .AddDiscordGateway()
    .AddGatewayEventHandlers(typeof(Program).Assembly)
    .AddApplicationCommands();

IHost host = builder.Build();

host.AddApplicationCommandModule<QueryModule>();

host.UseGatewayEventHandlers();

await host.RunAsync();