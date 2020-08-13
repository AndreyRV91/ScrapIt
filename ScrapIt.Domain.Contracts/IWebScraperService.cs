using ScrapIt.Domain.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScrapIt.Domain.Contracts
{
    public interface IWebScrapperService
    {
        Task<List<CarDto>> Get(int taskId);

        Task Create(long taskId, string url, int pagesCountToScrap);
    }
}