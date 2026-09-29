using Aduasis.Api.Modelos;

namespace Aduasis.Api.Interfaces;

/// <summary>
/// Contrato para el acceso a datos de usuarios.
/// El controlador y el servicio dependen de esta interfaz,
/// no de la implementación concreta (principio de inversión de dependencias).
/// </summary>
public interface IRepositorioUsuarios
{
    Task<Usuario?> ObtenerPorIdAsync(int id);
    Task<Usuario?> ObtenerPorCorreoAsync(string correo);
    Task<Usuario?> ObtenerPorNombreUsuarioAsync(string nombreUsuario);

    /// <summary>
    /// Busca por correo o nombre de usuario (para login con cualquiera de los dos).
    /// </summary>
    Task<Usuario?> ObtenerPorCredencialAsync(string credencial);

    Task<bool> ExisteCorreoAsync(string correo);
    Task<bool> ExisteNombreUsuarioAsync(string nombreUsuario);
    Task ActualizarAsync(Usuario usuario);
}
