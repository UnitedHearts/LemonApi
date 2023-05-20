using Contracts.Http;

namespace LemonApi;
public class AccountCreateInfo : IValidatable
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public bool IsValid
    {
        get { try { this.Validate(); return true; } catch { return false; } }
    }

    public void Validate()
    {
        if (string.IsNullOrEmpty(Name)) throw new Exception("Имя не указано");
        if (string.IsNullOrEmpty(Email)) throw new Exception("Почта не указана");
        if (!Email.Contains('@') || !Email.Contains('.')) throw new Exception("Почта имеет неверный формат");
        if (string.IsNullOrEmpty(Password) || Password.Count() < 6) throw new Exception("Пароль имеет менее 6 символов");
    }
}