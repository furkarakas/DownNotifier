namespace DownNotifier.Notification.Email.Model
{
    public class SendEmailModel
    {
        public string From { get; set; } 
        public List<string> To { get; set; } 
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
