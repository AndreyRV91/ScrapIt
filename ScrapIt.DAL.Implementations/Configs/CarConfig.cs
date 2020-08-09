using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapIt.DAL.Contracts.Entities;

namespace ScrapIt.DAL.Implementations.Configs
{
    public class CarConfig : IEntityTypeConfiguration<CarEntity>
    {
        public void Configure(EntityTypeBuilder<CarEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Name)
                .HasColumnName("Name")
                .HasMaxLength(255)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasColumnName("Description")
                .IsUnicode(false)
                .IsRequired(false);

            builder.Property(p => p.Price)
                .HasColumnName("Price")
                .IsRequired(false);

            builder.Property(p => p.PublishDate)
                .HasColumnName("PublishDate")
                .HasColumnType("date");

            builder.Property(p => p.Url)
                .HasColumnName("Url")
                .IsUnicode(false)
                .IsRequired(true);

            builder.HasOne(s => s.TaskEntity)
                .WithMany(g => g.Cars)
                .HasForeignKey(s => s.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.Price)
                .HasName("Index_Price");

            builder.HasIndex(p => p.Name)
                .HasName("Index_Name");
        }
    }
}
