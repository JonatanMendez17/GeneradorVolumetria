namespace GeneradorVolumetria.GeneradoresProcesos
{
    internal class GeneradorBajaFraude : IGenerador
    {
        public int Columns => 3;

        public string GenerarLinea(Random? random)
        {
            var customerIntegrationId = (long)(random!.NextDouble() * 999999999999999999);
            var reason = "FRAUDE";

            return $"{customerIntegrationId:D18}|{reason}";
        }

        public string GetIdentifier(Random? random)
        {
            var assetId = (long)(random!.NextDouble() * 999999999999999999);
            return $"{assetId:D18}";
        }
    }
}
