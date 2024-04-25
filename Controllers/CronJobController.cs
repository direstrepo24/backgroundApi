using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("[controller]")]
public class CronJobController : ControllerBase
{
    private readonly CronJobService _cronJobService;
    private readonly ILogger<CronJobController> _logger;

    public CronJobController(CronJobService cronJobService, ILogger<CronJobController> logger)
    {
        _cronJobService = cronJobService;
        _logger = logger;
    }

    [HttpGet("run")]
    public async Task<IActionResult> RunJobAsync()
    {
        try
        {
            await _cronJobService.PerformWorkNow();
            return Ok("Tarea ejecutada manualmente.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al ejecutar la tarea manualmente.");
            return StatusCode(500, "Error interno al ejecutar la tarea.");
        }
    }

    [HttpGet("next")]
    public IActionResult GetNextOccurrence()
    {
        var next = _cronJobService.GetNextOccurrence();
        if (next.HasValue)
        {
            return Ok($"La próxima ejecución está programada para: {next.Value.LocalDateTime}");
        }
        else
        {
            return NotFound("No se pudo calcular la próxima ejecución.");
        }
    }
}
