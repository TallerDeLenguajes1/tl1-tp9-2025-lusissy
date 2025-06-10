// See https://aka.ms/new-console-template for more information


Console.WriteLine("Ingrese el path del directorio que desea analizar: ");
string? pathIngresado;
string[] directorio,archivos;

do
{
    pathIngresado=Console.ReadLine();
    if (Directory.Exists(pathIngresado))
    {
        break;
    }else
    {
        Console.WriteLine("El path ingresado es invalido. intente Nuevamente:");
    }

} while (!Directory.Exists(pathIngresado));

Console.WriteLine("Carpetas encontradas:");
directorio=Directory.GetDirectories(pathIngresado);
foreach (var carpeta in directorio)
{
    archivos=Directory.GetFiles(carpeta);
    Console.WriteLine($"Archivos de la carpeta{carpeta}");
    foreach (var archivo in archivos)
    {
        var aux= new FileInfo(archivo);
        long tamanio= aux.Length/1024;
        System.Console.WriteLine($"---Archivo: {archivo} ---Tamaño:{tamanio}");
    }
}
