using AutoMapper;

namespace DownNotifier.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Business.UrlDefinition.Dto.UrlDefinitionDto, UrlDefinition.UrlDefinitionDto>().ReverseMap();
            CreateMap<Entity.UrlDefinitions.UrlDefinition, Business.UrlDefinition.Dto.UrlDefinitionDto>().ReverseMap();


            
        }
    }
}
