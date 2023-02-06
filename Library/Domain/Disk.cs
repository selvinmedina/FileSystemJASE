namespace Library.Domain
{
    public class Disk
    {
        public int FileDescriptor { get; set; }
        public int Blocks { get; set; }
        public int Reads { get; set; }
        public int Writes { get; set; }
        public int Mounts { get; set; }

        public int BlockSize { get; set; }
    }
}
