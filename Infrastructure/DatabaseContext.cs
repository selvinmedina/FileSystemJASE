using Infrastructure.Maps;
using Microsoft.EntityFrameworkCore;
using SelvinMedina.EntityFramework.Infrastructure.Core.Extensions;
using System.Reflection;

namespace Infrastructure
{
    public class DatabaseContext : DbContext
    {
        private readonly string _databseName;
        public DatabaseContext(string databaseName = "FileSystemJASE")
        {
            _databseName = databaseName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString: $"FileName = {_databseName}", sqliteOptionsAction: op =>
            {
                op.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new SystemSuperBlockMap());
            modelBuilder.AddConfiguration(new InodeTableMap());
            modelBuilder.AddConfiguration(new InodeMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
