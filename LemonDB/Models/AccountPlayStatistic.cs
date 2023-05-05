namespace LemonDB;

public class AccountPlayStatistic : DbEntity
{
    public Account Account { get; set; }
    public int Wins { get; set; }
    public int Deaths { get; set; }
    public double Money { get; set; }
    public double TotalMoney { get; set; }
}
