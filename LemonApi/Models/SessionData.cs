namespace LemonApi.Models;

public class SessionData
{
    public double Duration { get; set; }
    public IEnumerable<PlayerResult> Participants { get; set; }
}
