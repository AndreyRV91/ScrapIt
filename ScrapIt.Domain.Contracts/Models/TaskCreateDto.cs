using System.Collections.Generic;

namespace ScrapIt.Domain.Contracts.Models
{
    public class TaskCreateDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
