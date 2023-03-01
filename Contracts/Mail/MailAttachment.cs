namespace Contracts.Mail;

/// <summary>
/// Вложения к письму, где файл представлен строкой base64
/// </summary>
public class MailAttachment
{
    public string FileBase64 { get; set; }
    public string Name { get; set; }
}
