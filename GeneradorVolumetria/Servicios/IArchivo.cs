using GeneradorVolumetria.Configuracion;

namespace GeneradorVolumetria.Servicios
{
    // Servicio para operaciones de archivos
    public interface IArchivo
    {
        string GuardarArchivo(string nombreArchivo, string[] lineas);
        bool VerificarEspacioDisponible(int cantidadRegistros);
        long ObtenerTamañoArchivo(string rutaArchivo);
    }

    public class Archivo : IArchivo
    {
        public string GuardarArchivo(string nombreArchivo, string[] lineas)
        {
            var rutaCompleta = Path.Combine(ConfiguracionApp.Directorio, nombreArchivo);
            File.WriteAllLines(rutaCompleta, lineas);
            return rutaCompleta;
        }

        public bool VerificarEspacioDisponible(int cantidadRegistros)
        {
            var espacioNecesario = cantidadRegistros * ConfiguracionApp.BYTES_ESTIMADO;
            var drive = new DriveInfo(Path.GetPathRoot(ConfiguracionApp.Directorio)!);
            return drive.AvailableFreeSpace >= espacioNecesario;
        }

        public long ObtenerTamañoArchivo(string rutaArchivo)
        {
            var fileInfo = new FileInfo(rutaArchivo);
            return fileInfo.Length;
        }
    }
}
