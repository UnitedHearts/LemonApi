using Contracts.Mail;
using EmailService;
using System.Text;

namespace LemonApi.Extensions;

public static class MailExtansion
{
    readonly static string mail_wrap = "<html><body style=\"font-family: Helvetica;\"><table style=\"margin: 0;width: 100%;\"><tr style=\"border-bottom: 5px solid orange;padding: .5em 1em;display: flex;justify-content: space-between;align-items: center;\"><td><img style=\"max-width: 75px;\" src=\"http://lonewald.ru/logo.png\"></td><td><h1><span style=\"font-weight: bold;color: #CB000F\">United</span><span style=\"font-weight: bold;color: #5C0A53\">Hearts</span></h1></td></tr></table><table style=\"align-items: center;justify-content: space-around;margin: 50px 0;width: 100%;\">#content# </table><table style=\"border-top: 5px solid #5C0A53;min-width: 600;padding-top: 15px;margin: 0;width: 100%;\"><tr><td><p style=\"text-align: center;\">���� ��������:</p><p style=\"text-align: center;\">�������: <span>#our-telephone#</span></p><p style=\"text-align: center;\">Email: <span>#our-email#</span></p></td><td><p style=\"text-align: center;\">�� � ���. �����:</p><p style=\"text-align: center;\"><a style=\"background-color: #0022cc !important;line-height: 40px;font-size: 22px;color: #fff;font-weight: bold;display: inline-block;border-radius: 10px;text-decoration: none;padding: 0 10px;\" href=\"#Vk-link#\">���������</a></p></td></tr><tr><td colspan=\"2\" style=\"padding-top: 20;\"><p style=\"text-align: center;font-size: 10px;\">���� �� �������� ��� ������, �� �� ���� ������� ������������, ��������� � UnitedHearts, ���������, ��������� � ���� �� ������ #our-email# ��� �� �������� #our-telephone# � ������� ��������</p></td></tr></table></body></html>";
    readonly static string confirm_content = "<td><p style=\"text-align: center; font-size: 24px; font-weight: bold; \">���������� �� �����������</p><p style=\"text-align: center; font-size: 20px;\">�� ������������������ �� ������� <span style=\"font-weight: bold;color: #CB000F\">United</span><span style=\"font-weight: bold;color: #5C0A53\">Hearts</span></p><p style=\"text-align: center; font-size: 20px;\">��� �������������� �����������, ����������, ������� ����������� Email</p><p style=\"text-align: center;\"><a style=\"width: 300px;line-height: 50px;background-color: #5C0A53 !important;line-height: 40px;font-size: 22px;color: #fff;font-weight: bold;display: inline-block;border-radius: 10px;text-decoration: none;padding: 0 10px;\" href=\"#confirm-url#\">����������� Email</a></p><p style=\"text-align: center; font-size: 10px; text-align: center;\">���� �� �� ���������������� �� �������, � �������� ��� ������ �� ������, �������������� ��� ��� ��������� � ���� �� �����</p></td>";
    public static MailInfo ConfirmMail(string email)
    {
        var token = JWTExtansion.GetToken(email);
        var mailStr = Template();
        mailStr.Replace("#content#", confirm_content);
        mailStr.Replace("#confirm-url#", Endpoints.EMAIL_CONFIRM + $"?token={token}");
        var result = mailStr.ToString();
        return new MailBuilder().SetSubject("����������� �� ������� UnitedHearts")
        .SetHTML(result)
        .SetEmail(email)
        .Build();
    }
    static StringBuilder Template()
    {
        var mailStr = new StringBuilder(mail_wrap);
        mailStr.Replace("#our-email#", OurCompany.EMAIL);
        mailStr.Replace("#our-telephone#", OurCompany.TELEPHONE);
        mailStr.Replace("#Vk-link#", OurCompany.VK);
        return mailStr;
    }
}