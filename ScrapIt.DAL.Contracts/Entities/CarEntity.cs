
using System;

namespace ScrapIt.DAL.Contracts.Entities
{
    public class CarEntity: BaseEntity
    {
        public long TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; }
        public DateTime? PublishDate { get; set; }
        public string Url { get; set; }

        public TaskEntity TaskEntity { get; set; }
    }
}
