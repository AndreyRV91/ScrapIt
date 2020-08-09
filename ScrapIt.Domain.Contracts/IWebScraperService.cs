using ScrapIt.Domain.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScrapIt.Domain.Contracts
{
    public interface IWebScraperService
    {
        Task<List<CarCreateDto>> GetPageDetails(string url);
    }
}