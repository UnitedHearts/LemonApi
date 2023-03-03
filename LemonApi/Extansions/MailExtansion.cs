using Contracts.Mail;
using EmailService;
using System.Text;

namespace LemonApi.Extensions;

public static class MailExtansion
{
    readonly static string our_telephone = "+7 953 272 00 39";
    readonly static string our_email = "unitedheartsgame@yandex.ru";
    readonly static string vk_link = "https://vk.com/club217904439";
    readonly static string mail_wrap = "<html xmlns=\"http://www.w3.org/1999/xhtml\"><style> .logo{max-width:75;} .red-text{font-weight:bold;color:#CB000F} .violent-text{font-weight:bold;color:#5C0A53} body{font-family:Helvetica;margin:0;} div{padding:020px;} a{text-decoration:none;color:#000;} .soc-btn{line-height: 40px;font-size:22px;margin-bottom:10px;color:#fff;font-weight:bold;display:block;text-align:center;border-radius:10px;}a.vk{background-color:#3b5998 !important;}.header{border-bottom:1pxsolid#ccc;padding:.5em1em;display:flex;justify-content:space-between;align-items:center;}.content{display:flex;align-items:center;justify-content:space-around;}.footer{border-top:1pxsolid#ccc;}.footer-step{align-items:center;justify-content:space-around;display:flex;}.content {display: flex;align-items: center;justify-content: space-around;margin-bottom: 15px;}.confirm-content {size: 20px;}</style><body><header class=\"header\"><div class=\"site-identity\"><img class=\"logo\" src=\"http://lonewald.ru/logo.png\"></div><div><h1><span class=\"red-text\">United</span><span class=\"violent-text\">Hearts</span></h1></div></header><div class=\"content\">#content#</div><footer class=\"footer\"><div class=\"footer-step\"><div><p>Наши контакты:</p><p>Телефон:<span>#our-telephone#</span></p><p>Email:<span>#our-email#</span></p></div><div><p>Мы в соц.сетях:</p><ahref=\"#Vk-link#\" class=\"soc-btnvk\">Вконтакте</a></div></div><div class=\"footer-step\"><div><h6>Если вы получили это письмо, но не вели никакую деятельность, связанную с UnitedHearts, пожалуста, свяжитесь с нами по адресу #our-email# или по телефону #our-telephone# и опишите проблему</h6></div></div></footer></body></html>";
    readonly static string confirm_content = "<div><p style=\"font-size: 24px; font-weight: bold;\">Благодарим за регистрацию</p><p style=\"font-size: 20px;\">Вы зарегестрировались на портале <span class=\"red-text\">United</span><span  class=\"violent-text\">Hearts</span></p>  <p style=\"font-size: 20px;\">Для поддтверждения регистрации, пожалуйста, нажмите подтвердить Email</p><div class=\"content\"><a href=\"#confirm-url#\" class=\"soc-btn confirm\">Подтвердить Email</a><p style=\"font-size: 10px; text-align: center;\">Если вы не регистрировались на портале, и получили это письмо по ошибке, проигнорируйте его или свяжитесь с нами по почте</p></div></div>";
        //readonly static string Url="http://lonewald.ru/account/confirm";
readonly static string Url = "https://localhost:7109/account/confirm";
    public static MailInfo ConfirmMail(string email)
    {
        var token = JWTExtansion.GetToken(email);
        var mailStr = Template() ;
        mailStr.Replace("#content#", confirm_content);
        mailStr.Replace("#confirm-url#", Url + $"?token={token})");
        var result = mailStr.ToString();
        return new MailBuilder().SetSubject("Регистрация на портале UnitedHearts")
        .SetHTML(result)
        .SetEmail(email)
        .Build();
    }
    static StringBuilder Template()
    {
        var mailStr = new StringBuilder(mail_wrap);
        mail_wrap.Replace("#our-email#", our_email);
        mail_wrap.Replace("#our-telephone#", our_telephone);
        mail_wrap.Replace("#Vk-link#", vk_link);
        return mailStr;
    }
}