using GeneradorVolumetria.Configuracion;

namespace GeneradorVolumetria.Servicios
{
    // Servicio para validaciones de entrada
    public interface IValidacion
    {
        ResultadoValidacion ValidarCantidadRegistros(string input);
        bool RequiereConfirmacion(int cantidadRegistros);
    }

    public class Validacion : IValidacion
    {
        public ResultadoValidacion ValidarCantidadRegistros(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return new ResultadoValidacion 
                { 
                    EsValido = false, 
                    Error = "No se ingresó ningún valor." 
                };
            }

            if (!int.TryParse(input, out int cantidadRegistros) || cantidadRegistros <= 0)
            {
                return new ResultadoValidacion 
                { 
                    EsValido = false, 
                    Error = "Cantidad inválida. Debe ser un número mayor a 0." 
                };
            }

            return new ResultadoValidacion 
            { 
                EsValido = true, 
                CantidadRegistros = cantidadRegistros 
            };
        }

        public bool RequiereConfirmacion(int cantidadRegistros)
        {
            return cantidadRegistros > ConfiguracionApp.CANTIDAD_MAXIMA;
        }
    }

    public class ResultadoValidacion
    {
        public bool EsValido { get; set; }
        public int CantidadRegistros { get; set; }
        public string? Error { get; set; }
    }
}
