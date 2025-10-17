namespace GeneradorVolumetria.GeneradoresProcesos
{
    // Generador de datos para el proceso de gestión de devoluciones de equipos.
    // Simula la generación de listas de números de serie de equipos devueltos por clientes.

    internal class GestionDeDevoluciones : IGenerador
    {
        public int Columnas => 6;
        
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

        public string ObtenerId(Random? random)
        {
            var assetId = (long)(random!.NextDouble() * 9999999999999999);
            return $"{assetId:D16}";
        }
    }
}
