// using MP3;
string rutaCancion = @"C:\Users\mayri\Downloads\The Mystery Artist - Bones (part of flesh).mp3"; 
Id3v1Tag mp3 = new(rutaCancion);
Console.WriteLine("------------------\n");
Console.WriteLine(mp3);
Console.WriteLine("\n");
Console.WriteLine(mp3.EnRenglones());
Console.WriteLine("\n------------------");
