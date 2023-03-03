using Contracts.Http;

namespace LemonApi;
public class UpdateInfo : IValidatable
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsValid
    {
        get { try { this.Validate(); return true; } catch { return false; } }
    }

    public void Validate()
    {
        if (Id == null) throw new Exception("Id не указан");
        if (string.IsNullOrEmpty(Name)) throw new Exception("Новое имя не указано");
        if (string.IsNullOrEmpty(Email)) throw new Exception("Новая почта не указана");
        if (string.IsNullOrEmpty(Password)) throw new Exception("Новый пароль не указан");
    }
}