using GeneradorVolumetria.GeneradoresProcesos;

namespace GeneradorVolumetria
{
    internal class Program
    {
        const int _cntRegistros = 10;                             // Se puede modificar la cantidad de registros a generar
        const TipoProceso _tipoProceso = TipoProceso.BajaFraude;    // Se puede modificar el tipo de proceso a generar
        const string _directorio = "C:\\Users\\jonat\\Desktop\\test";        // Se puede modificar la ubicación de guardado del archivo

        static void Main()
        {
            var random = new Random();
            var lineas = new string[_cntRegistros];
            var generador = GetGeneradorPorProceso(_tipoProceso, random);

            for (var i = 0; i < lineas.Length; i++)
            {
                var identifier = generador.GetIdentifier(random);
                var lineaSinIdentifier = generador.GenerarLinea(random);

                if (generador.Columns > 1)
                {
                    lineas[i] = $"{identifier}|{lineaSinIdentifier}";
                }
                else
                {
                    lineas[i] = $"{identifier}";
                }
            }

            File.WriteAllLines($"{_directorio}\\{_tipoProceso}_Volumetria_{_cntRegistros}_{DateTime.Now:yyyyMMddhhmmss}.txt", lineas);
            Console.WriteLine("Archivo generado correctamente.");
        }

        private static IGenerador GetGeneradorPorProceso(TipoProceso tipoProceso, Random? random)
        {
            return tipoProceso switch
            {
                TipoProceso.Fidelizacion => new GeneradorFide(),
                TipoProceso.SuspYReconexion => new GeneradorSuspYRehab(),
                TipoProceso.BajaFraude => new GeneradorBajaFraude(),
                TipoProceso.Cancelador => new GeneradorCancelador(),
                TipoProceso.RecuperoEquipo => new GeneradorRecuperoEquipo(),
                TipoProceso.Degradacion => new GeneradorDegradacion(),
                _ => throw new NotImplementedException(),
            };
        }
    }

    internal interface IGenerador
    {
        string GenerarLinea(Random? random);
        string GetIdentifier(Random? random);
        int Columns { get; }
    }

    internal enum TipoProceso
    {
        BajaFraude,
        Cancelador,
        Degradacion, 
        Fidelizacion,
        RecuperoEquipo,
        SuspYReconexion,
    }
}