using Domain.Entities.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Maps
{
    internal class SystemSuperBlockMap : IEntityTypeConfiguration<SystemSuperBlock>
    {
        public void Configure(EntityTypeBuilder<SystemSuperBlock> builder)
        {
            builder.ToTable("SystemSuperBlock");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.SystemSize).IsRequired();
            builder.Property(x => x.BlockSize).IsRequired();
            builder.Property(x => x.UserName).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.FileSystemName).IsRequired();
            builder.Property(x => x.CreationDate).IsRequired();
            builder.Property(x => x.NumberOfExistingDirectories).IsRequired();
            builder.Property(x => x.NumberOfExistingFiles).IsRequired();
            builder.Property(x => x.UsedSpace).IsRequired();
            builder.Property(x => x.AvailableSpace).IsRequired();
            builder.Property(x => x.NumberOfBlocksUsed).IsRequired();
            builder.Property(x => x.NumberOfBlocksAvailable).IsRequired();
            builder.Property(x => x.TotalBlocks).IsRequired();
        }
    }
}
