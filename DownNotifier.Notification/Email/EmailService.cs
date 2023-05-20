using DownNotifier.Notification.Email.Model;
using System.Net.Mail;

namespace DownNotifier.Notification.Email
{
    public class EmailService : IEmailService
    {
        public bool SendEmail(SendEmailModel sendEmailModel)
        {
            try
            {
                MailMessage email = new MailMessage();
                email.From = new MailAddress("email");
                email.To.Add(sendEmailModel.To.FirstOrDefault());
                email.Subject = sendEmailModel.Subject;
                email.Body = sendEmailModel.Content;
                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new System.Net.NetworkCredential("email", "password");
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.SendAsync(email, (object)email);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
