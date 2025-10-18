namespace GeneradorVolumetria.Configuracion
{
    // Configuración centralizada de la aplicación
    public static class ConfiguracionApp
    {
        // Límites de validación
        public const int CANTIDAD_MAXIMA = 1000000;
        public const int CANTIDAD_PEQUENA = 100;
        public const int CANTIDAD_MEDIANA = 1000;
        
        // Intervalos de progreso
        public const int INTERVALO_PROGRESO_PEQUENO = 100;
        public const int INTERVALO_PROGRESO_MEDIANO = 1000;
        public const int INTERVALO_PROGRESO_GRANDE = 10000;
        
        // Estimaciones
        public const int BYTES_ESTIMADO = 50;
        
        // Rutas
        public static readonly string Directorio = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Volumetria_Generada");
        
        // Formatos
        public const string FORMATO_TIMESTAMP = "yyyy-MM-dd_HH-mm";
        public const string FORMATO_NOMBRE_ARCHIVO = "{0}_Cant_{1}_{2}.txt";
    }
}
