using LemonDB;

namespace LemonApi.Models;

public class SessionStatusResponse
{
    public Session? Session { get; set; }
    public int Searchers { get; set; }
}
