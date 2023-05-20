using LemonDB.Interfaces;

namespace LemonDB;

public class Account : DbEntity, IState
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int Role { get; set; }
    public bool EmailConfirmed { get; set; }
    public bool Active { get; set; }
    public string State { get; set; }

    public Cash Cash { get; set; }
    public AccountStatistic Statistic { get; set; }
    public IEnumerable<Session> Sessions { get; set; }
    public ICollection<Stuff> Stuffs{ get; set; }
}