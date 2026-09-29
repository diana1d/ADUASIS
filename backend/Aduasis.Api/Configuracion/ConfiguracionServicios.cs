using Aduasis.Api.Interfaces;
using Aduasis.Api.Repositorios;
using Aduasis.Api.Servicios;
using FluentValidation;
using FluentValidation.AspNetCore;
namespace Aduasis.Api.Configuracion;

/// <summary>
/// Extensión para registrar los servicios de aplicación, validaciones y mapeos.
/// A medida que se agreguen nuevos servicios en Sprints posteriores,
/// se registrarán aquí de forma organizada.
/// </summary>
public static class ConfiguracionServicios
{
    /// <summary>
    /// Registra todos los servicios y repositorios de la aplicación.
    /// </summary>
    public static IServiceCollection AgregarServiciosAplicacion(
        this IServiceCollection servicios)
    {
        // Repositorios
        servicios.AddScoped<IRepositorioUsuarios, RepositorioUsuarios>();
        servicios.AddScoped<IRepositorioTokensRefresco, RepositorioTokensRefresco>();
        servicios.AddScoped<IRepositorioActivos, RepositorioActivos>();
        servicios.AddScoped<IRepositorioCatalogos, RepositorioCatalogos>();
        servicios.AddScoped<IRepositorioUbicaciones, RepositorioUbicaciones>();
        servicios.AddScoped<IRepositorioHistorial, RepositorioHistorial>();
        servicios.AddScoped<IRepositorioAsignaciones, RepositorioAsignaciones>();

        // Servicios
        servicios.AddScoped<IServicioAutenticacion, ServicioAutenticacion>();
        servicios.AddScoped<IServicioActivos, ServicioActivos>();
        servicios.AddScoped<IServicioHistorial, ServicioHistorial>();
        // ServicioHistorial se registra también como concreto para que
        // ServicioActivos pueda inyectarlo directamente (métodos internos)
        servicios.AddScoped<ServicioHistorial>();

        return servicios;
    }

    /// <summary>
    /// Registra FluentValidation para validación automática de solicitudes.
    /// </summary>
    public static IServiceCollection AgregarValidaciones(
        this IServiceCollection servicios)
    {
        servicios
            .AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters()
            .AddValidatorsFromAssemblyContaining<Program>();

        return servicios;
    }

    /// <summary>
    /// Registra AutoMapper buscando todos los perfiles en el ensamblado actual.
    /// Los perfiles se crearán en la carpeta Mapeos/ por entidad.
    /// </summary>
    public static IServiceCollection AgregarMapeos(
        this IServiceCollection servicios)
    {
        servicios.AddAutoMapper(typeof(Program).Assembly);
        return servicios;
    }
}
