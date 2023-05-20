namespace DownNotifier.Business.UrlDefinition.Dto
{
    public class UrlDefinitionDto
    {
        public Guid ResourceId { get; set; } = Guid.Empty;
        public string UrlName { get; set; }
        public string Url { get; set; }
        public string TimeInterval { get; set; }
    }
}
