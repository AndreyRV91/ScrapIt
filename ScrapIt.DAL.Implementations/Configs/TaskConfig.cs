using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapIt.DAL.Contracts.Entities;

namespace ScrapIt.DAL.Implementations.Configs
{
    public class TaskConfig : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Url)
                .HasColumnName("Url")
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(p => p.Name)
                .HasColumnName("Name")
                .HasMaxLength(255)
                .IsUnicode(true)
                .IsRequired();

        }
    }
}
