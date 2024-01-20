using MailKit.Net.Smtp;
using MailKit.Security;
using MailWebApi.Models;
using Microsoft.Extensions.Options;
using MimeKit;

namespace MailWebApi.EmailService
{
    /// <summary>
    /// Сервис отправки сообщений.
    /// </summary>
    public class MailService : IMailService
    {
        /// <summary>
        /// Сообщение об ошибке.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Настройки для почты.
        /// </summary>
        private readonly MailSettings _settings;

        /// <summary>
        /// Инициализация сервиса.
        /// </summary>
        /// <param name="settings">Настройки для почты.</param>
        public MailService(IOptions<MailSettings> settings)
        {
            _settings = settings.Value;
        }

        /// <summary>
        /// Отправка сообщений.
        /// </summary>
        /// <param name="emailBody">Тело сообщений.</param>
        /// <returns>True - успешно, false - нет.</returns>
        public async Task<bool> SendAsync(EmailBody emailBody)
        {
            try
            {
                var mail = new MimeMessage();

                mail.From.Add(new MailboxAddress(_settings.DisplayName, _settings.From));
                mail.Sender = new MailboxAddress(_settings.DisplayName, _settings.From);

                foreach (string mailAddress in emailBody.Recipients)
                {
                    mail.To.Add(MailboxAddress.Parse(mailAddress));
                }

                var body = new BodyBuilder();
                mail.Subject = emailBody.Subject;
                body.HtmlBody = emailBody.Body;
                mail.Body = body.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    try
                    {
                        await client.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.SslOnConnect);
                        client.AuthenticationMechanisms.Remove("XOAUTH2");
                        client.Authenticate(_settings.UserName, _settings.Password);
                        await client.SendAsync(mail);
                    }
                    catch (Exception e)
                    {
                        ErrorMessage = e.ToString();
                        throw;
                    }
                    finally
                    {
                        await client.DisconnectAsync(true);
                        client.Dispose();
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                ErrorMessage = e.ToString();

                return false;
            }
        }
    }
}
