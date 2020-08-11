
using System;

namespace ScrapIt.Domain.Contracts.Models
{
    public class CarDto: RowBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; }
        public DateTime? PublishDate { get; set; }
        public string Url { get; set; }
    }
}
