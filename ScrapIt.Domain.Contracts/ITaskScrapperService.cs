using ScrapIt.Domain.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScrapIt.Domain.Contracts
{
    public interface ITaskScrapperService
    {
        Task<List<TaskDto>> GetAll();

        Task<TaskDto> GetById(long id);

        Task<long> Create(TaskCreateDto taskCreateDto);

        Task Update(TaskDto taskDto);

        Task Remove(TaskDto taskDto);
    }
}