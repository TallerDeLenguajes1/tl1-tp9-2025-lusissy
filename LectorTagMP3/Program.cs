using System;
using System.IO;
using System.Text;

 Console.WriteLine("Ingrese la ruta del archivo MP3:");
        string rutaArchivo = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(rutaArchivo))
        {
            Console.WriteLine("No se ingresó una ruta válida.");
            return;
        }

        if (!File.Exists(rutaArchivo))
        {
            Console.WriteLine("El archivo no existe. Verificá la ruta.");
            return;
        }

        if (!rutaArchivo.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("El archivo debe tener extensión .mp3.");
            return;
        }

        try
        {
            using var archivo = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read);

            const int tamañoTag = 128;
            if (archivo.Length < tamañoTag)
            {
                Console.WriteLine("El archivo es muy pequeño para contener un tag ID3v1.");
                return;
            }

            archivo.Seek(-tamañoTag, SeekOrigin.End);
            byte[] datosTag = new byte[tamañoTag];
            archivo.Read(datosTag, 0, tamañoTag);

            var codificacion = Encoding.GetEncoding("latin1");

            string cabecera = codificacion.GetString(datosTag, 0, 3);
            if (cabecera != "TAG")
            {
                Console.WriteLine("No se encontró el tag ID3v1 en el archivo.");
                return;
            }

            string titulo = codificacion.GetString(datosTag, 3, 30).TrimEnd('\0', ' ');
            string artista = codificacion.GetString(datosTag, 33, 30).TrimEnd('\0', ' ');
            string album = codificacion.GetString(datosTag, 63, 30).TrimEnd('\0', ' ');
            string anio = codificacion.GetString(datosTag, 93, 4).TrimEnd('\0', ' ');

            cancion cancion=new cancion(titulo, artista, album, anio);
            cancion.mostrar();
        }
        catch (Exception error)
        {
            Console.WriteLine("Error al leer el archivo: " + error.Message);
        }
