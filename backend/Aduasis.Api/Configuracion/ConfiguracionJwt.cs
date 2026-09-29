using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Aduasis.Api.Configuracion;

/// <summary>
/// Extensión para configurar la autenticación JWT Bearer.
/// La clave secreta debe estar en variables de entorno o appsettings.Development.json
/// y NUNCA debe ser commiteada al repositorio en producción.
/// </summary>
public static class ConfiguracionJwt
{
    public static IServiceCollection AgregarAutenticacionJwt(
        this IServiceCollection servicios,
        IConfiguration configuracion)
    {
        var claveSecreta = configuracion["Jwt:ClaveSecreta"]
            ?? throw new InvalidOperationException(
                "La clave secreta JWT no está configurada.");

        var emisor = configuracion["Jwt:Emisor"]
            ?? throw new InvalidOperationException(
                "El emisor JWT no está configurado.");

        var audiencia = configuracion["Jwt:Audiencia"]
            ?? throw new InvalidOperationException(
                "La audiencia JWT no está configurada.");

        var claveBytes = Encoding.UTF8.GetBytes(claveSecreta);

        servicios
            .AddAuthentication(opciones =>
            {
                opciones.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opciones.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opciones =>
            {
                opciones.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = emisor,
                    ValidAudience = audiencia,
                    IssuerSigningKey = new SymmetricSecurityKey(claveBytes),
                    ClockSkew = TimeSpan.Zero // Sin tolerancia de tiempo extra
                };
            });

        return servicios;
    }
}
