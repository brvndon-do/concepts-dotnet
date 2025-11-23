using Concepts.Server.Services.OpenAi;
using Concepts.Server.Utilities.AuthenticatedRequest;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.Configure<OpenAiOptions>(builder.Configuration.GetSection("OpenAi"));

builder.Services.AddScoped<AuthenticatedRequestFilter>();

builder.Services.AddSingleton<IOpenAiService, OpenAiService>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
