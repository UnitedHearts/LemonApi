namespace Contracts.Http;

public interface IValidatable
{
    public bool IsValid { get; }
    public void Validate();
}