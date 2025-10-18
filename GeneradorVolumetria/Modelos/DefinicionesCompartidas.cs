namespace GeneradorVolumetria.Modelos
{
    public interface IGenerador
    {
        string GenerarLinea(Random? random);
        string ObtenerId(Random? random);
        int Columnas { get; }
    }

    public enum TipoProceso
    {
        GestionDeBajas,
        GestionDeDevoluciones,
        GestionDeFraudes,
        GestionDePlanes,
        GestionDeRenovacion,
        GestionDeServicios,
    }
}
