using System.Net;

namespace Shared.Result
{
    public record Error(string message, HttpStatusCode StatusCode, string Title) { }
}
