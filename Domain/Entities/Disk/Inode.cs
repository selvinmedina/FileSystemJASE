using Domain.Enums;

namespace Domain.Entities.Disk
{
    public class Inode
    {
        public Guid UniqueId { get; set; }
        public string Name { get; set; } = null!;
        public int ParentDirectoryId { get; set; }
        public FileType FileType { get; set; }
        public DateTime CreationTime { get; set; }
        public int Size { get; set; }
        public bool IsDirectory { get; set; }
        public int NumberOfBlocksAssigned { get; set; }

        // Root directory Constructor
        Inode()
        {
            UniqueId = Guid.NewGuid();
        }

        // Root directory constructor
        public Inode(string name) : this()
        {
            ParentDirectoryId = -1;
            SetDirectory(name);
        }

        // Directories constructor
        public Inode(string name, int parentDirectoryId, int size) : this()
        {
            SetDirectory(name);
            ParentDirectoryId = parentDirectoryId;
        }


        // Files constructor
        public Inode(string name, int parentDirectoryId, int size, int numberOfBlocks) : this()
        {
            Name = name;
            CreationTime = DateTime.Now;
            ParentDirectoryId = parentDirectoryId;
            Size = size;
            NumberOfBlocksAssigned = numberOfBlocks;
            FileType = FileType.File;
            IsDirectory = false;
        }

        private void SetDirectory(string name)
        {
            Name = name;
            IsDirectory = true;
            FileType = FileType.Direcotry;
            NumberOfBlocksAssigned = 1;
            Size = 0;
            CreationTime = DateTime.Now;
        }
    }
}
