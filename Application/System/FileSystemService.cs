using Domain.Entities.Disk;
using Domain.Entities.System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using SelvinMedina.EntityFramework.Infrastructure.Core.UnitOfWork;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Application.System
{
    public class FileSystemService
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
            var jsonWriteOptions = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            jsonWriteOptions.Converters.Add(new JsonStringEnumConverter());

            var newSuperBlock = JsonSerializer.Serialize(superBlock, jsonWriteOptions);
            var newInodeTable = JsonSerializer.Serialize(inodeTable, jsonWriteOptions);
            List<Inode> inodes = new List<Inode>();
            inodes.Add(inode);
            var newInodes = JsonSerializer.Serialize(inodes, jsonWriteOptions);

            uofW.Repository<SystemSuperBlock>().Add(superBlock);
            Save(newInodes, "inodes.json");
            Save(newInodeTable, "inodeTable.json");

            await uofW.SaveAsync();
        }

        private static void Save(string text, string fileName)
        {
            var appSettingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            File.WriteAllText(appSettingsPath, text);
        }

        public SystemSuperBlock GetSuperBlock() {
            var superblockConfig = uofW.Repository<SystemSuperBlock>().AsQueryable().FirstOrDefault();

            return superblockConfig!;
        }

        public InodeTable GetInodeTable()
        {
            var inodeTable = new ConfigurationBuilder()
                   .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                   .AddJsonFile("inodeTable.json")
                   .Build()
                   .Get<InodeTable>();

            return inodeTable!;
        }

        public List<Inode> GetInodes()
        {
            var inodes = new ConfigurationBuilder()
                   .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                   .AddJsonFile("inodes.json")
                   .Build()
                   .Get<List<Inode>>();

            return inodes!;
        }
    }
}
