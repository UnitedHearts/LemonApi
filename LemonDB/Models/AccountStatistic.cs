namespace LemonDB;

public class AccountStatistic : DbEntity
{
    public Guid AccountId{ get; set; }
    public int Plays { get; set; }
    public int Wins { get; set; }
    public int Deaths { get; set; }
}
