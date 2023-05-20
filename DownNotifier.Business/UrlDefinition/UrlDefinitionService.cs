using AutoMapper;
using DownNotifier.Business.UrlDefinition.Dto;
using DownNotifier.Business.UrlDefinition.Result;
using DownNotifier.DataAccess.Repository;

namespace DownNotifier.Business.UrlDefinition
{
    public class UrlDefinitionService : IUrlDefinitionService
    {

        private readonly IRepository<Entity.UrlDefinitions.UrlDefinition> _urlDefinitionRepository;
        private readonly IMapper _mapper;

        public UrlDefinitionService(IRepository<Entity.UrlDefinitions.UrlDefinition> urlDefinitionRepository,
            IMapper mapper)
        {
            _urlDefinitionRepository = urlDefinitionRepository;
            _mapper = mapper;
        }

        public DeleteUrlDefinitionResult DeleteUrlDefinition(Guid ResourceId)
        {
            var data = _urlDefinitionRepository.FindOne(x => x.ResourceId == ResourceId);
            if (data == null)
            {
                return new DeleteUrlDefinitionResult()
                {
                    Message = "Url tanımı bulunamadı",
                    Status = false
                };
            }

            _urlDefinitionRepository.Delete(data);

            return new DeleteUrlDefinitionResult()
            {
                Message = "Url tanımı başarıyla silindi",
                Status = true
            };
        }

        public UrlDefinitionDto GetUrlDefinition(Guid ResourceId)
        {
            return _mapper.Map<UrlDefinitionDto>(_urlDefinitionRepository.FindOne(x => x.ResourceId == ResourceId) ?? new Entity.UrlDefinitions.UrlDefinition());
        }

        public List<UrlDefinitionDto> GetUrlDefinitionList()
        {
            var urlList = _urlDefinitionRepository.FindAll().ToList();

            return _mapper.Map<List<UrlDefinitionDto>>(urlList);
        }

        public SaveUrlDefinitionResult SaveUrlDefinition(UrlDefinitionDto definitionDto)
        {
            var entity = _mapper.Map<Entity.UrlDefinitions.UrlDefinition>(definitionDto);

            var data = _urlDefinitionRepository.FindOne(x => x.ResourceId == entity.ResourceId);

            if (data != null)
            {
                data.Url = entity.Url;
                data.UrlName = entity.UrlName;
                if(data.TimeInterval != entity.TimeInterval)
                {
                    data.TimeInterval = entity.TimeInterval;
                    data.CreatedDate = DateTime.Now;
                }
                
                var result = _urlDefinitionRepository.Update(data);

                return new SaveUrlDefinitionResult()
                {
                    ResourceId = result.ResourceId,
                    Message = "Kayıt güncelleme işlem başarıyla tamamlandı",
                    Status = true
                };
            }
            else
            {
                var result = _urlDefinitionRepository.Insert(entity);

                if (result.Id > 0)
                { 
                    return new SaveUrlDefinitionResult()
                    {
                        ResourceId = result.ResourceId,
                        Message = "Kayıt ekleme işlem başarıyla tamamlandı",
                        Status = true
                    };
                }
                else
                {
                    return new SaveUrlDefinitionResult()
                    {
                        Message = "Kayıt eklenemedi",
                        Status = false
                    };
                }
            }

            

            
        }



    }
}
