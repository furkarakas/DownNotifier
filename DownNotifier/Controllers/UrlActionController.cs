using AutoMapper;
using DownNotifier.BackgroundJob.CheckUrl;
using DownNotifier.Business.UrlDefinition;
using DownNotifier.Business.UrlDefinition.Result;
using DownNotifier.Models.UrlDefinition;
using Hangfire;
using Hangfire.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DownNotifier.Controllers
{
    [Authorize]
    public class UrlActionController : Controller
    {
        private readonly IUrlDefinitionService _urlDefinitionService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public UrlActionController(IUrlDefinitionService urlDefinitionService,
            IMapper mapper,
            UserManager<IdentityUser> userManager)
        {
            _urlDefinitionService = urlDefinitionService;
            _mapper = mapper;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SaveUrlDefinition(UrlDefinitionDto definitionDto)
        {  
            if (!Uri.IsWellFormedUriString(definitionDto.Url, UriKind.Absolute))
                return Json(
                    new SaveUrlDefinitionResult()
                    {
                        Message = "URL formatı uygun değildir. URL https://www.google.com şeklinde olmalı.",
                        Status = false
                    });

            if (string.IsNullOrEmpty(definitionDto.Url))
                return Json(
                    new SaveUrlDefinitionResult()
                    {
                        Message = "URL Boş bırakalamaz.",
                        Status = false
                    });
             
            if (Int32.Parse(definitionDto.TimeInterval) >= 60)
                return Json(
                    new SaveUrlDefinitionResult()
                {
                    Message = "60 dakikadan düşük bir değer giriniz.",
                    Status = false
                });

            var serviceRequest = _mapper.Map<Business.UrlDefinition.Dto.UrlDefinitionDto>(definitionDto);
            var result = _urlDefinitionService.SaveUrlDefinition(serviceRequest);

            if (result.Status)
            {  
                var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();

                RecurringJob.AddOrUpdate<CheckUrlJobManager>(result.ResourceId.ToString(), x => x.CheckUrl(definitionDto.Url, user.UserName), $"*/{serviceRequest.TimeInterval} * * * * *");
            }

            return Json(result);
        }

        public IActionResult GetUrlDefinitionList()
        {
            var urlList = _urlDefinitionService.GetUrlDefinitionList();

            return View("~/Views/UrlAction/PartialViews/UrlListTable.cshtml", urlList);
        }

        public IActionResult GetUrlActionForm(Guid ResourceId)
        {
            var urlDefinition = _urlDefinitionService.GetUrlDefinition(ResourceId);

            return View("~/Views/UrlAction/PartialViews/UrlActionForm.cshtml", urlDefinition);
        }

        public IActionResult DeleteUrlDefinition(Guid ResourceId)
        {
            var result = _urlDefinitionService.DeleteUrlDefinition(ResourceId);
            if (result.Status)
            {
                RecurringJob.RemoveIfExists(ResourceId.ToString());
            }
            return Json(result);
        }
    }
}
