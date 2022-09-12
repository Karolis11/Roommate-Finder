using Microsoft.AspNetCore.Mvc;

namespace roommate_app.Controllers;

[ApiController]
[Route("[controller]")]
public class ServerTestController : ControllerBase
{

    private readonly ILogger<WeatherForecastController> _logger;

    public ServerTestController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetServerTest")]
    public string Get()
    {
        Console.WriteLine("inside ServerTestController::Get()");
        return "{\"server\": \"works\", \"success\": \"true\"}";
    }
}
