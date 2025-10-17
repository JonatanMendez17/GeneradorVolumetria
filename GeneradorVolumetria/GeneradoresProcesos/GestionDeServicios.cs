namespace GeneradorVolumetria.GeneradoresProcesos
{
    internal class GestionDeServicios : IGenerador
    {
        private bool _isIMEI = false;
        public int Columns => 6;
        public string GenerarLinea(Random? random)
        {
            _isIMEI = random.Next(2) == 1;

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

        public string GetIdentifier(Random? random)
        {
            var nroIMEI = (long)(random.NextDouble() * 999999999999999);
            var nroLinea = (long)random.NextInt64(100000000, 999999999);

            return _isIMEI ? $"|{nroIMEI:D15}" : $"{nroLinea:D10}|";
        }
    }
}
