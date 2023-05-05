namespace LemonDB;

public class Account : DbEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool EmailConfirmed { get; set; }
    public bool Active { get; set; }
}