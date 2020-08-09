using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapIt.DAL.Contracts.Entities
{
    public class TaskEntity:BaseEntity
    {
        public string Url { get; set; }
        public string Name { get; set; }

        public ICollection<CarEntity> Cars { get; set; }
    }
}
