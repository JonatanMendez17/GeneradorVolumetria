namespace GeneradorVolumetria.GeneradoresProcesos
{
    internal class GeneradorCancelador : IGenerador
    {
        public int Columns => 1;

        public string GenerarLinea(Random? random)
        {
            return string.Empty;
        }

        public string GetIdentifier(Random? random)
        {
            var caseNumber = (long)(random!.NextDouble() * 999999999999999999);
            var motivo = "Descripcion de motivo";
            return $"{caseNumber:D18}|{motivo}";
        }
    }
}
