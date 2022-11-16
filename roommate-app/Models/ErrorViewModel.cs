using System.Diagnostics.CodeAnalysis;

namespace roommate_app.Models
{
    [ExcludeFromCodeCoverage]
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}