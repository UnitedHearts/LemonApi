using Contracts.Mail;
using MailKit.Net.Smtp;
using MimeKit;

namespace EmailService
{
    public class SmtpMailSender : IMailSender
    {
        readonly EmailConfiguration _emailConfig;

        public SmtpMailSender(EmailConfiguration emailConfiguration)
        {
            _emailConfig = emailConfiguration;
        }

        public void Send(MailInfo mail)
        {
            var message = CreateEmailMessage(mail);
            SendMessage(message);
        }

        public async Task SendAsync(MailInfo mail)
        {
            var message = CreateEmailMessage(mail);
            await SendMessageAsync(message);
        }

        ////////////////////////////////////////////////////////////////

        /// <summary>
        /// Формирует сообщение
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        MimeMessage CreateEmailMessage(MailInfo data)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.FromName, _emailConfig.FromAsress));
            emailMessage.To.Add(new MailboxAddress("", data.EmailTo));
            emailMessage.Subject = data.Subject ?? "";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = data.Message ?? "";
            foreach (var file in data.Files)
            {
                bodyBuilder.Attachments.Add(file.Name, Convert.FromBase64String(file.FileBase64));
            }
            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        /// <summary>
        /// Отправляет сообщение асинхронно
        /// </summary>
        /// <param name="mailMessage"></param>
        /// <returns></returns>
        async Task SendMessageAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, _emailConfig.IsUseSsl);
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
                    await client.SendAsync(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }

        /// <summary>
        /// Отправляет сообщение
        /// </summary>
        /// <param name="mailMessage"></param>
        /// <returns></returns>
        void SendMessage(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, _emailConfig.IsUseSsl);
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                    client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}