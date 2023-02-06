namespace Library.Domain
{
    public struct Block
    {
        public SuperBlock Super { get; set; }
        public Inode INodes { get; set; } // TODO: Inodes[INODES_PER_BLOCK]
        public int Pointers { get; set; } // Pointers[POINTERS_PER_BLOCK]
    }
}
