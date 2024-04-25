var builder = WebApplication.CreateBuilder(args);
//add task service

// Leer la expresión cron desde configuración o usar la por defecto
string cronSchedule = builder.Configuration.GetValue<string>("CronSchedule") ?? "0 0,12 * * *"; // Cada medianoche y mediodía

// Registro del servicio cron
builder.Services.AddSingleton<CronJobService>(serviceProvider =>
{
    var logger = serviceProvider.GetRequiredService<ILogger<CronJobService>>();
    return new CronJobService(cronSchedule, logger);
});
builder.Services.AddHostedService(provider => provider.GetRequiredService<CronJobService>());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
