using DownNotifier.Notification.Email;

namespace DownNotifier.BackgroundJob.CheckUrl
{
    public class CheckUrlJobManager
    {
        private readonly ICheckUrlService _checkUrlService;
        private readonly IEmailService _emailService;

        public CheckUrlJobManager(ICheckUrlService checkUrlService,
            IEmailService emailService)
        {
            _checkUrlService = checkUrlService;
            _emailService = emailService;
        }

        public void CheckUrl(string url, string toMailAddress)
        {
            var result = _checkUrlService.CheckUrl(url);
            if(result)
            {
                _emailService.SendEmail(new Notification.Email.Model.SendEmailModel() { 
                    Content = $"{url} adresinin kontrolü başarılı",
                    Subject = $"{url} kontrol maili",
                    To = new List<string>() { toMailAddress}
                });
            }
            else
            {
                _emailService.SendEmail(new Notification.Email.Model.SendEmailModel()
                {
                    Content = $"{url} adresine ulaşılamıyor",
                    Subject = $"{url} kontrol maili",
                    To = new List<string>() { toMailAddress }
                });
            }

        }
    }
}
