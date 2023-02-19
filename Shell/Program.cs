using Application.System;
using Domain.Entities.Disk;
using Domain.Entities.System;
using Shell;
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

    int option = GetDatabaseOption(databases);

    if (option > 0)
    {
        dbName = databases[option - 1];
    }
}


bool configureNewFileSystem = string.IsNullOrEmpty(dbName);

if (configureNewFileSystem)
{
    dbName = await FileSystemNewConfig.New();
}

FileSystemService fileSystemService = await FileSystemService.GetFileSystemService(dbName);

var superBlock = await fileSystemService.GetSuperBlock();

Console.WriteLine($"Bienvenido a {superBlock.FileSystemName}");
Console.WriteLine("-------------------------------------------------");
Console.WriteLine("Opciones disponibles: ");
Console.WriteLine("stat: Consulta  estadísticas sobre el estado del sistema de archivos.");
Console.WriteLine("pwd: Indicar en que directorio se encuentra actualmente.");
Console.WriteLine("cd: Moverse a través de los directorios");
Console.WriteLine("touch: Crear archivos en el directorio actual.");
Console.WriteLine("mkdir: Crear directorios en el directorio actual.");
Console.WriteLine("mv: Mover archivos a un directorio ya existente.");
Console.WriteLine("cp: Copiar archivos a un directorio ya existente.");
Console.WriteLine("df -H: Ver espacio disponible en disco (basado en la cantidad de bloques libres).");
Console.WriteLine("rm: Borrar un archivo.");
Console.WriteLine("rm -d: Borrar un directorio (considerar que hacer en el caso de contener mas archivos o  subdirectorios).");
Console.WriteLine("ls: Listar archivos y directorios dentro de un directorio.");
Console.WriteLine("ls -al: Listar información detallada de un archivo / directorio (nombre, numero de inodo,  nombre de carpeta padre, fecha de creación, tipo (archivo o directorio).");
Console.WriteLine("mkfs: Formatear el disco.");
Console.WriteLine("exit: Salir.");
Console.WriteLine("-------------------------------------------------");

while (true)
{
    string opcion = Console.ReadLine()!;
    var split = opcion.Split(" ");
    opcion = split[0];
    string argumentos = "";

    if (split.Length > 1)
    {
        for (int i = 1; i < split.Length; i++)
        {
            argumentos += split[i];
        }
    }

    switch (opcion)
    {
        case "stat":
            Console.WriteLine($"Fecha de creación: {superBlock.CreationDate}");
            Console.WriteLine($"Nombre del sistema de archivo: {superBlock.FileSystemName}");
            Console.WriteLine($"Cantidad de directorios existenets: {superBlock.NumberOfExistingDirectories}");
            Console.WriteLine($"Cantidad de archivos existenets: {superBlock.NumberOfExistingFiles}");
            Console.WriteLine($"Usuario admin: {superBlock.UserName}");
            Console.WriteLine($"Contraseña admin: {superBlock.Password}");
            Console.WriteLine($"Espacio utilizado: {superBlock.UsedSpace}");
            Console.WriteLine($"Espacio disponible: {superBlock.AvailableSpace}");
            Console.WriteLine($"Cantidad de bloques utilizados: {superBlock.NumberOfBlocksUsed}");
            Console.WriteLine($"Cantidad de bloques disponibles: {superBlock.NumberOfBlocksAvailable}");
            break;
        case "pwd":
            Console.WriteLine($"Directorio actual: /");
            break;
        case "cd":
            Console.WriteLine($"Moviendose a {argumentos}");
            break;
        case "touch":
            Console.WriteLine($"Creando archivos {argumentos} en el directorio actual.");
            break;
        case "mkdir":
            Console.WriteLine($"Creando directorio {argumentos} en el directorio actual.");
            break;
        case "mv":
            Console.WriteLine($"Moviendo {argumentos} a un directorio ya existente.");
            break;
        case "cp":
            Console.WriteLine($"Copiando {argumentos} a un directorio ya existente.");
            break;
        case "df -H":
            Console.WriteLine($"Espacio disponible: {superBlock.AvailableSpace}");
            break;
        case "rm":
            Console.WriteLine($"Borrando archivo {argumentos}...");
            break;
        case "rm -d":
            Console.WriteLine($"Borrando directorio: {argumentos}");
            break;
        case "ls":
            Console.WriteLine($"Listando archivos del directorio actual...");
            break;
        case "ls -al":
            Console.WriteLine($"Listando información detallada de un archivo / directorio (nombre, numero de inodo,  nombre de carpeta padre, fecha de creación, tipo (archivo o directorio) del directorio actual...");
            break;
        case "mkfs":
            Console.WriteLine($"Formateando disco {superBlock.FileSystemName}...");
            break;
        case "exit":
            Console.WriteLine("Saliendo...");
            return;
        default:
            Console.WriteLine("Comando no reconocido");
            break;
    }
}

#region Get Values
static int GetDatabaseOption(List<string> databases)
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