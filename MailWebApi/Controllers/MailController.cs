using MailWebApi.EmailService;
using MailWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace MailWebApi.Controllers
{
    /// <summary>
    /// Контроллер писем.
    /// </summary>
    [ApiController]
    [Route("/api/mails")]
    public class MailController : ControllerBase
    {
        /// <summary>
        /// Интерфейс сервиса отправки сообщений.
        /// </summary>
        private readonly IMailService _mailService;

        /// <summary>
        /// Контекст данных.
        /// </summary>
        private readonly ApplicationContext _context;

        /// <summary>
        /// Инициализация контроллера.
        /// </summary>
        /// <param name="mailService">Сервис отправки сообщений.</param>
        /// <param name="context">Контекст данных.</param>
        public MailController(IMailService mailService, ApplicationContext context)
        {
            _mailService = mailService;
            _context = context;
        }

        /// <summary>
        /// Отправка email сообщения.
        /// </summary>
        /// <param name="emailBody">Тело сообщения.</param>
        /// <returns>Код отправки сообщения.</returns>
        [HttpPost]
        public async Task<IActionResult> SendMessage(EmailBody emailBody)
        {
            bool result = await _mailService.SendAsync(emailBody);

            var emails = new StringBuilder();

            foreach (var email in emailBody.Recipients)
            {
                emails.Append(email + "|");
            }

            emails.Remove(emails.Length - 1, 1);

            if (result)
            {
                await _context.EmailResults.AddAsync(new EmailResult
                {
                    Subject = emailBody.Subject,
                    Body = emailBody.Body,
                    CreationDate = DateTime.Now,
                    Result = "Ok",
                    RecipientList = emails.ToString()
                });

                await _context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, "Mail has successfully been sent.");
            }
            else
            {
                await _context.EmailResults.AddAsync(new EmailResult
                {
                    Subject = emailBody.Subject,
                    Body = emailBody.Body,
                    CreationDate = DateTime.Now,
                    Result = "Failed",
                    FailedMessage = _mailService.ErrorMessage,
                    RecipientList = emails.ToString()
                });

                await _context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status400BadRequest, "An error occured. The Mail could not be sent.");
            }
        }

        /// <summary>
        /// Получение результатов всех отправленных сообщений.
        /// </summary>
        /// <returns>Список результатов.</returns>
        [HttpGet]
        public async Task<List<EmailResult>> GetAllEmails()
        {
            var result = await _context.EmailResults.ToListAsync();

            return result;
        }
    }
}
