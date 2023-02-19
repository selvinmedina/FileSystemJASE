using Domain.Entities.Disk;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Maps
{
    public class InodeTableMap : IEntityTypeConfiguration<InodeTable>
    {
        public void Configure(EntityTypeBuilder<InodeTable> builder)
        {
            builder.ToTable("InodeTable");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.UniqueNodeId).IsRequired();
        }
    }
}
