using Aduasis.Api.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Aduasis.Api.Datos;

/// <summary>
/// Inicializa la base de datos con datos semilla necesarios para
/// que el sistema sea funcional desde el primer despliegue.
///
/// El usuario administrador inicial se crea aquí porque requiere
/// BCrypt en tiempo de ejecución, que no puede usarse en el seed
/// estático de EF Core (HasData).
///
/// La contraseña se lee desde la variable de entorno
/// ADUASIS_ADMIN_PASSWORD para no quedar en el código ni en el repo.
/// </summary>
public static class InicializadorBD
{
    public static async Task InicializarAsync(IServiceProvider servicios)
    {
        using var alcance = servicios.CreateScope();
        var contexto = alcance.ServiceProvider.GetRequiredService<ContextoAduasis>();
        var logger   = alcance.ServiceProvider.GetRequiredService<ILogger<ContextoAduasis>>();

        try
        {
            // Aplica migraciones pendientes automáticamente al iniciar
            await contexto.Database.MigrateAsync();

            await SembrarAdministradorAsync(contexto, logger);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error al inicializar la base de datos.");
            throw;
        }
    }

    private static async Task SembrarAdministradorAsync(
        ContextoAduasis contexto,
        ILogger logger)
    {
        // Solo crear si no existe ningún administrador todavía
        var existeAdmin = await contexto.Usuarios
            .AnyAsync(u => u.Rol.Nombre == "Administrador");

        if (existeAdmin) return;

        // La contraseña viene de variable de entorno.
        // Si no está definida, usamos una contraseña temporal de desarrollo
        // y advertimos explícitamente en los logs.
        var contrasena = Environment.GetEnvironmentVariable("ADUASIS_ADMIN_PASSWORD");

        if (string.IsNullOrWhiteSpace(contrasena))
        {
            contrasena = "Admin@2024!Temporal";
            logger.LogWarning(
                "⚠ ADUASIS_ADMIN_PASSWORD no está definida. " +
                "Se usó una contraseña temporal. " +
                "Cámbiala inmediatamente en producción.");
        }

        var hashContrasena = BCrypt.Net.BCrypt.HashPassword(contrasena, workFactor: 12);

        var administrador = new Usuario
        {
            RolId         = 1, // Administrador (sembrado en ConfiguracionRol)
            Nombre        = "Administrador",
            Apellido      = "Sistema",
            Correo        = "admin@aduasis.gob.bo",
            NombreUsuario = "admin",
            HashContrasena = hashContrasena,
            Activo        = true,
            CreadoEn      = DateTime.UtcNow,
            ActualizadoEn = DateTime.UtcNow
        };

        contexto.Usuarios.Add(administrador);
        await contexto.SaveChangesAsync();

        logger.LogInformation(
            "✓ Usuario administrador inicial creado. " +
            "Usuario: admin | Correo: admin@aduasis.gob.bo");
    }
}
