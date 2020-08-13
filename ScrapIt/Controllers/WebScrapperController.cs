using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScrapIt.Domain.Contracts;
using ScrapIt.Domain.Contracts.Models;

namespace ScrapIt.Web.Controllers
{
    /// <summary>
    /// REST API for scrapping Avito
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class WebScrapperController : ControllerBase
    {
        private readonly ILogger<WebScrapperController> _logger;
        private readonly IWebScrapperService _webScraperService;

        public WebScrapperController(ILogger<WebScrapperController> logger, IWebScrapperService webScraperService)
        {
            _logger = logger;
            _webScraperService = webScraperService;
        }

        /// <summary>
        /// Get information from web page in Avito
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(long taskId, string url, int pagesCountToScrap)
        {
            try
            {
                await _webScraperService.Create(taskId, url, pagesCountToScrap);
            }
            catch (Exception ex)
            {
                _logger.LogDebug(1, ex.ToString());
                return NotFound();
            }         
            
            return Ok();
        }

        /// <summary>
        /// Get all cars of task
        /// </summary>
        /// <param name="taskModel"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(int taskId)
        {
            var tasks = await _webScraperService.Get(taskId);

            return Ok(tasks);
        }
    }
}
