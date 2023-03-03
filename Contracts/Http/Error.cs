using System.Text.Json;

namespace Contracts.Http;

public class Error
{
    public Error(Guid id, string message)
    {
        Id = id;
        Message = message;
    }

    public Guid Id { get; set; }
    public string Message { get; set; }
    public override string ToString() => JsonSerializer.Serialize(this);
}