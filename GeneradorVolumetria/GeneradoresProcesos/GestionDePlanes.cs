using GeneradorVolumetria.Modelos;

namespace GeneradorVolumetria.GeneradoresProcesos
{
    // Generador de datos para el proceso de gestión de planes de suscripción.
    // Simula la generación de lotes de procesamiento con identificadores de paquetes, suscripciones y órdenes de tarea.

    internal class GestionDePlanes : IGenerador
    {
        public int Columnas => 6;

        public string GenerarLinea(Random? random)
        {
            var idLote = "CBSDC_CRM_DEG_20250311_602.TXT";
            var idPaquete = Guid.NewGuid;
            var idSuscripcion = Guid.NewGuid;
            var idOrdenTarea = "100000000250440155_55053381_2002334408_0";

            return $"{idLote}|{idPaquete}|{idSuscripcion}|{idOrdenTarea}";
        }

        public string ObtenerId(Random? random)
        {
            var assetId = (long)(random!.NextDouble() * 9999999999999999);
            return $"{assetId:D16}";
        }
    }
}
