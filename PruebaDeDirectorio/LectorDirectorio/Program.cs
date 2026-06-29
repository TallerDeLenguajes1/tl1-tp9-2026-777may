
// comprobar si existe D:\Privado\Facultad\2026\TallerDeLenguajes1\TP9\tl1-tp9-2026-777may\PruebaDeDirectorio? crear sino
using System.Text;

string direccion = """D:\Privado\Facultad\2026\TallerDeLenguajes1\TP9\tl1-tp9-2026-777may\PruebaDeDirectorio""";
if (!Directory.Exists(direccion))
{
    Directory.CreateDirectory(direccion); 
}
// crear tambien un archivo en él 
//* File.Create(Path.Combine(direccion, "archivo.csv"));   no se usa porque lo deja abierto  y sw puede crearlo
// trabajar con CSV, leerlo, sacar datos nombre, apellido y edad
using (var sw = new StreamWriter(Path.Combine(direccion, "archivo.csv")))
{
    sw.WriteLine("Nombre; Apellido; DNI; Edad; Direccion");
    sw.WriteLine("Mengano; Juarez; 304050; 19; PadreMonti 93");
    sw.WriteLine("Luciano; Varella; 502070; 27; Centro");
    sw.WriteLine("May; G; 902010; 29; Yb 100");
    sw.WriteLine("Mica; Perez; 402060; 40; ruta 56");
}
//! En el pdf hay una opcion File.WriteAllLines(ruta, lineas);      estos no son stream
/**
// La mejor forma de escribirlo
// string[] headers = ["Nombre", "Apellido", "DNI", "Edad", "Direccion"];
// string[][] datos =
// [
//     ["Mengano", "Juarez", "304050", "19", "PadreMonti 93"],
//     ["Luciano", "Varella", "502070", "27", "Centro"],
//     ["May", "G", "902010", "29", "Yb 100"],
//     ["Mica", "Perez", "402060", "40", "ruta 56"]
// ];

// using (var sw = new StreamWriter(Path.Combine(direccion, "archivo.csv")))
// {
//     sw.WriteLine(string.Join("; ", headers));
//     foreach (var fila in datos)
//         sw.WriteLine(string.Join("; ", fila));
// }
*/

using (var sw = new StreamReader(Path.Combine(direccion, "archivo.csv")))
{
    List<string> texto = [];
    //! String[] leidas = File.ReadAllLines(ruta);            estos no son stream
    string?leerlo;
    // sw.ReadLine();   esto leeería una linea y la "descartaría" a la primera porque queda fuera del while
    while ((leerlo = sw.ReadLine())!= null)
    {
        texto.Add(leerlo);
    } 

    Console.WriteLine($"======================================================================================================");
    foreach (var txt in texto.Skip(1))  //skip evita el primer renglon del encabezado de columnas
    {
        string[] palabras = txt.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        Console.WriteLine($"\nNombre: {palabras[0]}, Apellido: {palabras[1]}, Edad: {palabras[3]} años.\n");
    }
    Console.WriteLine($"======================================================================================================");
    /** for (int i = 1; i <= texto.Count; i++)
    // {

    //     Console.WriteLine($"\n{texto[i]}\n");
        
    // }
    */
}


string rutaCancion = @"C:\Users\mayri\Downloads\The Mystery Artist - Bones (part of flesh).mp3";
using (var fs = new FileStream(rutaCancion, FileMode.Open))
{
    byte[] buffer = new byte[128];
    fs.Seek(-128, SeekOrigin.End); //La etiqueta ID3v1 está en los últimos 128 bytes del archivo
    int leidos = fs.Read(buffer, 0, 128);

    string texto = Encoding.UTF8.GetString(buffer, 0, leidos);

    Console.WriteLine(texto);

    //*======================Esto lo hiso la IA=================================
    // Los primeros 3 bytes son "TAG" si tiene etiqueta ID3v1
    string tag = Encoding.ASCII.GetString(buffer, 0, 3);
    if (tag == "TAG")
    {
        string titulo  = Encoding.ASCII.GetString(buffer, 3, 30).TrimEnd('\0');
        string artista = Encoding.ASCII.GetString(buffer, 33, 30).TrimEnd('\0');
        string album   = Encoding.ASCII.GetString(buffer, 63, 30).TrimEnd('\0');
        Console.WriteLine($"Título: {titulo}");
        Console.WriteLine($"Artista: {artista}");
        Console.WriteLine($"Album: {album}");
    }
    else
    {
        Console.WriteLine("No tiene etiqueta ID3v1");
    }
    //*==========================================================================
    // fs.Seek(0, SeekOrigin.Begin);
}