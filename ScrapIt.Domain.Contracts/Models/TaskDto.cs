using System.Collections.Generic;

namespace ScrapIt.Domain.Contracts.Models
{
    public class TaskDto
    {
        public long Id { get; set; }

        public string Url { get; set; }
        public string Name { get; set; }
    }
}
