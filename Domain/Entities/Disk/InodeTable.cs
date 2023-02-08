namespace Domain.Entities.Disk
{
    public class InodeTable
    {
        private Dictionary<Guid, bool> _inodes;

        public InodeTable()
        {
            _inodes = new Dictionary<Guid, bool>();
        }

        public void AddInode(Guid inodeUniqueId, bool available)
        {
            _inodes[inodeUniqueId] = available;
        }

        public bool GetInodeStatus(Guid inodeUniqueId)
        {
            return _inodes[inodeUniqueId];
        }

        public void SetInodeStatus(Guid inodeUniqueId, bool status)
        {
            _inodes[inodeUniqueId] = status;
        }
    }
}
