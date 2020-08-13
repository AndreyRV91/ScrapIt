using ScrapIt.Domain.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScrapIt.Domain.Contracts
{
    public interface IScrapper
    {
        Task<List<CarDto>> GetPageDetails(long taksId, string url, int pagesCountToScrap);
    }
}
