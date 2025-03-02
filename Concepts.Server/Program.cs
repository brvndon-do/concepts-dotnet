using Concepts.Server.Services.OpenAi;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.Configure<OpenAiOptions>(builder.Configuration.GetSection("OpenAi"));

builder.Services.AddScoped<IOpenAiService, OpenAiService>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
