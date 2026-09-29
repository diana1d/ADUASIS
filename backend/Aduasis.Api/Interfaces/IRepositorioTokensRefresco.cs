using Aduasis.Api.Modelos;

namespace Aduasis.Api.Interfaces;

/// <summary>
/// Contrato para el acceso a datos de tokens de refresco.
/// </summary>
public interface IRepositorioTokensRefresco
{
    Task<TokenRefresco?> ObtenerPorTokenAsync(string token);
    Task AgregarAsync(TokenRefresco token);
    Task ActualizarAsync(TokenRefresco token);

    /// <summary>
    /// Revoca todos los tokens activos de un usuario (cierre de sesión total).
    /// </summary>
    Task RevocarTodosDeUsuarioAsync(int usuarioId);
}
