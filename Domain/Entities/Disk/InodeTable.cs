namespace Domain.Entities.Disk
{
    public class InodeTable
    {
        public int Id { get; set; }
        public Guid UniqueNodeId { get; set; }
        public bool Status { get; set; }
    }
}
