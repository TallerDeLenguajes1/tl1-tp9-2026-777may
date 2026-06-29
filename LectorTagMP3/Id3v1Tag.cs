// namespace MP3
// {
    using System.Text;
    class Id3v1Tag
    {
        public string Path {get;} //me gusta as el readonly
        public readonly string Titulo      = string.Empty;
        public readonly string Artista     = string.Empty;
        public readonly string Album       = string.Empty;
        public readonly string Anio        = string.Empty;
        public readonly string Comentario  = string.Empty;
        public readonly string Genero      = string.Empty;
        private byte[] ProcesarTag()
        {
            byte[] buffer = new byte[128];
            using (var fs = new FileStream(Path, FileMode.Open))
            {
                fs.Seek(-128, SeekOrigin.End);
                // fs.Read(buffer,0, 128);
                fs.ReadExactly(buffer,0, 128);
            }
            return buffer;
        }
        public Id3v1Tag(string path){
            Path = path;
            byte[] buffer = ProcesarTag();
            string tag = Encoding.ASCII.GetString(buffer, 0, 3);
            if (tag == "TAG")
            {
                Titulo     = Encoding.ASCII.GetString(buffer, 3,  30).TrimEnd('\0'); //mostrar
                Artista    = Encoding.ASCII.GetString(buffer, 33, 30).TrimEnd('\0'); //mostrar
                Album      = Encoding.ASCII.GetString(buffer, 63, 30).TrimEnd('\0'); //mostrar
                Anio       = Encoding.ASCII.GetString(buffer, 93,  4).TrimEnd('\0'); //mostrar
                Comentario = Encoding.ASCII.GetString(buffer, 97, 30).TrimEnd('\0');
                Genero     = Encoding.ASCII.GetString(buffer, 127, 1).TrimEnd('\0');
            }
            else
            {
                Console.WriteLine("Error al cargar. Posible archivo incorrecto."); 
            }
        }
        public override string ToString()
        {
            return $"Titulo: {Titulo}, Artista: {Artista}, Album: {Album}, Año: {Anio}";
        }
        public string InfoMP3()
        {
            return $"Titulo: {Titulo}, Artista: {Artista}, Album: {Album}, Año: {Anio}, Comentario: {Comentario}, Genero: {Genero}";
        }
        public string EnRenglones()
        {
            return $"""
            Titulo: {Titulo}
            Artista: {Artista}
            Album: {Album}
            Año: {Anio}
            Comentario: {Comentario}
            Genero: {Genero}
            """;
        }
    }
// }



