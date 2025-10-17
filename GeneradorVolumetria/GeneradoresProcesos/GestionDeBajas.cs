namespace GeneradorVolumetria.GeneradoresProcesos
{
    // Generador de datos para el proceso de gestión de bajas de equipos.
    // Simula la generación equipos de clientes dados de baja del sistema.

    internal class GestionDeBajas : IGenerador
    {
        public int Columnas => 1;

        public string GenerarLinea(Random? random)
        {
            var motivo = "Descripcion de motivo";
            return $"{motivo}";
        }

        public string ObtenerId(Random? random)
        {
            var assetId = (long)(random!.NextDouble() * 999999999999999999);
            return $"{assetId:D17}";
        }
    }
}
