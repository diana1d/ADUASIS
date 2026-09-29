using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Aduasis.Api.DTOs.Respuestas;
using Aduasis.Api.DTOs.Solicitudes;
using Aduasis.Api.Interfaces;
using Aduasis.Api.Modelos;
using Microsoft.IdentityModel.Tokens;

namespace Aduasis.Api.Servicios;

/// <summary>
/// Servicio de autenticación. Concentra toda la lógica de:
/// - Validar credenciales
/// - Generar access tokens JWT
/// - Gestionar refresh tokens
/// - Cerrar sesión
///
/// El controlador no contiene ninguna de esta lógica,
/// solo llama a este servicio y devuelve la respuesta.
/// </summary>
public class ServicioAutenticacion : IServicioAutenticacion
{
    private readonly IRepositorioUsuarios _repoUsuarios;
    private readonly IRepositorioTokensRefresco _repoTokens;
    private readonly IConfiguration _configuracion;

    // Tiempo de vida del access token: 60 minutos
    private const int MinutosAccessToken = 60;
    // Tiempo de vida del refresh token: 7 días
    private const int DiasRefreshToken = 7;

    public ServicioAutenticacion(
        IRepositorioUsuarios repoUsuarios,
        IRepositorioTokensRefresco repoTokens,
        IConfiguration configuracion)
    {
        _repoUsuarios   = repoUsuarios;
        _repoTokens     = repoTokens;
        _configuracion  = configuracion;
    }

    public async Task<RespuestaAutenticacion> IniciarSesionAsync(
        SolicitudLogin solicitud,
        string? ipOrigen)
    {
        // 1. Buscar usuario por correo o nombre de usuario
        var usuario = await _repoUsuarios.ObtenerPorCredencialAsync(solicitud.Credencial);

        // 2. Validar que existe, está activo y la contraseña es correcta
        if (usuario is null || !usuario.Activo)
            throw new UnauthorizedAccessException("Credenciales inválidas.");

        var contrasenaValida = BCrypt.Net.BCrypt.Verify(solicitud.Contrasena, usuario.HashContrasena);
        if (!contrasenaValida)
            throw new UnauthorizedAccessException("Credenciales inválidas.");

        // 3. Registrar último acceso
        usuario.UltimoAcceso = DateTime.UtcNow;
        await _repoUsuarios.ActualizarAsync(usuario);

        // 4. Generar tokens
        return await GenerarRespuestaAutenticacionAsync(usuario, ipOrigen);
    }

    public async Task<RespuestaAutenticacion> RefrescarTokenAsync(
        SolicitudRefrescarToken solicitud,
        string? ipOrigen)
    {
        var tokenExistente = await _repoTokens.ObtenerPorTokenAsync(solicitud.TokenRefresco);

        // Validar que el token existe, no fue usado, no fue revocado y no expiró
        if (tokenExistente is null || tokenExistente.Usado || tokenExistente.Revocado)
            throw new UnauthorizedAccessException("Token de refresco inválido.");

        if (tokenExistente.ExpiraEn < DateTime.UtcNow)
            throw new UnauthorizedAccessException("Token de refresco expirado.");

        // Marcar el token anterior como usado (rotación de tokens)
        tokenExistente.Usado = true;
        await _repoTokens.ActualizarAsync(tokenExistente);

        // Generar nuevos tokens
        return await GenerarRespuestaAutenticacionAsync(tokenExistente.Usuario, ipOrigen);
    }

    public async Task CerrarSesionAsync(string tokenRefresco)
    {
        var token = await _repoTokens.ObtenerPorTokenAsync(tokenRefresco);
        if (token is null) return;

        // Revocar todos los tokens del usuario (cierre de sesión en todos los dispositivos)
        await _repoTokens.RevocarTodosDeUsuarioAsync(token.UsuarioId);
    }

    // ─────────────────────────────────────────────
    // Métodos privados
    // ─────────────────────────────────────────────

    private async Task<RespuestaAutenticacion> GenerarRespuestaAutenticacionAsync(
        Usuario usuario,
        string? ipOrigen)
    {
        var expiracionAccessToken = DateTime.UtcNow.AddMinutes(MinutosAccessToken);
        var accessToken = GenerarAccessToken(usuario, expiracionAccessToken);
        var tokenRefresco = await GenerarYGuardarRefreshTokenAsync(usuario, ipOrigen);

        return new RespuestaAutenticacion
        {
            AccessToken   = accessToken,
            TokenRefresco = tokenRefresco,
            ExpiraEn      = expiracionAccessToken,
            Usuario = new UsuarioAutenticado
            {
                Id             = usuario.Id,
                NombreCompleto = $"{usuario.Nombre} {usuario.Apellido}",
                NombreUsuario  = usuario.NombreUsuario,
                Correo         = usuario.Correo,
                Rol            = usuario.Rol.Nombre
            }
        };
    }

    private string GenerarAccessToken(Usuario usuario, DateTime expiracion)
    {
        var claveSecreta = _configuracion["Jwt:ClaveSecreta"]!;
        var emisor       = _configuracion["Jwt:Emisor"]!;
        var audiencia    = _configuracion["Jwt:Audiencia"]!;

        var claveBytes   = Encoding.UTF8.GetBytes(claveSecreta);
        var credenciales = new SigningCredentials(
            new SymmetricSecurityKey(claveBytes),
            SecurityAlgorithms.HmacSha256);

        // Claims incluidos en el token JWT
        // El frontend puede leer estos datos sin llamar al servidor
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub,   usuario.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, usuario.Correo),
            new Claim(JwtRegisteredClaimNames.Jti,   Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name,               usuario.NombreUsuario),
            new Claim(ClaimTypes.Role,               usuario.Rol.Nombre)
        };

        var token = new JwtSecurityToken(
            issuer:             emisor,
            audience:           audiencia,
            claims:             claims,
            notBefore:          DateTime.UtcNow,
            expires:            expiracion,
            signingCredentials: credenciales);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<string> GenerarYGuardarRefreshTokenAsync(
        Usuario usuario,
        string? ipOrigen)
    {
        // Generar un token aleatorio criptográficamente seguro
        var bytesAleatorios = RandomNumberGenerator.GetBytes(64);
        var tokenString     = Convert.ToBase64String(bytesAleatorios);

        var tokenRefresco = new TokenRefresco
        {
            UsuarioId = usuario.Id,
            Token     = tokenString,
            ExpiraEn  = DateTime.UtcNow.AddDays(DiasRefreshToken),
            IpOrigen  = ipOrigen
        };

        await _repoTokens.AgregarAsync(tokenRefresco);
        return tokenString;
    }
}
