// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var respuesta = Console.ReadLine();

if (respuesta.Contains("cd"))
{
    string directorioDestino = respuesta.Split(" ")[1];
    Console.WriteLine($"Moviendose al directorio: '{directorioDestino}'");
}

Console.WriteLine(respuesta);


