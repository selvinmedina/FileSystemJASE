using Application.System;
using Domain.Entities.Disk;
using Domain.Entities.System;
using static Shell.Common.ConsoleExtensions;
using static Shell.Common.GetValidData;

Console.Title = "JASE";

string dbName = "";
Console.WriteLine("Sistemas de archivos genéricos creado por Javier y Selvin");

List<string> databases = new List<string>();
foreach (var item in Directory.GetFiles(Directory.GetCurrentDirectory()))
{
    if (item.EndsWith(".db"))
    {
        databases.Add(item.Split("\\")[^1]);
    }
}

if (databases.Count > 0)
{
    Console.WriteLine("Seleccione un sistema de archivos configurado:");
    Console.WriteLine($"0. Nuevo");
    for (int i = 0; i < databases.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {databases[i]}");
    }

    int option = GetOption(databases);

    if (option > 0)
    {
        dbName = databases[option - 1];
    }
}


bool configureNewFileSystem = string.IsNullOrEmpty(dbName);

if (configureNewFileSystem)
{
    int systemSizeKb = GetSystemSize(),
        blockSize = GetBlockSize();

    string systemName = GetSystemName(),
        adminUser = GetAdminUser(),
        adminPassword = GetAdminPassword();

    Inode root = new Inode("C:/");
    InodeTable inodeTable = new InodeTable();

    SystemSuperBlock superBlock = new SystemSuperBlock();

    superBlock.Create(systemSizeKb,
        blockSize,
        adminUser!,
        adminPassword!,
        systemName!
        );

    dbName = $"{systemName!.Replace(" ", "")}.db";
    FileSystemService fileSystemService = await GetFileSystemService(dbName);

    await fileSystemService.SaveConfig(superBlock, inodeTable, root);

    var test1 = await fileSystemService.GetSuperBlock();
    var test2 = fileSystemService.GetInodeTable();
    var test3 = fileSystemService.GetInodes();
}
else
{
    FileSystemService fileSystemService = await GetFileSystemService(dbName);

    var test1 = await fileSystemService.GetSuperBlock();
    var test2 = await fileSystemService.GetInodeTable();
    var test3 = await fileSystemService.GetInodes();
}

#region Get Values
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

static int GetOption(List<string> databases)
{
    bool isValid = true;
    int option;
    do
    {
        option = GetValidNumber<int>(Console.ReadLine());

        if (!isValid) LogMinimoOMaximoInvalido(0, maximun: databases.Count);
        isValid = EsValidoRango(0, maximun: databases.Count, option);
    } while (!isValid);

    return option;
}
#endregion