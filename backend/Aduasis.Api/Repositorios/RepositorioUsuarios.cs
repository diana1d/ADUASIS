using Aduasis.Api.Datos;
using Aduasis.Api.Interfaces;
using Aduasis.Api.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Aduasis.Api.Repositorios;

/// <summary>
/// Implementación del repositorio de usuarios.
/// Solo se encarga del acceso a datos, sin lógica de negocio.
/// </summary>
public class RepositorioUsuarios : IRepositorioUsuarios
{
    private readonly ContextoAduasis _contexto;

    public RepositorioUsuarios(ContextoAduasis contexto)
    {
        _contexto = contexto;
    }

    public async Task<Usuario?> ObtenerPorIdAsync(int id)
    {
        return await _contexto.Usuarios
            .Include(u => u.Rol)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Usuario?> ObtenerPorCorreoAsync(string correo)
    {
        return await _contexto.Usuarios
            .Include(u => u.Rol)
            .FirstOrDefaultAsync(u => u.Correo == correo.ToLower());
    }

    public async Task<Usuario?> ObtenerPorNombreUsuarioAsync(string nombreUsuario)
    {
        return await _contexto.Usuarios
            .Include(u => u.Rol)
            .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario.ToLower());
    }

    public async Task<Usuario?> ObtenerPorCredencialAsync(string credencial)
    {
        var credencialNormalizada = credencial.ToLower().Trim();

        return await _contexto.Usuarios
            .Include(u => u.Rol)
            .FirstOrDefaultAsync(u =>
                u.Correo == credencialNormalizada ||
                u.NombreUsuario == credencialNormalizada);
    }

    public async Task<bool> ExisteCorreoAsync(string correo)
    {
        return await _contexto.Usuarios
            .AnyAsync(u => u.Correo == correo.ToLower());
    }

    public async Task<bool> ExisteNombreUsuarioAsync(string nombreUsuario)
    {
        return await _contexto.Usuarios
            .AnyAsync(u => u.NombreUsuario == nombreUsuario.ToLower());
    }

    public async Task ActualizarAsync(Usuario usuario)
    {
        _contexto.Usuarios.Update(usuario);
        await _contexto.SaveChangesAsync();
    }
}
