namespace LemonApi.Models;

public class CreateSessionData
{
    public string MapId { get; set; }
    public int StartPlayersCount { get; set; }
    public double Duration { get; set; }
    public string GameKey { get; set; }
    public IEnumerable<PlayerResult> Participants { get; set; }
}
