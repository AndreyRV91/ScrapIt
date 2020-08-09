using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.XPath;
using Microsoft.Extensions.Logging;
using ScrapIt.DAL.Contracts;
using ScrapIt.Domain.Contracts;
using ScrapIt.Domain.Contracts.Models;
using ScrapIt.Domain.Implementations.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrapIt.Domain.Implementations.Services
{
    public class WebScrapperService : IWebScraperService
    {
        private readonly IConfiguration _config;
        private readonly IBrowsingContext _browsingContext;
        private readonly ILogger _logger;
        private readonly IDbRepository _dbRepository;

        public WebScrapperService(ILogger<IWebScraperService> logger, IDbRepository dbRepository)
        {
            _config = Configuration.Default.WithDefaultLoader().WithXPath();
            _browsingContext = BrowsingContext.New(_config);

            _logger = logger;

            _dbRepository = dbRepository;
        }

        public async Task<List<CarCreateDto>> GetPageDetails(string url)
        {
            var document = await GetDocument(url);
            var elementList = new List<CarCreateDto>();

            try
            {
                var names = document.Body.SelectNodes("//div[@class='item__line']//span[contains(@itemprop,'name')]");
                var descriptions = document.Body.SelectNodes("//div[@data-marker='item-specific-params']");
                var prices = document.Body.SelectNodes("//span[contains(@class,'snippet-price')]");
                var publishDates = document.QuerySelectorAll(".snippet-date-info").Select(m => m.GetAttribute("data-tooltip")).ToArray();
                var urls = document.QuerySelectorAll(".snippet-link").Select(m => m.GetAttribute("href")).ToArray();

                for (int i = 0; i < 5; i++)
                {
                    elementList.Add(new CarCreateDto { Name = names.Any()? names[i].Text().NewLineDelete() : default,
                                                   Description = descriptions.Any()? descriptions[i].Text().NewLineDelete(): default, 
                                                   Price = prices.Any()? prices[i].Text().PriceClean(): default,
                                                   PublishDate = publishDates.Any()? publishDates[i].ConvertToYYYMMDD() : default,
                                                   Url = urls.Any()? urls[i] : default

                    });
                }
            }
            catch(Exception e)
            {
                _logger.LogDebug(1, e.ToString());
            }

            return elementList;
        }

        private async Task<IDocument> GetDocument(string url)
        {
            return await _browsingContext.OpenAsync(url);
        }
    }
}
