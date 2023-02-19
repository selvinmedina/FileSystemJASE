using Infrastructure.Maps;
using Microsoft.EntityFrameworkCore;
using SelvinMedina.EntityFramework.Infrastructure.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            optionsBuilder.UseSqlite(connectionString: $"FileName = {_databseName}.db", sqliteOptionsAction: op =>
            {
                op.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new SystemSuperBlockMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
