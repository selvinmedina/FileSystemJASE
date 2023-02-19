using Shell.Common;
using System.Text;
using static Shell.Common.GetValidData;
using static Shell.Common.ConsoleExtensions;
using Domain.Entities.Disk;
using Domain.Entities.System;
using Application.System;

Console.Title = "JASE";

string dbName = "";
Console.WriteLine("Sistema de archivos genérico creado por Javier y Selvin");

bool thereareSystemSettings = false; // TODO: Asignar true cuando se detecte algo en bases de datos.

if (!thereareSystemSettings)
{
    int systemSizeKb = GetSystemSize(),
        blockSize = GetBlockSize();

    string systemName = GetSystemName(),
        adminUser = GetAdminUser(),
        adminPassword = GetAdminPassword();

    Inode root = new Inode("C:/");
    InodeTable inodeTable = new InodeTable();
    inodeTable.AddInode(root.UniqueId, available: true);

    SystemSuperBlock superBlock = new SystemSuperBlock();

    superBlock.Create(systemSizeKb,
        blockSize,
        adminUser!,
        adminPassword!,
        systemName!
        );

    dbName = systemName!.Replace(" ", "");
    FileSystemService fileSystemService = await GetFileSystemService(dbName);

    await fileSystemService.SaveConfig(superBlock, inodeTable, root);

    var sb = fileSystemService.GetSuperBlock();


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
    string response = "";
    bool isValid = true;
    do
    {
        Console.WriteLine("Ingrese el nombre del Sistema de Archivos");
        response = Console.ReadLine();
        isValid = !string.IsNullOrEmpty(response) || response?.Length < 3;

        if (!isValid) Console.WriteLine("El nombre del sistema de archivos debe ser mayor a 3 dígitos.");
    } while (!isValid);
    return response!;
}

static string GetAdminUser()
{
    string? response = "";
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
    string? response = "";
    do
    {
        Console.WriteLine("Ingrese la contraseña del usuario administrador del Sistema de Archivos");
        response = GetPassword();
        isValid = !string.IsNullOrEmpty(response) || response?.Length < 6;

        if (!isValid) Console.WriteLine("La contraseña debe ser mayor a 6 dígitos.");
    } while (!isValid);

    return response!;
}

static async Task<FileSystemService> GetFileSystemService(string dbName)
{
    FileSystemService fileSystemService = new FileSystemService();
    await fileSystemService.ConfigureDataBase(dbName);
    return fileSystemService;
}