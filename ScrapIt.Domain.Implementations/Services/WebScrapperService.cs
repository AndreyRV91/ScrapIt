using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.XPath;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ScrapIt.DAL.Contracts;
using ScrapIt.DAL.Contracts.Entities;
using ScrapIt.Domain.Contracts;
using ScrapIt.Domain.Contracts.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrapIt.Domain.Implementations.Services
{
    public class WebScrapperService : IWebScrapperService
    {
        private readonly ILogger<WebScrapperService> _logger;
        private readonly IMapper _mapper;
        private readonly IDbRepository _dbRepository;
        private readonly IScrapper _scrapper;

        public WebScrapperService(ILogger<WebScrapperService> logger, IMapper mapper, IDbRepository dbRepository, IScrapper scrapper)
        {
            _logger = logger;

            _mapper = mapper;
            _dbRepository = dbRepository;
            _scrapper = scrapper;
        }

        public async Task<List<CarDto>> Get(int taskId)
        {
            var results = (await _dbRepository.Get<CarEntity>(c => c.TaskId == taskId)).ToList();

            var taskDto = _mapper.Map<List<CarEntity>, List<CarDto>>(results);

            return taskDto;
        }

        public async Task Create(long taskId, string url, int pagesCountToScrap)
        {
            List<CarDto> carDtoList;

            carDtoList = await _scrapper.GetPageDetails(taskId, url, pagesCountToScrap);
            var entities = _mapper.Map<List<CarDto>, List<CarEntity>>(carDtoList);

            await _dbRepository.AddRange(entities);
            await _dbRepository.SaveChangesAsync();
        }
    }
}
