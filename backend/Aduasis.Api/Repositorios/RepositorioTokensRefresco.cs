using Aduasis.Api.Datos;
using Aduasis.Api.Interfaces;
using Aduasis.Api.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Aduasis.Api.Repositorios;

/// <summary>
/// Implementación del repositorio de tokens de refresco.
/// </summary>
public class RepositorioTokensRefresco : IRepositorioTokensRefresco
{
    private readonly ContextoAduasis _contexto;

    public RepositorioTokensRefresco(ContextoAduasis contexto)
    {
        _contexto = contexto;
    }

    public async Task<TokenRefresco?> ObtenerPorTokenAsync(string token)
    {
        return await _contexto.TokensRefresco
            .Include(t => t.Usuario)
                .ThenInclude(u => u.Rol)
            .FirstOrDefaultAsync(t => t.Token == token);
    }

    public async Task AgregarAsync(TokenRefresco token)
    {
        await _contexto.TokensRefresco.AddAsync(token);
        await _contexto.SaveChangesAsync();
    }

    public async Task ActualizarAsync(TokenRefresco token)
    {
        _contexto.TokensRefresco.Update(token);
        await _contexto.SaveChangesAsync();
    }

    public async Task RevocarTodosDeUsuarioAsync(int usuarioId)
    {
        var tokens = await _contexto.TokensRefresco
            .Where(t => t.UsuarioId == usuarioId && !t.Revocado)
            .ToListAsync();

        foreach (var token in tokens)
            token.Revocado = true;

        await _contexto.SaveChangesAsync();
    }
}
