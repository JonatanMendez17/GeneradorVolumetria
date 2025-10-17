namespace GeneradorVolumetria.GeneradoresProcesos
{
    internal class GestionDeRenovacion : IGenerador
    {
        public int Columns => 2;

        public string GenerarLinea(Random? random)
        {
            var promocion = (long)(random!.NextDouble() * 9999);
            return $"Fide_{promocion:D4}";//promotion
        }

        public string GetIdentifier(Random? random)
        {
            var primerItem = (long)(random!.NextDouble() * 999999999999999999);
            return $"{primerItem:D18}";
        }
    }
}
