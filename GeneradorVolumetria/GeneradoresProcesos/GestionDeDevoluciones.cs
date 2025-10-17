namespace GeneradorVolumetria.GeneradoresProcesos
{
    internal class GestionDeDevoluciones : IGenerador
    {
        public int Columns => 6;
        
        public string GenerarLinea(Random? random)
        {
            var cantSeriales = random!.Next(1, 200);
            var seriales = new List<long>();
            
            for (int i = 0; i < cantSeriales; i++)
            {
                seriales.Add(random.NextInt64(000000000000, 9999999999999999));
            }

            return $"{string.Join(";", seriales)}";
        }

        public string GetIdentifier(Random? random)
        {
            var NroCuenta = (long)(random!.NextDouble() * 9999999999999999);
            return $"{NroCuenta:D16}";
        }
    }
}
