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
    public class TaskScrapperService : ITaskScrapperService
    {
        private readonly IMapper _mapper;
        private readonly IDbRepository _dbRepository;

        public TaskScrapperService(IMapper mapper, IDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
            _mapper = mapper;
        }

        public async Task<List<TaskDto>> GetAll()
        {
            var results = (await _dbRepository.GetAll<TaskEntity>()).ToList();

            var taskGetDto = _mapper.Map<List<TaskEntity>, List<TaskDto>>(results);

            return taskGetDto;
        }

        public async Task<TaskDto> GetById(long id)
        {
            var result = await _dbRepository.GetById<TaskEntity>(id);

            var taskDto = _mapper.Map<TaskDto>(result);

            return taskDto;
        }

        public async Task<long> Create(TaskCreateDto taskCreateDto)
        {
            var entity = _mapper.Map<TaskEntity>(taskCreateDto);

            var result = await _dbRepository.Add(entity);
            await _dbRepository.SaveChangesAsync();

            return result;
        }

        public async Task Update(TaskDto taskDto)
        {
            var entity = _mapper.Map<TaskEntity>(taskDto);

            await _dbRepository.Update(entity);
            await _dbRepository.SaveChangesAsync();
        }

        public async Task Remove(TaskDto taskDto)
        {
            var entity = _mapper.Map<TaskEntity>(taskDto);

            await _dbRepository.Remove(entity);
            await _dbRepository.SaveChangesAsync();
        }
    }
}
