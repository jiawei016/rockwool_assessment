using Serilog;
using Serilog.Events;
using web_api.DependencyModule;

var builder = WebApplication.CreateBuilder(args);

var allowSpecificOrigins = "_allowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowSpecificOrigins,
    policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IConfiguration _IConfiguration = builder.Configuration;

new RepoModule().ConfigureServices(builder.Services);
new OtherAPIModule().ConfigureServices(builder.Services);
new ExtensionModule().ConfigureServices(builder.Services);

using var log = new LoggerConfiguration()
              .WriteTo.Logger(lc => lc.Filter
                .ByIncludingOnly(e => e.Level == LogEventLevel.Information || e.Level == LogEventLevel.Debug)
                .WriteTo.File($"Logs/info-{DateTimeOffset.Now.DateTime.ToString("yyyyMMdd")}.txt"))
              .WriteTo.Logger(lc => lc.Filter
                .ByIncludingOnly(e => e.Level == LogEventLevel.Error)
                .WriteTo.File($"Logs/error-{DateTimeOffset.Now.DateTime.ToString("yyyyMMdd")}.txt"))
              .CreateLogger();

builder.Services.AddSingleton<Serilog.ILogger>(log);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
