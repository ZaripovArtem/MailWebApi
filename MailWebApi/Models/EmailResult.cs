namespace MailWebApi.Models
{
    /// <summary>
    /// Результат отправки email.
    /// </summary>
    public class EmailResult : EmailBody
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата создания.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Результат.
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// Сообщение об ошибке.
        /// </summary>
        public string? FailedMessage { get; set; }

        /// <summary>
        /// Список получателей.
        /// </summary>
        public string RecipientList { get; set; }
    }
}
