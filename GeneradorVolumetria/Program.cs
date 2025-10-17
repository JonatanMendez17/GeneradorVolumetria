using GeneradorVolumetria.GeneradoresProcesos;

namespace GeneradorVolumetria
{
    internal class Program
    {
        static readonly string _directorio = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Volumetria_Generada");

        static void Main()
        {
            try
            {
                if (!Directory.Exists(_directorio))
                {
                    Directory.CreateDirectory(_directorio);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"📁 Carpeta creada: {_directorio}");
                    Console.ResetColor();
                }

                Console.Clear();
                MostrarMenu();
            }
            catch (UnauthorizedAccessException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: No tienes permisos para crear la carpeta en el escritorio.");
                Console.WriteLine($"Directorio: {_directorio}");
                Console.WriteLine("Intenta ejecutar como administrador.");
                Console.ResetColor();
                Console.WriteLine("\nPresiona cualquier tecla para salir...");
                Console.ReadKey();
                return;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error al inicializar la aplicación:");
                Console.WriteLine($"Mensaje: {ex.Message}");
                Console.ResetColor();
                Console.WriteLine("\nPresiona cualquier tecla para salir...");
                Console.ReadKey();
                return;
            }
            
            while (true)
            {
                try
                {
                    Console.Write("\nIngresa tu opción: ");
                    var opcion = Console.ReadLine();

                    if (opcion == "0")
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("¡Hasta luego!");
                        Console.ResetColor();
                        break;
                    }

                    if (int.TryParse(opcion, out int opcionNum) && opcionNum >= 1 && opcionNum <= 6)
                    {
                        var tipoProceso = (TipoProceso)(opcionNum - 1);
                        GenerarProceso(tipoProceso);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opción inválida. Intenta de nuevo.");
                        Console.ResetColor();
                    }

                    Console.WriteLine("\nPresiona enter para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                    MostrarMenu();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error inesperado en el menú principal:");
                    Console.WriteLine($"Mensaje: {ex.Message}");
                    Console.ResetColor();
                    Console.WriteLine("\nPresiona enter para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                    MostrarMenu();
                }
            }
        }

        static void MostrarMenu()
        {
            Console.WriteLine("╔═════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                   GENERADOR DE VOLUMETRÍA                   ║");
            Console.WriteLine("║                    Herramienta para Test                    ║");
            Console.WriteLine("╠═════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║ 1. Gestión de Bajas                                         ║");
            Console.WriteLine("║ 2. Gestión de Devoluciones                                  ║");
            Console.WriteLine("║ 3. Gestión de Fraudes                                       ║");
            Console.WriteLine("║ 4. Gestión de Planes                                        ║");
            Console.WriteLine("║ 5. Gestión de Renovación                                    ║");
            Console.WriteLine("║ 6. Gestión de Servicios                                     ║");
            Console.WriteLine("║ 0. Salir                                                    ║");
            Console.WriteLine("╚═════════════════════════════════════════════════════════════╝");
        }

        static void GenerarProceso(TipoProceso tipoProceso)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Generando datos para: {tipoProceso}");
                Console.ResetColor();
                
                Console.Write("\nIngresa la cantidad de registros: ");
                var input = Console.ReadLine();
                
