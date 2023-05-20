using DownNotifier.Business.UrlDefinition.Dto;
using DownNotifier.Business.UrlDefinition.Result;

namespace DownNotifier.Business.UrlDefinition
{
    public interface IUrlDefinitionService
    {
        SaveUrlDefinitionResult SaveUrlDefinition(UrlDefinitionDto definitionDto);
        List<UrlDefinitionDto> GetUrlDefinitionList();
        UrlDefinitionDto GetUrlDefinition(Guid ResourceId);
        DeleteUrlDefinitionResult DeleteUrlDefinition(Guid ResourceId);
    }
}
