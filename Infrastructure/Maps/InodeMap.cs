using Domain.Entities.Disk;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Maps
{
    public class InodeMap : IEntityTypeConfiguration<Inode>
    {
        public void Configure(EntityTypeBuilder<Inode> builder)
        {
            builder.ToTable("Inode");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.UniqueId).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Size).IsRequired();
            builder.Property(x => x.ParentDirectoryId).IsRequired();
            builder.Property(x => x.FileType).IsRequired();
            builder.Property(x => x.CreationTime).IsRequired();
            builder.Property(x => x.Size).IsRequired();
            builder.Property(x => x.IsDirectory).IsRequired();
            builder.Property(x => x.NumberOfBlocksAssigned).IsRequired();
        }
    }
}
