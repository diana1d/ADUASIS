using Aduasis.Api.Modelos;

namespace Aduasis.Api.Interfaces;

/// <summary>
/// Contrato para el acceso a datos de los catálogos del sistema.
/// Agrupa tipos de activo, marcas, modelos y estados en una sola interfaz
/// porque todos comparten el mismo patrón CRUD simple.
/// </summary>
public interface IRepositorioCatalogos
{
    // Tipos de activo
    Task<IEnumerable<TipoActivo>> ObtenerTiposActivoAsync();
    Task<TipoActivo?> ObtenerTipoActivoPorIdAsync(int id);
    Task<TipoActivo> AgregarTipoActivoAsync(TipoActivo tipo);
    Task ActualizarTipoActivoAsync(TipoActivo tipo);

    // Marcas
    Task<IEnumerable<Marca>> ObtenerMarcasAsync();
    Task<Marca?> ObtenerMarcaPorIdAsync(int id);
    Task<Marca> AgregarMarcaAsync(Marca marca);
    Task ActualizarMarcaAsync(Marca marca);

    // Modelos (filtrados por marca)
    Task<IEnumerable<ModeloActivo>> ObtenerModelosAsync(int? marcaId = null);
    Task<ModeloActivo?> ObtenerModeloPorIdAsync(int id);
    Task<ModeloActivo> AgregarModeloAsync(ModeloActivo modelo);
    Task ActualizarModeloAsync(ModeloActivo modelo);

    // Estados de activo
    Task<IEnumerable<EstadoActivo>> ObtenerEstadosActivoAsync();
    Task<EstadoActivo?> ObtenerEstadoActivoPorIdAsync(int id);
    Task<EstadoActivo> AgregarEstadoActivoAsync(EstadoActivo estado);
    Task ActualizarEstadoActivoAsync(EstadoActivo estado);
}
