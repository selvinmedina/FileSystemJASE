using Shell.Common;
using System.Text;
using static Shell.Common.GetValidData;
using static Shell.Common.ConsoleExtensions;

Console.Title = "JASE";

Console.WriteLine("Sistema de archivos genérico creado por Javier y Selvin");

bool thereareSystemSettings = false; // TODO: Asignar true cuando se detecte algo en bases de datos.

if (!thereareSystemSettings)
{
    int? minimun = 10000, maximun = null, number;
    bool isValid = true;
    do
    {
        if (!isValid) LogMinimoOMaximoInvalido(minimun, maximun);
        
        Console.WriteLine("¿De qué tamaño desea su sistema de archivos? (en KB)");

        number = GetValidNumber<int>(Console.ReadLine());
        isValid = EsValidoRango(minimun, maximun, number);
    } while (!isValid);

    int systemSizeKb = number ?? 0;

    minimun = 200;
    do
    {
        if (!isValid) LogMinimoOMaximoInvalido(minimun, maximun);
        Console.WriteLine("¿Cuál es el tamaño de cada bloque de información? (en KB)");

        number = GetValidNumber<int>(Console.ReadLine());
        isValid = EsValidoRango(minimun, maximun, number);
    } while (!isValid);

    int blockSize = number ?? 0;

    int blockQuantity = systemSizeKb / blockSize;

    string? adminUser = "", adminPassword = "", systemName;

    isValid = true;

    do
    {
        Console.WriteLine("Ingrese el nombre del Sistema de Archivos");
        systemName = Console.ReadLine();
        isValid = !string.IsNullOrEmpty(systemName) || systemName?.Length < 3;

        if (!isValid) Console.WriteLine("El nombre del sistema de archivos debe ser mayor a 3 dígitos.");
    } while (!isValid);

    isValid = true;

    do
    {
        Console.WriteLine("Ingrese el nombre del usuario administrador del Sistema de Archivos");
        adminUser = Console.ReadLine();
        isValid = !string.IsNullOrEmpty(adminUser) || adminUser?.Length < 4;

        if (!isValid) Console.WriteLine("El usuario debe ser mayor a 4 dígitos.");
    } while (!isValid);

    isValid = true;

    do
    {
        Console.WriteLine("Ingrese la contraseña del usuario administrador del Sistema de Archivos");
        adminPassword = GetPassword();
        isValid = !string.IsNullOrEmpty(adminPassword) || adminPassword?.Length < 6;

        if (!isValid) Console.WriteLine("La contraseña debe ser mayor a 6 dígitos.");
    } while (!isValid);

    Console.WriteLine($"Cantidad de bloques: {blockQuantity}");

    //TODO: Continuar con InodeTable, validar como hacerlo desde la capa de aplicacion
}