using Aduasis.Api.Datos;
using Microsoft.EntityFrameworkCore;

namespace Aduasis.Api.Configuracion;

/// <summary>
/// Extensión para registrar el contexto de base de datos con PostgreSQL.
/// </summary>
public static class ConfiguracionBaseDatos
{
    public static IServiceCollection AgregarBaseDatos(
        this IServiceCollection servicios,
        IConfiguration configuracion)
    {
        var cadenaConexion = configuracion.GetConnectionString("BaseDatos")
            ?? throw new InvalidOperationException(
                "La cadena de conexión 'BaseDatos' no está configurada.");

        servicios.AddDbContext<ContextoAduasis>(opciones =>
            opciones.UseNpgsql(cadenaConexion));

        return servicios;
    }
}
