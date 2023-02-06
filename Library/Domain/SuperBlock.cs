namespace Library.Domain
{
    public struct SuperBlock
    {
        public int MagicNumber { get; set; }
        public int Blocks { get; set; }
        public int InodeBlocks { get; set; }
        public int Inodes { get; set; }
    }
}
