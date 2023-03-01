namespace Contracts.Mail;
public interface IMailSender
{

    /// <summary>
    /// Отправляет письмо
    /// </summary>
    /// <param name="mail"></param>
    /// <returns></returns>
    public void Send(MailInfo mail);

    /// <summary>
    /// Ассинхронно отправляет письмо
    /// </summary>
    /// <param name="mail"></param>
    /// <returns></returns>
    public Task SendAsync(MailInfo mail);
}
