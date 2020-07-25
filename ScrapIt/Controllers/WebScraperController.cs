using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScrapIt.Domain.Contracts;
using ScrapIt.Domain.Contracts.Models;

namespace ScrapIt.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WebScraperController : ControllerBase
    {
        private readonly ILogger<WebScraperController> _logger;
        private readonly IWebScraperService _webScraperService;

        public WebScraperController(ILogger<WebScraperController> logger, IWebScraperService webScraperService)
        {
            _logger = logger;
            _webScraperService = webScraperService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<CarModel> carModelList;

            try
            {
                carModelList = await _webScraperService.GetPageDetails(@"https://www.avito.ru/moskva/avtomobili/haval-ASgBAgICAUTgtg2umCg?radius=200&p=1");

            }
            catch (Exception ex)
            {
                _logger.LogDebug(1, ex.ToString());
                return NotFound();
            }         
            
            return Ok(carModelList);
        }
    }
}
