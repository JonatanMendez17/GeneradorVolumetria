using GeneradorVolumetria.GeneradoresProcesos;
using GeneradorVolumetria.Modelos;

namespace GeneradorVolumetria.Fabrica
{
    /// Fabrica para crear generadores de procesos
    public interface IFabricaGenerador
    {
        IGenerador CrearGenerador(TipoProceso tipoProceso);
    }

    public class FabricaGenerador : IFabricaGenerador
    {
        public IGenerador CrearGenerador(TipoProceso tipoProceso)
        {
            return tipoProceso switch
            {
                TipoProceso.GestionDeBajas => new GestionDeBajas(),
                TipoProceso.GestionDeDevoluciones => new GestionDeDevoluciones(),
                TipoProceso.GestionDeFraudes => new GestionDeFraudes(),
                TipoProceso.GestionDePlanes => new GestionDePlanes(),
                TipoProceso.GestionDeRenovacion => new GestionDeRenovacion(),
                TipoProceso.GestionDeServicios => new GestionDeServicios(),
                _ => throw new ArgumentException($"Tipo de proceso no soportado: {tipoProceso}")
            };
        }
    }
}