                int cantidadRegistros;
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No se ingresó ningún valor.");
                    Console.ResetColor();
                    return;
                }
                else if (!int.TryParse(input, out cantidadRegistros) || cantidadRegistros <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Cantidad inválida. Debe ser un número mayor a 0.");
                    Console.ResetColor();
                    return;
                }
                else if (cantidadRegistros > 1000000)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Cantidad muy grande. Esto puede tomar mucho tiempo. ¿Continuar? (si/no)");
                    Console.ResetColor();
                    var confirmacion = Console.ReadLine()?.ToLower();
                    if (confirmacion != "s" && confirmacion != "si" && confirmacion != "y" && confirmacion != "yes")
                    {
                        Console.WriteLine("Operación cancelada.");
                        return;
                    }
                }

            var random = new Random();
            var lineas = new string[cantidadRegistros];
            var generador = GetGeneradorPorProceso(tipoProceso);

            Console.WriteLine($"Generando {cantidadRegistros:N0} registros...");
            Console.WriteLine("\nPresiona 'C' para cancelar");

            var cancelado = false;
            for (var i = 0; i < lineas.Length && !cancelado; i++)
            {
                // Verificar si se presionó una tecla (sin bloquear)
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.KeyChar == 'c' || key.KeyChar == 'C')
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Generación cancelada por el usuario");
                        Console.ResetColor();
                        cancelado = true;
                        break;
                    }
                }

                var obtenerId = generador.ObtenerId(random);
                var formatoLinea = generador.GenerarLinea(random);

                lineas[i] = (generador.Columnas > 1) ? $"{obtenerId}|{formatoLinea}" : $"{obtenerId}";

                bool mostrarProgreso;
                if (cantidadRegistros <= 100)
                {
                    mostrarProgreso = (i + 1) % 10 == 0 || i == lineas.Length - 1;
                }
                else if (cantidadRegistros <= 1000)
                {
                    mostrarProgreso = (i + 1) % 100 == 0 || i == lineas.Length - 1;
                }
                else
                {
                    mostrarProgreso = (i + 1) % 1000 == 0 || i == lineas.Length - 1;
                }

                if (mostrarProgreso)
                {
                    var porcentaje = (i + 1) * 100 / lineas.Length;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"\rProgreso: {i + 1:N0}/{cantidadRegistros:N0} ({porcentaje}%)");
                    Console.ResetColor();
                }
            }
            
            Console.WriteLine();

                if (!cancelado)
                {
                    var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm");
                    var nombreArchivo = $"{tipoProceso}_Cant_{cantidadRegistros}_{timestamp}.txt";
                    var rutaCompleta = Path.Combine(_directorio, nombreArchivo);
                    
                    // Verificar espacio en disco
                    var drive = new DriveInfo(Path.GetPathRoot(_directorio));
                    var espacioNecesario = cantidadRegistros * 50; // Estimación aproximada en bytes
                    if (drive.AvailableFreeSpace < espacioNecesario)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("No hay suficiente espacio en disco para generar el archivo.");
                        Console.ResetColor();
                        return;
                    }
                    
                    File.WriteAllLines(rutaCompleta, lineas);
                    
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nArchivo generado correctamente:");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Ubicación: {rutaCompleta}");
                    Console.WriteLine($"Registros: {cantidadRegistros:N0}");
                    Console.WriteLine($"Tamaño: {new FileInfo(rutaCompleta).Length / 1024:N0} KB");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("No se generó ningún archivo.");
                }
            }
            catch (DirectoryNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: No se pudo encontrar el directorio de destino.");
                Console.WriteLine($"Directorio: {_directorio}");
                Console.ResetColor();
            }
            catch (UnauthorizedAccessException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: No tienes permisos para escribir en el directorio.");
                Console.WriteLine($"Directorio: {_directorio}");
                Console.ResetColor();
            }
            catch (OutOfMemoryException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Memoria insuficiente para generar tantos registros.");
                Console.WriteLine("Intenta con una cantidad menor.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error inesperado:");
                Console.WriteLine($"Tipo: {ex.GetType().Name}");
                Console.WriteLine($"Mensaje: {ex.Message}");
                Console.ResetColor();
            }
        }

        private static IGenerador GetGeneradorPorProceso(TipoProceso tipoProceso)
        {
            return tipoProceso switch
            {
                TipoProceso.GestionDeBajas => new GestionDeBajas(),
                TipoProceso.GestionDeDevoluciones => new GestionDeDevoluciones(),
                TipoProceso.GestionDeFraudes => new GestionDeFraudes(),
                TipoProceso.GestionDePlanes => new GestionDePlanes(),
                TipoProceso.GestionDeRenovacion => new GestionDeRenovacion(),
                TipoProceso.GestionDeServicios => new GestionDeServicios(),
                _ => throw new NotImplementedException(),
            };
        }
    }

    internal interface IGenerador
    {
        string GenerarLinea(Random? random);
        string ObtenerId(Random? random);
        int Columnas { get; }
    }

    internal enum TipoProceso
    {
        GestionDeBajas,
        GestionDeDevoluciones,
        GestionDeFraudes,
        GestionDePlanes,
        GestionDeRenovacion,
        GestionDeServicios,
    }
}