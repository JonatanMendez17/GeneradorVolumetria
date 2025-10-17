namespace GeneradorVolumetria.GeneradoresProcesos
{
    // Generador de datos para el proceso de gestión de renovaciones de equipos.
    // Simula la generación de códigos de promoción y comentarios para procesos de renovación.

    internal class GestionDeRenovacion : IGenerador
    {

        public int Columnas => 2;

        public string GenerarLinea(Random? random)
        {
            var promocion = (long)(random!.NextDouble() * 9999);
            var comentario = "Comentario de prueba";
            return $"Renovacion_{promocion:D4}|{comentario}";
        }

        public string ObtenerId(Random? random)
        {
            var assetId = (long)(random!.NextDouble() * 999999999999999999);
            return $"{assetId:D18}";
        }
    }
}
