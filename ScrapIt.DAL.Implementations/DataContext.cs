using Microsoft.EntityFrameworkCore;
using ScrapIt.DAL.Contracts.Entities;
using ScrapIt.DAL.Implementations.Configs;
using System.Linq;
using System.Threading.Tasks;

namespace ScrapIt.DAL.Implementations
{
    public class DataContext: DbContext
    {
        public DbSet<CarEntity> Cars { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }

        public DataContext(DbContextOptions<DataContext> opt) : base(opt)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaskConfig());
            modelBuilder.ApplyConfiguration(new CarConfig());
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public DbSet<T> DbSet<T>() where T : class
        {
            return Set<T>();
        }

        public new IQueryable<T> Query<T>() where T : class
        {
            return Set<T>();
        }
    }
}
