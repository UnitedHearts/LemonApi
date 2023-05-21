using Contracts.Http;

namespace LemonApi.Models;

public class PaymentAddMoney : IValidatable
{
    public double Money { get; set; }
    public string? Email { get; set; }
    public Guid? AccountId{ get; set; }

    public bool IsValid => throw new NotImplementedException();

    public void Validate()
    {
        throw new NotImplementedException();
    }
}
