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
    public class WebScraperController : ControllerBase
    {
        private readonly ILogger<WebScraperController> _logger;
        private readonly IWebScraperService _webScraperService;

        public WebScraperController(ILogger<WebScraperController> logger, IWebScraperService webScraperService)
        {
            _logger = logger;
            _webScraperService = webScraperService;
        }

        /// <summary>
        /// Get information from web page in Avito
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(long taskId, string url)
        {
            try
            {
                await _webScraperService.Create(taskId, url);
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
