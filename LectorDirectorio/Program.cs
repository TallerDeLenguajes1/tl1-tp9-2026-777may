// string direccion1 = """D:\Privado\Facultad\2026\TallerDeLenguajes1\TP9\tl1-tp9-2026-777may\PruebaDeDirectorio""";
using Microsoft.VisualBasic;
// //TODO INI tonteando con tiempo
//     using System.Diagnostics;
//     Stopwatch sw1 = new();
//     sw1.Start(); //TODO Watch1
//     Stopwatch sw2 = new();
// //TODO FIN tonteando con tiempo

string pathArchivo;
MenuPP(out string pathPc);
ListarPath(pathPc);
pathArchivo = crearCSV();
// ListarPath(pathPc);
//* BorrarArchivo(pathArchivo);
// ListarPath(pathPc);


// //TODO INI tonteando con tiempo
//     sw2.Stop();
//     Console.WriteLine($"Tardó: {sw2.ElapsedMilliseconds} ms");
//     sw1.Stop();
//     Console.WriteLine($"Tardó: {sw1.ElapsedMilliseconds} ms");
// //TODO FIN tonteando con tiempo


void MenuPP(out string path) //Solicitar path, validarlo e informar si no existe.
{
    string ? texto;
    bool esValido;
    DecoradorTexto();
    do
    {
        Console.WriteLine("\nIngrese el path a analizar:\n");
        texto = Console.ReadLine();
        esValido = DirectorioExiste(texto);
        Advertir(esValido, "\nEl path ingresado no existe.\n");
    } while (!esValido);
    path = texto!;
    // sw2.Start(); //TODO Watch2
    EspaciadorTexto();
}
bool DirectorioExiste(string? directorio)
{
    return !string.IsNullOrWhiteSpace(directorio) && Directory.Exists(directorio) ; 
}
void Advertir(bool condicion, string mensaje){
    if(!condicion)
        Console.WriteLine($"⚠ {mensaje}");
}
void DecoradorTexto(string texto = " ")
{
    Console.WriteLine("=========================================");
    if (!string.IsNullOrWhiteSpace(texto)) 
        Console.WriteLine(texto); 
}
void EspaciadorTexto(){
    Console.WriteLine();
}
FileInfo[] ObtenerInfoArchivos(string path)
{
    // return Directory.GetFiles(path).Select(archivo => new FileInfo(archivo)).ToArray();
    return[.. Directory.GetFiles(path)
            .Select(archivo => new FileInfo(archivo))
            .ToArray()];
}
void ListarPath(string path)
{
    //Todas las carpetas que se encuentran en ese path. Solo el nombre de carpeta
    string[] directorios = Directory.GetDirectories(path);
    DecoradorTexto($"Las carpetas en {path} son:\n");
    foreach (var directorio in directorios)
    {
        // Console.WriteLine(directorio);          no es la correcta, devuelve direccion1\LectorDirectorio
        Console.WriteLine(Path.GetFileName(directorio)); // devuelve LectorDirectorio
    } 
    EspaciadorTexto();
    //Toodos los archivos que se encuentran directamente en esa carpeta. Junto a cada nombre de archivo, se deberá mostrar su tamaño en kilobytes (KB)
    
    var infoArchivos = ObtenerInfoArchivos(path); // aqui era string[] archivos = Directory.GetFiles(path)
    DecoradorTexto($"Los archivos en {path} son:\n");
    foreach (var info in infoArchivos)
    {
        //* FileInfo info = new(archivo); // esto era cuando era string[] antes de crear ObtenerArchivos
        // Console.WriteLine($"{info.Name} - {info.Length} bytes"); // devuelve LectorDirectorio
        Console.WriteLine($"{info.Name} - {info.Length / 1024.0:N4} KB"); // devuelve LectorDirectorio
        // info.Name          // nombre con extensión
        // info.FullName      // ruta completa
        // info.Extension     // ".csv", ".mp3", etc.
        // info.Length        // tamaño en bytes
        // info.CreationTime  // fecha de creación
        // info.LastWriteTime // última modificación
        // info.LastAccessTime // último acceso
        // info.IsReadOnly    // si es solo lectura
    }
    EspaciadorTexto();

}
string crearCSV(){
    string rutaRelativa = Path.GetRelativePath(Path.GetFullPath("."), pathPc);
    // File.Create("reporte_archivos.csv"); 
    var infoArchivos = ObtenerInfoArchivos(rutaRelativa); // obtengo un arreglo que contiene FileInfo de cada archivo encontrado
    string rutaCSV = Path.Combine(rutaRelativa, "reporte_archivos.csv");

    using (var sw = new StreamWriter(rutaCSV))
    {
        sw.WriteLine("Nombre; Tamaño (KB); Última Modificación");
        foreach (var info in infoArchivos)
        { 
            sw.WriteLine($"{info.Name}; {info.Length / 1024.0:N4}; {info.LastWriteTime}");
        }
    }
    //* Si lo hiciera con File.WriteAllLines::
    /**
    string[] lineas = ["Nombre; Tamaño; Última Modificación", 
        .. infoArchivos.Select(info => $"{info.Name}; {info.Length / 1024.0:N2}; {info.LastWriteTime}")];
    File.WriteAllLines(rutaCSV, lineas);
    */

    return rutaCSV;
}

void BorrarArchivo(string path)
{
    bool condicion = File.Exists(path);
    if (condicion)
    {
        File.Delete(path);
        Console.WriteLine("El archivo ha sido borrado");
    }
    Advertir(condicion,"El archivo no existe");
}


