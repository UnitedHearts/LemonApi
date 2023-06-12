using Contracts.Mail;
using EmailService;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Reflection;
using System.Text;

namespace LemonApi.Extensions;

public static class MailExtansion
{
    readonly static string wrap = "wrap.html";
    readonly static string confirmContent = "confirm_content.html";
    public static MailInfo ConfirmMail(string email, string host, string accountName)
    {
        var token = JWTExtansion.GetToken(email);
        var mailStr = Template();
        mailStr.Replace("#content#", LoadFileText(confirmContent));
        mailStr.Replace("#target-name#", accountName);
        mailStr.Replace("#confirm-url#", host + Endpoints.EMAIL_CONFIRM + $"?token={token}");
        var result = mailStr.ToString();
        return new MailBuilder().SetSubject("Регистрация на портале UnitedHearts")
                                .SetHTML(result)
                                .SetEmail(email)
                                .Build();
    }
    static StringBuilder Template()
    {
        //var mailStr = new StringBuilder(mail_wrap);
        var mailStr = new StringBuilder(LoadFileText(wrap));
        mailStr.Replace("#our-email#", OurCompany.EMAIL);
        mailStr.Replace("#our-telephone#", OurCompany.TELEPHONE);
        mailStr.Replace("#Vk-link#", OurCompany.VK);
        return mailStr;
    }

    static string LoadFileText(string staticFilePath)
    {
        var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"wwwroot", staticFilePath);
        var str = File.ReadAllText(path);
        return str;
    }
}