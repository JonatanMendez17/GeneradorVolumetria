using GeneradorVolumetria.Modelos;

namespace GeneradorVolumetria.GeneradoresProcesos
{
    // Generador de datos para el proceso de gestión de servicios de equipos.
    // Simula la generación de acciones de suspensión/recuperación con tipos técnicos y comerciales,

    internal class GestionDeServicios : IGenerador
    {
        public int Columnas => 6;

        private readonly bool _isIMEI = false;

        public string GenerarLinea(Random? random)
        {
            string[] accion = { "SUS", "REC" };
            string[] tipo = { "COMERCIAL", "TÉCNICO" };
            string[] subtipoTecnico = { "Spam", "Bypass", "Hackeo", "IRSF" };
            string[] subtipoComercial = { "Administrativo por suscripción", "Investigación", "Desconocimiento", "Irregular" };
            var comentario = "Comentario de prueba";

            var accionElegida = accion[random.Next(accion.Length)];

            var tipoElegido = "";
            if (accionElegida == "SUS")
            {
                tipoElegido = tipo[random.Next(tipo.Length)];
            }

            var subtipoElegido = "";
            if (accionElegida == "SUS" && tipoElegido == "TÉCNICO")
            {
                subtipoElegido = subtipoTecnico[random.Next(subtipoTecnico.Length)];
            }
            else if (accionElegida == "SUS" && tipoElegido == "COMERCIAL")
            {
                subtipoElegido = subtipoComercial[random.Next(subtipoComercial.Length)];
            }

            return $"{accionElegida}|{tipoElegido}|{subtipoElegido}|{comentario}";
        }

        public string ObtenerId(Random? random)
        {
            var nroIMEI = (long)(random.NextDouble() * 999999999999999);
            var assetId = (long)random.NextInt64(100000000, 999999999);

            return _isIMEI ? $"|{nroIMEI:D15}" : $"{assetId:D10}|";
        }
    }
}
