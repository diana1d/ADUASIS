import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import TarjetaEstadistica from '../Componentes/comunes/TarjetaEstadistica';
import Cargando from '../Componentes/comunes/Cargando';
import MensajeError from '../Componentes/comunes/MensajeError';
import servicioActivos from '../Servicios/servicioActivos';
import type { Activo } from '../Tipos/activos';
import { usarSesion } from '../Hooks/usarSesion';

/**
 * Panel principal (dashboard).
 * Muestra estadísticas generales de los activos del sistema.
 */
export default function PaginaPanel() {
  const [activos, setActivos] = useState<Activo[]>([]);
  const [cargando, setCargando] = useState(true);
  const [error, setError] = useState('');
  const { usuario } = usarSesion();

  useEffect(() => {
    servicioActivos
      .obtenerTodos()
      .then(setActivos)
      .catch(() => setError('No se pudieron cargar los datos del panel.'))
      .finally(() => setCargando(false));
  }, []);

  // Calcular estadísticas desde los activos cargados
  const totalActivos = activos.length;
  const operativos = activos.filter((a) => a.estadoActivo === 'Operativo').length;
  const enReparacion = activos.filter((a) => a.estadoActivo === 'En reparación').length;
  const sinUbicacion = activos.filter((a) => !a.espacioId).length;

  return (
    <div>
      {/* Encabezado */}
      <div className="d-flex justify-content-between align-items-center mb-4">
        <div>
          <h4 className="mb-0 fw-bold">Panel de control</h4>
          <small className="text-muted">
            Bienvenido, {usuario?.nombreCompleto}
          </small>
        </div>
        <Link to="/activos" className="btn btn-primary btn-sm">
          <i className="bi bi-pc-display me-1"></i>
          Ver activos
        </Link>
      </div>

      {cargando && <Cargando />}
      {error && <MensajeError mensaje={error} />}

      {!cargando && !error && (
        <>
          {/* Estadísticas */}
          <div className="row g-3 mb-4">
            <div className="col-sm-6 col-xl-3">
              <TarjetaEstadistica
                titulo="Total de activos"
                valor={totalActivos}
                icono="bi-collection"
                colorIcono="text-primary"
              />
            </div>
            <div className="col-sm-6 col-xl-3">
              <TarjetaEstadistica
                titulo="Operativos"
                valor={operativos}
                icono="bi-check-circle"
                colorIcono="text-success"
              />
            </div>
            <div className="col-sm-6 col-xl-3">
              <TarjetaEstadistica
                titulo="En reparación"
                valor={enReparacion}
                icono="bi-tools"
                colorIcono="text-warning"
              />
            </div>
            <div className="col-sm-6 col-xl-3">
              <TarjetaEstadistica
                titulo="Sin ubicación"
                valor={sinUbicacion}
                icono="bi-geo-alt"
                colorIcono="text-danger"
              />
            </div>
          </div>

          {/* Últimos activos registrados */}
          <div className="card border-0 shadow-sm">
            <div className="card-header bg-white border-bottom">
              <h6 className="mb-0 fw-semibold">Últimos activos registrados</h6>
            </div>
            <div className="card-body p-0">
              {activos.length === 0 ? (
                <div className="text-center text-muted py-4">
                  <i className="bi bi-inbox fs-3 d-block mb-2"></i>
                  No hay activos registrados todavía.
                </div>
              ) : (
                <div className="table-responsive">
                  <table className="table table-hover mb-0">
                    <thead className="table-light">
                      <tr>
                        <th>Código</th>
                        <th>Nombre</th>
                        <th>Tipo</th>
                        <th>Estado</th>
                        <th>Ubicación</th>
                      </tr>
                    </thead>
                    <tbody>
                      {activos.slice(0, 8).map((activo) => (
                        <tr key={activo.id}>
                          <td>
                            <Link
                              to={`/activos/${activo.id}`}
                              className="text-decoration-none fw-semibold"
                            >
                              {activo.codigoActivo}
                            </Link>
                          </td>
                          <td className="text-truncate" style={{ maxWidth: 200 }}>
                            {activo.nombreDescriptivo}
                          </td>
                          <td>{activo.tipoActivo}</td>
                          <td>
                            <EstadoBadge estado={activo.estadoActivo} />
                          </td>
                          <td className="text-muted small">
                            {activo.espacio ?? '—'}
                          </td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
              )}
            </div>
            {activos.length > 8 && (
              <div className="card-footer bg-white text-center">
                <Link to="/activos" className="text-decoration-none small">
                  Ver todos los activos ({activos.length})
                </Link>
              </div>
            )}
          </div>
        </>
      )}
    </div>
  );
}

/** Badge de color según el estado del activo */
function EstadoBadge({ estado }: { estado: string }) {
  const colores: Record<string, string> = {
    Operativo: 'success',
    'En reparación': 'warning',
    'En bodega': 'secondary',
    'Dado de baja': 'danger',
    'En préstamo': 'info',
  };
  const color = colores[estado] ?? 'secondary';
  return <span className={`badge bg-${color}`}>{estado}</span>;
}
