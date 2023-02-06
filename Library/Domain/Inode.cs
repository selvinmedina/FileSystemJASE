namespace Library.Domain
{
    public struct Inode
    {
        public bool Valid { get; set; }
        public int Size { get; set; }

        // TODO: Verificar como hacer el tipo de direct pointers
        public int DirectPointers { get; set; }

        // TODO: Verificar como hacer el tipo de indirect pointers
        public int Indirect { get; set; }
    }
}
