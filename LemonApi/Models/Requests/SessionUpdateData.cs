namespace LemonApi.Models;
public class SessionUpdateData
{
    public string SessionId { get; set; }
    public string State { get; set; }
    public IEnumerable<PlayerResult> Participants { get; set; }
}
