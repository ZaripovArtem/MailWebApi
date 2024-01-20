using MailWebApi.Models;

namespace MailWebApi.EmailService
{
    /// <summary>
    /// Сервис отправки сообщений.
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        /// Сообщение об ошибки.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Отправка сообщения.
        /// </summary>
        /// <param name="emailBody">Тело сообщения.</param>
        /// <returns>True - успешно, false - нет.</returns>
        Task<bool> SendAsync(EmailBody emailBody);
    }
}
