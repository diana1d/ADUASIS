using Aduasis.Api.Modelos;

namespace Aduasis.Api.Interfaces;

/// <summary>
/// Contrato para el acceso a datos de la jerarquía de ubicaciones:
/// Edificio → Piso → Área / Espacio
/// </summary>
public interface IRepositorioUbicaciones
{
    // Edificios
    Task<IEnumerable<Edificio>> ObtenerEdificiosAsync();
    Task<Edificio?> ObtenerEdificioPorIdAsync(int id);
    Task<Edificio> AgregarEdificioAsync(Edificio edificio);
    Task ActualizarEdificioAsync(Edificio edificio);

    // Pisos
    Task<IEnumerable<Piso>> ObtenerPisosAsync(int? edificioId = null);
    Task<Piso?> ObtenerPisoPorIdAsync(int id);
    Task<Piso> AgregarPisoAsync(Piso piso);
    Task ActualizarPisoAsync(Piso piso);

    // Áreas
    Task<IEnumerable<Area>> ObtenerAreasAsync(int? pisoId = null);
    Task<Area?> ObtenerAreaPorIdAsync(int id);
    Task<Area> AgregarAreaAsync(Area area);
    Task ActualizarAreaAsync(Area area);

    // Espacios
    Task<IEnumerable<Espacio>> ObtenerEspaciosAsync(int? pisoId = null);
    Task<Espacio?> ObtenerEspacioPorIdAsync(int id);
    Task<Espacio> AgregarEspacioAsync(Espacio espacio);
    Task ActualizarEspacioAsync(Espacio espacio);
}
