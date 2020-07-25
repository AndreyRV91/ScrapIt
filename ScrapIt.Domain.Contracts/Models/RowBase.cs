
namespace ScrapIt.Domain.Contracts.Models
{
    public abstract class RowBase
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
    }
}
