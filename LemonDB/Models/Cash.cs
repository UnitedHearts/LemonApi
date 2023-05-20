namespace LemonDB;

public class Cash : DbEntity
{
    public Guid AccountId { get; set; }
    public double Current { get; set; }
    public double TotalHistory { get; set; }
}
