using Application.System;
using Domain.Entities.Disk;
using Domain.Entities.System;
using static Shell.Common.ConsoleExtensions;
using static Shell.Common.GetValidData;

namespace Shell
{
    public static class FileSystemNewConfig
    {
        public static async Task<string> New()
        {
            int systemSizeKb = GetSystemSize(), blockSize = GetBlockSize();

            string systemName = GetSystemName(), adminUser = GetAdminUser(), adminPassword = GetAdminPassword();

            Inode root = new Inode("C:/");
            InodeTable inodeTable = new InodeTable();

            SystemSuperBlock superBlock = new SystemSuperBlock();

            superBlock.Create(systemSizeKb,
                blockSize,
                adminUser!,
                adminPassword!,
                systemName!
                );

            string dbName = $"{systemName!.Replace(" ", "")}.db";
            FileSystemService fileSystemService = await FileSystemService.GetFileSystemService(dbName);

            await fileSystemService.SaveConfig(superBlock, inodeTable, root);

            return dbName;
        }

        static int GetSystemSize()
        {
            int minimun = 1000;

            int number;
            bool isValid = true;
            do
            {
                if (!isValid) LogMinimoOMaximoInvalido(minimun, maximun: null);

                Console.WriteLine("¿De qué tamaño desea su sistema de archivos? (en KB)");

                number = GetValidNumber<int>(Console.ReadLine());
                isValid = EsValidoRango(minimun, maximun: null, number);
            } while (!isValid);
            return number!;
        }

        static int GetBlockSize()
        {
            int minimun = 200;
            bool isValid = true;
            int number;
            do
            {
                if (!isValid) LogMinimoOMaximoInvalido(minimun, maximun: null);
                Console.WriteLine("¿Cuál es el tamaño de cada bloque de información? (en KB)");

                number = GetValidNumber<int>(Console.ReadLine());
                isValid = EsValidoRango(minimun, maximun: null, number);
            } while (!isValid);
            return number;
        }

        static string GetSystemName()
        {
            string response;
            bool isValid = true;
            do
            {
                Console.WriteLine("Ingrese el nombre del Sistema de Archivos");
                response = Console.ReadLine()!;
                isValid = !string.IsNullOrEmpty(response) || response?.Length < 3;

                if (!isValid) Console.WriteLine("El nombre del sistema de archivos debe ser mayor a 3 dígitos.");
            } while (!isValid);
            return response!;
        }

        static string GetAdminUser()
        {
            string? response;
            bool isValid = true;
            do
            {
                Console.WriteLine("Ingrese el nombre del usuario administrador del Sistema de Archivos");
                response = Console.ReadLine();
                isValid = !string.IsNullOrEmpty(response) || response?.Length < 4;

                if (!isValid) Console.WriteLine("El usuario debe ser mayor a 4 dígitos.");
            } while (!isValid);

            return response!;
        }

        static string GetAdminPassword()
        {
            bool isValid = true;
            string? response;
            do
            {
                Console.WriteLine("Ingrese la contraseña del usuario administrador del Sistema de Archivos");
                response = GetPassword();
                isValid = !string.IsNullOrEmpty(response) || response?.Length < 6;

                if (!isValid) Console.WriteLine("La contraseña debe ser mayor a 6 dígitos.");
            } while (!isValid);

            return response!;
        }
    }
}
