namespace Contracts.Mail;
///<summary>
///Обьект, представляющий письмо для отправки на электронную почту
///</summary>
public class MailInfo
{
    public string EmailTo { get; set; }
    public string? Message { get; set; }
    public string? HTMLMessage { get; set; }
    public string? Subject { get; set; }
    public IEnumerable<MailAttachment> Files { get; set; }
}
