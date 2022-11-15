using System.Diagnostics.CodeAnalysis;

namespace roommate_app.Controllers.Authentication;

[ExcludeFromCodeCoverage]

public class AppSettings
{
    public string Secret { get; set; }
}