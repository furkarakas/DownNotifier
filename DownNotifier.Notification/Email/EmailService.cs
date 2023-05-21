using DownNotifier.Notification.Email.Model;
using MailKit.Net.Smtp;
using MimeKit;
using System.Net; 

namespace DownNotifier.Notification.Email
{
    public class EmailService : IEmailService
    {
        public bool SendEmail(SendEmailModel sendEmailModel)
        {
            try
            { 
                var email = new MimeMessage();

                email.From.Add(new MailboxAddress("demodownnotifier@gmail.com", "demodownnotifier@gmail.com"));
                email.To.Add(new MailboxAddress(sendEmailModel.To.FirstOrDefault(), sendEmailModel.To.FirstOrDefault()));

                email.Subject = sendEmailModel.Subject;
                email.Body = new TextPart(MimeKit.Text.TextFormat.Text)
                {
                    Text = sendEmailModel.Content
                };

                using (var smtp = new SmtpClient())
                {
                    smtp.Connect("smtp.gmail.com", 587, false);

                    smtp.Authenticate("demodownnotifier@gmail.com", "zpypnjrhdjpcmdiv");

                    smtp.Send(email);
                    smtp.Disconnect(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
