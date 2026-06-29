
// comprobar si existe D:\Privado\Facultad\2026\TallerDeLenguajes1\TP9\tl1-tp9-2026-777may\PruebaDeDirectorio? crear sino
string direccion = """D:\Privado\Facultad\2026\TallerDeLenguajes1\TP9\tl1-tp9-2026-777may\PruebaDeDirectorio""";
if (!Directory.Exists(direccion))
{
    Directory.CreateDirectory(direccion); 
}
// crear tambien un archivo en él 
File.Create(Path.Combine(direccion, "archivo.csv"));
// trabajar con CSV, leerlo, sacar datos nombre, apellido y edad
using (var sw = new StreamWriter(Path.Combine(direccion, "archivo.csv")))
{
    sw.Write("Nombre; Apellido; DNI; Edad; Direccion");
    sw.Write("Mengano; Juarez; 304050; 19; PadreMonti 93");
    sw.Write("Luciano; Varella; 502070; 27; Centro");
    sw.Write("May; G; 902010; 29; Yb 100");
    sw.Write("Mica; Perez; 402060; 40; ruta 56");
}
using (var sw = new StreamReader(Path.Combine(direccion, "archivo.csv")))
{
    List<string> texto = [];
    string?leerlo;
    while ((leerlo = sw.ReadLine())!= null)
    {
        texto.Add(leerlo);
    } 

    foreach (var txt in texto)
    {
        Console.WriteLine($"\ntxt\n");
    }

    
}
