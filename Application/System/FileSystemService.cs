using Domain.Entities.Disk;
using Domain.Entities.System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SelvinMedina.EntityFramework.Infrastructure.Core.UnitOfWork;

namespace Application.System
{
    public partial class FileSystemService
    {
        private IUnitOfWork uofW = null!;
        public FileSystemService()
        {
        }

        public async Task ConfigureDataBase(string databaseName)
        {
            DatabaseContext databaseContext = new DatabaseContext(databaseName);
            await databaseContext.Database.EnsureCreatedAsync();
            UnitOfWork unitOfWork = new UnitOfWork(databaseContext);
            uofW = unitOfWork;
        }

        public async Task SaveConfig(SystemSuperBlock superBlock, InodeTable inodeTable, Inode inode)
        {
            try
            {
                uofW.Repository<SystemSuperBlock>().Add(superBlock);
                uofW.Repository<Inode>().Add(inode);
                await AddToInodeTable(inode.UniqueId, available: true, save: false);

                await uofW.SaveAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hubo un error al guardar la configuracion: {ex.Message}, {ex.StackTrace}");
            }
        }

        private static void Save(string text, string fileName)
        {
            var appSettingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            File.WriteAllText(appSettingsPath, text);
        }

        public async Task<SystemSuperBlock> GetSuperBlock()
        {
            var superblockConfig = await uofW.Repository<SystemSuperBlock>()
                .AsQueryable()
                .FirstOrDefaultAsync();

            return superblockConfig!;
        }

        public async Task<List<InodeTable>> GetInodeTable()
        {
            var inodeTable = await uofW.Repository<InodeTable>()
                .AsQueryable()
                .ToListAsync();

            return inodeTable!;
        }

        public async Task<List<Inode>> GetInodes()
        {
            var inodes = await uofW.Repository<Inode>()
                .AsQueryable()
                .ToListAsync();

            return inodes!;
        }

        public async Task AddToInodeTable(Guid inodeUniqueId, bool available, bool save = true)
        {
            uofW.Repository<InodeTable>().Add(new InodeTable { Status = available, UniqueNodeId = inodeUniqueId });

            if (save) await uofW.SaveAsync();
        }

        public async Task<bool> GetInodeStatus(Guid inodeUniqueId)
        {
            InodeTable? inodeTableFound = await GetInodeTable(inodeUniqueId);

            if (inodeTableFound is null)
            {
                throw new Exception("Not found inodeTable");
            }

            var response = inodeTableFound.Status;

            return response;
        }

        private async Task<InodeTable?> GetInodeTable(Guid inodeUniqueId)
        {
            return await uofW.Repository<InodeTable>().AsQueryable().FirstOrDefaultAsync(x => x.UniqueNodeId == inodeUniqueId);
        }

        public async Task SetInodeStatus(Guid inodeUniqueId, bool status, bool save = true)
        {
            var inodeTableFound = await GetInodeTable(inodeUniqueId);
            if (inodeTableFound is null)
            {
                throw new Exception("Not found inodeTable");
            }

            inodeTableFound.Status = status;

            if (save) await uofW.SaveAsync();
        }

        public static async Task<FileSystemService> GetFileSystemService(string dbName)
        {
            FileSystemService fileSystemService = new FileSystemService();
            await fileSystemService.ConfigureDataBase(dbName);
            return fileSystemService;
        }
    }
}
