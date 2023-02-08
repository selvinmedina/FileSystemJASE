using Domain;

namespace Application.Disk
{
    public class DiskService
    {
        public Disk Disk { get; set; }
        public DiskService()
        {
            Disk = new Disk();
        }

        ~DiskService()
        {

        }

        public void Open(string pathToDiskImage, int numberOfBlockInDiskImage)
        {
        }

        int GetSize()
        {
            return Disk.Blocks;
        }

        bool IsMounted()
        {
            return Disk.Mounts > 0;
        }

        void Mount()
        {
            Disk.Mounts++;
        }

        void UnMount()
        {
            if (Disk.Mounts > 0) Disk.Mounts--;
        }

        void Read(int blockNumber, string data)
        {

        }

        void Write(int blockNumber, string data)
        {

        }
    }
}
