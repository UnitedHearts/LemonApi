namespace LemonDB;
public class Session : DbEntity
{
    public DateTime Date { get; set; }
    public double Duration { get; set; }
    public IEnumerable<Account> Participants { get; set; }
}