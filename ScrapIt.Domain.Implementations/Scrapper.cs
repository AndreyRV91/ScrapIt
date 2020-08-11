using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.XPath;
using Microsoft.Extensions.Logging;
using ScrapIt.Domain.Contracts;
using ScrapIt.Domain.Contracts.Models;
using ScrapIt.Domain.Implementations.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrapIt.Domain.Implementations
{
    public class Scrapper: IScrapper
    {
        private readonly IConfiguration _config;
        private readonly IBrowsingContext _browsingContext;
        private readonly ILogger<Scrapper> _logger;

        public Scrapper(ILogger<Scrapper> logger)
        {
            _config = Configuration.Default.WithDefaultLoader().WithXPath();
            _browsingContext = BrowsingContext.New(_config);

            _logger = logger;
        }

        public async Task<List<CarDto>> GetPageDetails(long taskId, string url)
        {
            var document = await GetDocument(String.Format(url, 1));
            var elementList = new List<CarDto>();

            try
            {
                var names = document.Body.SelectNodes("//div[@class='item__line']//span[contains(@itemprop,'name')]");
                var descriptions = document.Body.SelectNodes("//div[@data-marker='item-specific-params']");
                var prices = document.Body.SelectNodes("//span[contains(@class,'snippet-price')]");
                var publishDates = document.QuerySelectorAll(".snippet-date-info").Select(m => m.GetAttribute("data-tooltip")).ToArray();
                var urls = document.QuerySelectorAll(".snippet-link").Select(m => m.GetAttribute("href")).ToArray();

                for (int i = 0; i < 10; i++)
                {
                    elementList.Add(new CarDto
                    {
                        Name = names.Any() ? names[i].Text().NewLineDelete() : default,
                        TaskId = taskId,
                        Description = descriptions.Any() ? descriptions[i].Text().NewLineDelete() : default,
                        Price = prices.Any() ? prices[i].Text().PriceClean() : default,
                        PublishDate = publishDates.Any() ? publishDates[i].ConvertToYYYMMDD() : default,
                        Url = urls.Any() ? urls[i] : default

                    });
                }
            }
            catch (Exception e)
            {
                _logger.LogDebug(1, e.ToString());
                throw;
            }

            return elementList;
        }

        private async Task<IDocument> GetDocument(string url)
        {
            return await _browsingContext.OpenAsync(url);
        }
    }
}
