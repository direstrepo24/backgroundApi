using Cronos;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

public class CronJobService : BackgroundService
{
    private readonly CronExpression _expression;
    private readonly TimeZoneInfo _timeZoneInfo;
    private readonly ILogger<CronJobService> _logger;

    public CronJobService(string cronExpression, ILogger<CronJobService> logger,
                          TimeZoneInfo timeZoneInfo = null)
    {
        _expression = CronExpression.Parse(cronExpression ?? "0 0,12 * * *"); // Ejecuta a la medianoche y al mediodía
        _timeZoneInfo = timeZoneInfo ?? TimeZoneInfo.Local;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var next = _expression.GetNextOccurrence(DateTimeOffset.Now, _timeZoneInfo);
            if (next.HasValue)
            {
                var delay = next.Value - DateTimeOffset.Now;
                if (delay > TimeSpan.Zero)
                {
                    _logger.LogInformation("Próxima ejecución programada a las: {Time}", next.Value);
                    await Task.Delay(delay, stoppingToken);
                }

                _logger.LogInformation("Ejecutando tarea programada a las: {Time}", DateTimeOffset.Now);
                await DoWork(stoppingToken);
            }
        }
    }

    private Task DoWork(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Trabajo ejecutado a las: {Time}", DateTimeOffset.Now);
        return Task.CompletedTask; // Implementa la lógica de la tarea aquí
    }

    public async Task PerformWorkNow()
    {
        await DoWork(CancellationToken.None); // Asegúrarse de que DoWork pueda ser llamado de forma segura desde fuera
    }

    public DateTimeOffset? GetNextOccurrence()
    {
        return _expression.GetNextOccurrence(DateTimeOffset.Now, _timeZoneInfo);
    }

}
