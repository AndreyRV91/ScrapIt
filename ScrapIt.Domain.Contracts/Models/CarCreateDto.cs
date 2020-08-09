
using System;

namespace ScrapIt.Domain.Contracts.Models
{
    public class CarCreateDto: RowBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; }
        public DateTime? PublishDate { get; set; }
        public string Url { get; set; }

        public TaskCreateDto TaskModel { get; set; }
    }
}
