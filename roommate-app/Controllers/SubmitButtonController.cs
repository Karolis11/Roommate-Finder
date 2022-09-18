using Microsoft.AspNetCore.Mvc;

namespace roommate_app.Controllers;

[ApiController]
[Route("[controller]")]
public class submitButtonController : ControllerBase
{

    private readonly ILogger<WeatherForecastController> _logger;

    public ServerTestController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "PostSubmitListing")]
    public string Post()
    {
        Console.WriteLine("VIDUJ");
        return "{\"server\": \"works\", \"success\": \"true\"}";
    }
}