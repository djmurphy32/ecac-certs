using Ecac.Certs.Api.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
configuration.AddEnvironmentVariables();
builder.Services.AddControllers();
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
    opts.EnableAnnotations();
});

builder.Services.AddScoped<ISpreadsheetParser, SpreadsheetParser>();

var app = builder.Build();
app.UseHealthChecks("/healthcheck");
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.UseHttpsRedirection();

app.Run();
