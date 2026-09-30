import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import Cargando from '../Componentes/comunes/Cargando';
import MensajeError from '../Componentes/comunes/MensajeError';
import servicioActivos from '../Servicios/servicioActivos';
import type { Activo } from '../Tipos/activos';
import { usarSesion } from '../Hooks/usarSesion';

/**
 * Página de listado de activos tecnológicos.
 * Muestra todos los activos con búsqueda y filtro por estado.
 */
export default function PaginaActivos() {
  const [activos, setActivos] = useState<Activo[]>([]);
  const [filtrados, setFiltrados] = useState<Activo[]>([]);
  const [cargando, setCargando] = useState(true);
  const [error, setError] = useState('');
  const [busqueda, setBusqueda] = useState('');
  const [filtroEstado, setFiltroEstado] = useState('');
  const { usuario } = usarSesion();

  const puedeCrear = usuario?.rol === 'Administrador' || usuario?.rol === 'Funcionario';

  useEffect(() => {
    servicioActivos
      .obtenerTodos()
      .then((datos) => {
        setActivos(datos);
        setFiltrados(datos);
      })
      .catch(() => setError('No se pudieron cargar los activos.'))
      .finally(() => setCargando(false));
  }, []);

  // Filtrar cada vez que cambia la búsqueda o el filtro de estado
  useEffect(() => {
    let resultado = activos;

    if (busqueda.trim()) {
      const termino = busqueda.toLowerCase();
      resultado = resultado.filter(
        (a) =>
          a.codigoActivo.toLowerCase().includes(termino) ||
          a.nombreDescriptivo.toLowerCase().includes(termino) ||
          a.marca.toLowerCase().includes(termino) ||
          (a.modeloActivo ?? '').toLowerCase().includes(termino)
      );
    }

    if (filtroEstado) {
      resultado = resultado.filter((a) => a.estadoActivo === filtroEstado);
    }

    setFiltrados(resultado);
  }, [busqueda, filtroEstado, activos]);

  const estadosUnicos = [...new Set(activos.map((a) => a.estadoActivo))];

  return (
    <div>
      {/* Encabezado */}
      <div className="d-flex justify-content-between align-items-center mb-4">
        <div>
          <h4 className="mb-0 fw-bold">Activos tecnológicos</h4>
          <small className="text-muted">{activos.length} activos registrados</small>
        </div>
        {puedeCrear && (
          <Link to="/activos/nuevo" className="btn btn-primary btn-sm">
            <i className="bi bi-plus-lg me-1"></i>
            Registrar activo
          </Link>
        )}
      </div>

      {/* Filtros */}
      <div className="card border-0 shadow-sm mb-3">
        <div className="card-body py-2">
          <div className="row g-2">
            <div className="col-md-7">
              <div className="input-group input-group-sm">
                <span className="input-group-text">
                  <i className="bi bi-search"></i>
                </span>
                <input
                  type="text"
                  className="form-control"
                  placeholder="Buscar por código, nombre, marca..."
                  value={busqueda}
                  onChange={(e) => setBusqueda(e.target.value)}
                />
              </div>
            </div>
            <div className="col-md-3">
              <select
                className="form-select form-select-sm"
                value={filtroEstado}
                onChange={(e) => setFiltroEstado(e.target.value)}
              >
                <option value="">Todos los estados</option>
                {estadosUnicos.map((e) => (
                  <option key={e} value={e}>{e}</option>
                ))}
              </select>
            </div>
            <div className="col-md-2">
              <button
                className="btn btn-outline-secondary btn-sm w-100"
                onClick={() => { setBusqueda(''); setFiltroEstado(''); }}
              >
                <i className="bi bi-x-circle me-1"></i>
                Limpiar
              </button>
            </div>
          </div>
        </div>
      </div>

      {cargando && <Cargando />}
      {error && <MensajeError mensaje={error} />}

      {!cargando && !error && (
        <div className="card border-0 shadow-sm">
          <div className="card-body p-0">
            {filtrados.length === 0 ? (
              <div className="text-center text-muted py-5">
                <i className="bi bi-inbox fs-2 d-block mb-2"></i>
                {activos.length === 0
                  ? 'No hay activos registrados todavía.'
                  : 'No se encontraron resultados para la búsqueda.'}
              </div>
            ) : (
              <div className="table-responsive">
                <table className="table table-hover align-middle mb-0">
                  <thead className="table-light">
                    <tr>
                      <th>Código</th>
                      <th>Nombre descriptivo</th>
                      <th>Tipo</th>
                      <th>Marca / Modelo</th>
                      <th>Estado</th>
                      <th>Ubicación</th>
                      <th>Asignado a</th>
                      <th></th>
                    </tr>
                  </thead>
                  <tbody>
                    {filtrados.map((activo) => (
                      <tr key={activo.id}>
                        <td>
                          <span className="fw-semibold text-primary">
                            {activo.codigoActivo}
                          </span>
                        </td>
                        <td className="text-truncate" style={{ maxWidth: 200 }}>
                          {activo.nombreDescriptivo}
                        </td>
                        <td className="small text-muted">{activo.tipoActivo}</td>
                        <td className="small">
                          <div>{activo.marca}</div>
                          {activo.modeloActivo && (
                            <div className="text-muted">{activo.modeloActivo}</div>
                          )}
                        </td>
                        <td>
                          <EstadoBadge estado={activo.estadoActivo} />
                        </td>
                        <td className="small text-muted">
                          {activo.espacio ? (
                            <>
                              <div>{activo.espacio}</div>
                              <div className="text-muted">{activo.piso}</div>
                            </>
                          ) : (
                            <span className="text-danger">Sin ubicar</span>
                          )}
                        </td>
                        <td className="small text-muted">
                          {activo.usuarioAsignado ?? '—'}
                        </td>
                        <td>
                          <Link
                            to={`/activos/${activo.id}`}
                            className="btn btn-outline-primary btn-sm"
                          >
                            <i className="bi bi-eye"></i>
                          </Link>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            )}
          </div>
          {filtrados.length > 0 && (
            <div className="card-footer bg-white text-muted small">
              Mostrando {filtrados.length} de {activos.length} activos
            </div>
          )}
        </div>
      )}
    </div>
  );
}

function EstadoBadge({ estado }: { estado: string }) {
  const colores: Record<string, string> = {
    Operativo: 'success',
    'En reparación': 'warning',
    'En bodega': 'secondary',
    'Dado de baja': 'danger',
    'En préstamo': 'info',
  };
  return <span className={`badge bg-${colores[estado] ?? 'secondary'}`}>{estado}</span>;
}
