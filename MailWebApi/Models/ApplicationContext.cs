using Microsoft.EntityFrameworkCore;

namespace MailWebApi.Models
{
    /// <summary>
    /// Контекст данных.
    /// </summary>
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// Результат отправки сообщения.
        /// </summary>
        public DbSet<EmailResult> EmailResults { get; set; } = null!;

        /// <summary>
        /// Контекст данных.
        /// </summary>
        /// <param name="options">Параметры.</param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
    }
}
