using System;

namespace ScrapIt.Domain.Contracts.Models
{
    public class CarModel: RowBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string PublishDate { get; set; }
        public string Link { get; set; }
    }
}
