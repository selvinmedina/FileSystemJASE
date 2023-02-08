using Domain.Entities.Disk;

namespace Application.System
{
    public class FileSystemService
    {
        const int InodosPorBloque = 128;
        int numeroInodos;

        public FileSystemService()
        {

        }

        bool LoadInode(int inumber, Inode inode)
        {
            throw new NotImplementedException();
        }

        bool SaveInode(int inumber, Inode inode)
        {
            throw new NotImplementedException();
        }
    }
}
