using Contracts;
using Contracts.Mail;
using Alias = Contracts.Mail.MailInfo;
using ThisBuilder = EmailService.MailBuilder;
namespace EmailService;

public class MailBuilder : IBuilder<Alias>
{
    Alias _alias { get; set; }
    public MailBuilder()
    {
        _alias = new() { Files = Array.Empty<MailAttachment>() };
    }
    public MailBuilder(Alias alias)
    {
        _alias = alias;
    }
     public ThisBuilder SetEmail(string email)
    {
        _alias.EmailTo = email;
        return this;
    }
    public ThisBuilder SetSubject(string subject)
    {
        _alias.Subject = subject;
        return this;
    }
    public ThisBuilder SetMessage(string message)
    {
        _alias.Message = message;
        return this;
    }
    public ThisBuilder SetHTML(string html)
    {
        _alias.HTMLMessage = html;
        return this;
    }
    public Alias Build()
    {
        var temp = _alias;
        _alias = new() { Files = Array.Empty<MailAttachment>() };
        return temp;
    }
}