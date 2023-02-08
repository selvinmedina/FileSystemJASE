using static Shell.Common.GetValidData;

Console.WriteLine("Bienvenido al sistema de archivos JASE");

// TODO: Hacer algo para leer un archivo, y dependiendo si existe leerlo
//       Esto se hara desde bases de datos, en una coleccion de mongo

// consultar el tamaño del sistema de archivos

bool thereareSystemSettings = false; // TODO: Asignar true cuando se detecte algo en bases de datos.

if (!thereareSystemSettings)
{    
    Console.WriteLine("¿De qué tamaño desea su sistema de archivos? (en KB)");

    var systemSizeKb = GetValidNumber<float>(Console.ReadLine());

    //TODO: Continuar con InodeTable, validar como hacerlo desde la capa de aplicacion
}
