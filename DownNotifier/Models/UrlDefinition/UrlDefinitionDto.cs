namespace DownNotifier.Models.UrlDefinition
{
    public class UrlDefinitionDto
    {
        public Guid ResourceId { get; set; }
        public string UrlName { get; set; }
        public string Url { get; set; }
        public string TimeInterval { get; set; }
    }
}
