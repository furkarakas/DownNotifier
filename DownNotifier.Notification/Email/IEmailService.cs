using DownNotifier.Notification.Email.Model;

namespace DownNotifier.Notification.Email
{
    public interface IEmailService
    {
        bool SendEmail(SendEmailModel sendEmailModel);
    }
}
