namespace Aduasis.Api.Modelos;

/// <summary>
/// Constantes para los tipos de evento del historial.
/// Centralizar los valores aquí evita strings mágicos dispersos en el código.
/// </summary>
public static class TipoEventoHistorial
{
    public const string Creacion         = "CREACION";
    public const string CambioEstado     = "CAMBIO_ESTADO";
    public const string CambioUbicacion  = "CAMBIO_UBICACION";
    public const string CambioAsignacion = "CAMBIO_ASIGNACION";
    public const string Actualizacion    = "ACTUALIZACION";
    public const string Baja             = "BAJA";
    public const string Nota             = "NOTA";
}
