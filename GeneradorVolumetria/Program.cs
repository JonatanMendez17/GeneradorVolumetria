using GeneradorVolumetria.Servicios;
using GeneradorVolumetria.Fabrica;
using GeneradorVolumetria.Configuracion;
using GeneradorVolumetria.Modelos;

namespace GeneradorVolumetria
{
    internal class Program
    {
        private static Servicios.GeneradorVolumetria? _generadorVolumetria;
        private static IValidacion? _validacion;

        static void Main()
        {
            try
            {
                Inicializar();
                CrearDirectorio();
                MostrarMenu();
                EjecutarBuclePrincipal();
            }
            catch (Exception ex)
            {
                MostrarError("Error al inicializar la aplicación:", ex.Message);
                Console.WriteLine("\nPresiona cualquier tecla para salir...");
                Console.ReadKey();
            }
        }

        private static void Inicializar()
        {
            var fabricaGenerador = new FabricaGenerador();
            var archivo = new Archivo();
            var validacion = new Validacion();
            
            _generadorVolumetria = new Servicios.GeneradorVolumetria(fabricaGenerador, archivo);
            _validacion = validacion;
        }

        private static void CrearDirectorio()
        {
            if (!Directory.Exists(ConfiguracionApp.Directorio))
            {
                Directory.CreateDirectory(ConfiguracionApp.Directorio);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Carpeta creada: {ConfiguracionApp.Directorio}");
                Console.ResetColor();
            }
        }

        private static void EjecutarBuclePrincipal()
        {
            while (true)
            {
                try
                {
                    Console.Write("\nIngresa tu opción: ");
                    var opcion = Console.ReadLine();

                    if (opcion == "0")
                    {
                        MostrarDespedida();
                        break;
                    }

                    if (int.TryParse(opcion, out int opcionNum) && opcionNum >= 1 && opcionNum <= 6)
                    {
                        var tipoProceso = (TipoProceso)(opcionNum - 1);
                        GenerarProceso(tipoProceso);
                    }
                    else
                    {
                        MostrarError("Opción inválida. Intenta de nuevo.");
                    }

                    PausarYLimpiarPantalla();
                }
                catch (Exception ex)
                {
                    MostrarError("Error inesperado en el menú principal:", ex.Message);
                    PausarYLimpiarPantalla();
                }
            }
        }

        private static void GenerarProceso(TipoProceso tipoProceso)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Generando datos para: {tipoProceso}");
                Console.ResetColor();

                var cantidadRegistros = SolicitarCantidadRegistros();
                if (cantidadRegistros == null) return;

                if (_validacion!.RequiereConfirmacion(cantidadRegistros.Value))
                {
                    if (!ConfirmarGeneracionMasiva()) return;
                }

                Console.WriteLine("\nPresiona 'C' para cancelar");

                var resultado = _generadorVolumetria!.GenerarProceso(tipoProceso, cantidadRegistros.Value);

                if (resultado.Exitoso)
                {
                    MostrarResultadoExitoso(resultado);
                }
                else if (resultado.Cancelado)
                {
                    Console.WriteLine("No se generó ningún archivo.");
                }
                else
                {
                    MostrarError("Error en la generación:", resultado.Error ?? "Error desconocido");
                }
            }
            catch (DirectoryNotFoundException)
            {
                MostrarError("No se pudo encontrar el directorio de destino.", ConfiguracionApp.Directorio);
            }
            catch (OutOfMemoryException)
            {
                MostrarError("Memoria insuficiente para generar tantos registros.", "Intenta con una cantidad menor.");
            }
            catch (Exception ex)
            {
                MostrarError("Error inesperado:", $"{ex.GetType().Name}: {ex.Message}");
            }
        }

        private static int? SolicitarCantidadRegistros()
        {
            Console.Write("\nIngresa la cantidad de registros: ");
            var input = Console.ReadLine();

            var validacion = _validacion!.ValidarCantidadRegistros(input ?? "");
            if (!validacion.EsValido)
            {
                MostrarError(validacion.Error!);
                return null;
            }

            return validacion.CantidadRegistros;
        }

        private static bool ConfirmarGeneracionMasiva()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Cantidad muy grande. Esto puede tomar mucho tiempo. ¿Continuar? (si/no)");
            Console.ResetColor();
            var confirmacion = Console.ReadLine()?.ToLower();
            return confirmacion == "s" || confirmacion == "si" || confirmacion == "y" || confirmacion == "yes";
        }

        private static void MostrarResultadoExitoso(ResultadoGeneracion resultado)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nArchivo generado correctamente:");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Ubicación: {resultado.RutaArchivo}");
            Console.WriteLine($"Registros: {resultado.CantidadRegistros:N0}");
            Console.ResetColor();
        }

        private static void MostrarError(string mensaje, string? detalle = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mensaje);
            if (!string.IsNullOrEmpty(detalle))
            {
                Console.WriteLine(detalle);
            }
            Console.ResetColor();
        }

        private static void MostrarDespedida()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("¡Hasta luego!");
            Console.ResetColor();
        }

        private static void PausarYLimpiarPantalla()
        {
            Console.WriteLine("\nPresiona enter para continuar...");
            Console.ReadKey();
            Console.Clear();
            MostrarMenu();
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
    }
}