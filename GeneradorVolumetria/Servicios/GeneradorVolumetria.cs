using GeneradorVolumetria.Configuracion;
using GeneradorVolumetria.Fabrica;
using GeneradorVolumetria.Modelos;

namespace GeneradorVolumetria.Servicios
{
    // Servicio principal para la generación de volumetría
    public class GeneradorVolumetria(IFabricaGenerador fabricaGenerador, IArchivo archivo)
    {
        private readonly IFabricaGenerador _fabricaGenerador = fabricaGenerador;
        private readonly IArchivo _archivo = archivo;

        public ResultadoGeneracion GenerarProceso(TipoProceso tipoProceso, int cantidadRegistros)
        {
            try
            {
                var generador = _fabricaGenerador.CrearGenerador(tipoProceso);
                var lineas = new string[cantidadRegistros];
                var random = new Random();

                for (var i = 0; i < lineas.Length; i++)
                {
                    var obtenerId = generador.ObtenerId(random);
                    var formatoLinea = generador.GenerarLinea(random);
                    lineas[i] = (generador.Columnas > 1) ? $"{obtenerId}|{formatoLinea}" : $"{obtenerId}";
                }

                var nombreArchivo = string.Format(ConfiguracionApp.FORMATO_NOMBRE_ARCHIVO, 
                    tipoProceso, cantidadRegistros, DateTime.Now.ToString(ConfiguracionApp.FORMATO_TIMESTAMP));
                
                var rutaCompleta = _archivo.GuardarArchivo(nombreArchivo, lineas);
                
                return new ResultadoGeneracion 
                { 
                    Exitoso = true, 
                    RutaArchivo = rutaCompleta, 
                    CantidadRegistros = cantidadRegistros 
                };
            }
            catch (Exception ex)
            {
                return new ResultadoGeneracion 
                { 
                    Exitoso = false, 
                    Error = ex.Message 
                };
            }
        }

        public static bool DebeMostrarProgreso(int cantidadRegistros, int indiceActual)
        {
            var intervalo = cantidadRegistros switch
            {
                <= ConfiguracionApp.CANTIDAD_PEQUENA => ConfiguracionApp.INTERVALO_PROGRESO_PEQUENO,
                <= ConfiguracionApp.CANTIDAD_MEDIANA => ConfiguracionApp.INTERVALO_PROGRESO_MEDIANO,
                _ => ConfiguracionApp.INTERVALO_PROGRESO_GRANDE
            };

            return (indiceActual + 1) % intervalo == 0 || indiceActual == cantidadRegistros - 1;
        }
    }

    public class ResultadoGeneracion
    {
        public bool Exitoso { get; set; }
        public bool Cancelado { get; set; }
        public string? RutaArchivo { get; set; }
        public int CantidadRegistros { get; set; }
        public string? Error { get; set; }
    }
}
