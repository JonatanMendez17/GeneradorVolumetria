using GeneradorVolumetria.Modelos;

namespace GeneradorVolumetria.GeneradoresProcesos
{
    // Generador de datos para el proceso de gestión de fraudes.
    // Simula la generación de registros de equipos marcados como fraudulentos en el sistema.

    internal class GestionDeFraudes : IGenerador
    {
        public int Columnas => 3;

        public string GenerarLinea(Random? random)
        {
            var razon = "FRAUDE";
            return $"{razon}";
        }

        public string ObtenerId(Random? random)
        {
            var assetId = (long)(random!.NextDouble() * 999999999999999999);
            return $"{assetId:D18}";
        }
    }
}
