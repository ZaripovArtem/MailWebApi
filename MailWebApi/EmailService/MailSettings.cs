namespace MailWebApi.EmailService
{
    /// <summary>
    /// Настройки для почты.
    /// </summary>
    public class MailSettings
    {
        /// <summary>
        /// Отображаемое имя.
        /// </summary>
        public string? DisplayName { get; set; }

        /// <summary>
        /// От кого.
        /// </summary>
        public string? From { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Хост (для Яндекса - smtp.yandex.ru).
        /// </summary>
        public string? Host { get; set; }

        /// <summary>
        /// Порт (для Яндекса - 465).
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Использование SSL.
        /// </summary>
        public bool UseSSL { get; set; }

        /// <summary>
        /// Использование Tls.
        /// </summary>
        public bool UseStartTls { get; set; }

        /// <summary>
        /// Использование OAuth.
        /// </summary>
        public bool UseOAuth { get; set; }
    }
}
