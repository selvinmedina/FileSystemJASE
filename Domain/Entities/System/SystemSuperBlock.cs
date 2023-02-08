using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.System
{
    public class SystemSuperBlock
    {
        public float SystemSize { get; set; }
        public float BlockSize { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!; // TODO: Encriptar esto
        public string FileSystemName { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public int NumberOfExistingDirectories { get; set; }
        public int NumberOfExistingFiles { get; set; }
        public float UsedSpace { get; set; }
        public float AvailableSpace { get; set; }
        public int NumberOfBlocksUsed { get; set; }
        public int NumberOfBlocksAvailable { get; set; }
        public int TotalBlocks { get; set; }

        public void Create(float systemSize,
                                float blockSize,
                                string userName,
                                string password,
                                string fileSystemName,
                                int numberOfExistingDirectories,
                                int numberOfExistingFiles,
                                int numberOfBlocksUsed,
                                int numberOfBlocksAvailable)
        {
            SystemSize = systemSize;
            BlockSize = blockSize;
            UserName = userName;
            Password = password;
            FileSystemName = fileSystemName;
            CreationDate = DateTime.Now;
            NumberOfExistingDirectories = 1; // inicialmente es el directorio raiz 
            NumberOfExistingFiles = 0;

            // Hacer calculo para el espacio utilizado
            //UsedSpace = usedSpace;
            //AvailableSpace = availableSpace;

            NumberOfBlocksUsed = 0;



            NumberOfBlocksAvailable = TotalBlocks - 3; // 1 Superbloque, 1 tabla inodos, 1 inodos en si
        }
    }
}
