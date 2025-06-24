// See https://aka.ms/new-console-template for more information
string path = "";

        do
        {
            Console.WriteLine("Ingrese la ruta de la carpeta a analizar:");
            path = Console.ReadLine();
        } while (string.IsNullOrEmpty(path) || string.IsNullOrWhiteSpace(path));

        if (Directory.Exists(path))
        {
            var directories = Directory.GetDirectories(path);
            var files = Directory.GetFiles(path);

            if (directories.Length > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine($"Listados de directorios en {path}");
                Console.ResetColor();
                foreach (var directory in directories)
                {
                    var nameDirectory = Path.GetFileName(directory);
                    Console.WriteLine(nameDirectory);
                }
            }
            else
            {
                Console.WriteLine($"El directorio {path} no contiene subdirectorios");
            }

            Console.WriteLine();
            if (files.Length > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Listados de archivos en {path}");
                Console.ResetColor();
                foreach (var pathFile in files)
                {
                    var nameFile = Path.GetFileName(pathFile);
                    var info = new FileInfo(pathFile);
                    var length = info.Length / 1024.0;
                    Console.WriteLine($" {nameFile} - Tamaño {info.Length / 1024} KB ");
                }

                string path_file = "reporte_archivos.csv";

                string path_reporte = Path.Combine(path, path_file);


                using (var stream = new StreamWriter(path_reporte))
                {
                    stream.WriteLine($"Archivo,Tamaño en KB,Fecha de ultima modificación");
                    foreach (var item in files)
                    {
                        string nombre = Path.GetFileName(item);
                        var info = new FileInfo(item);
                        var length = info.Length / 1024.0;
                        var date = info.LastWriteTime;
                        stream.WriteLine($"{nombre},{length},{date}");
                    }
                }

            }
            else
            {
                Console.WriteLine($"El directorio {path} no contiene archivos");
            }

        }
        else
        {
            Console.WriteLine($"No se encontro el directorio: {path}");
        }


