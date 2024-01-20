using System.ComponentModel.DataAnnotations.Schema;

namespace MailWebApi.Models
{
    /// <summary>
    /// Тело email сообщения.
    /// </summary>
    public class EmailBody
    {
        /// <summary>
        /// Тема сообщения.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Сообщение.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Получатели.
        /// </summary>
        [NotMapped]
        public List<string> Recipients { get; set; }
    }
}
