namespace Aduasis.Api.Configuracion;

/// <summary>
/// Extensión para configurar la política de CORS (Cross-Origin Resource Sharing).
/// Permite que el frontend React en localhost:5173 pueda comunicarse con la API.
/// En producción, los orígenes permitidos deben restringirse al dominio real.
/// </summary>
public static class ConfiguracionCors
{
    public static IServiceCollection AgregarCors(
        this IServiceCollection servicios,
        IConfiguration configuracion)
    {
        var origenesPermitidos = configuracion
            .GetSection("Cors:OrigenesPermitidos")
            .Get<string[]>() ?? [];

        servicios.AddCors(opciones =>
        {
            opciones.AddPolicy("PoliticaCors", politica =>
            {
                politica
                    .WithOrigins(origenesPermitidos)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        return servicios;
    }
}
