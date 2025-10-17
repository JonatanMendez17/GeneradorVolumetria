namespace GeneradorVolumetria.GeneradoresProcesos
{
    internal class GestionDePlanes : IGenerador
    {
        public int Columns => 6;

        public string GenerarLinea(Random? random)
        {
            var CustomerId = (long)(random.NextDouble() * 99999999999);
            var BatchId = "CBSDC_CRM_DEG_20250311_602.TXT";
            var BundleId = Guid.NewGuid;
            var SusbscriptionId = Guid.NewGuid;
            var TaskOrderId = "100000000250440155_55053381_2002334408_0";

            return $"{CustomerId}|{BatchId}|{BundleId}|{SusbscriptionId}|{TaskOrderId}";
        }

        public string GetIdentifier(Random? random)
        {
            var AccountId = (long)(random!.NextDouble() * 9999999999999999);
            return $"{AccountId:D16}";
        }
    }
}
