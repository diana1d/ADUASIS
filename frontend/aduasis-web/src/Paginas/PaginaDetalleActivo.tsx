import { useEffect, useState } from 'react';
import { useParams, Link, useNavigate } from 'react-router-dom';
import Cargando from '../Componentes/comunes/Cargando';
import MensajeError from '../Componentes/comunes/MensajeError';
import servicioActivos from '../Servicios/servicioActivos';
import type { Activo, HistorialActivo } from '../Tipos/activos';
import { usarSesion } from '../Hooks/usarSesion';

/**
 * Página de detalle de un activo tecnológico.
 * Muestra toda la información del activo y su historial de eventos.
 */
export default function PaginaDetalleActivo() {
  const { id } = useParams<{ id: string }>();
  const navegar = useNavigate();
  const { usuario } = usarSesion();

  const [activo, setActivo] = useState<Activo | null>(null);
  const [historial, setHistorial] = useState<HistorialActivo[]>([]);
  const [cargando, setCargando] = useState(true);
  const [error, setError] = useState('');
  const [nota, setNota] = useState('');
  const [guardandoNota, setGuardandoNota] = useState(false);

  const puedeEditar = usuario?.rol === 'Administrador' || usuario?.rol === 'Funcionario';

  useEffect(() => {
    if (!id) return;
    const idNum = parseInt(id);

    Promise.all([
      servicioActivos.obtenerPorId(idNum),
      servicioActivos.obtenerHistorial(idNum),
    ])
      .then(([activoData, historialData]) => {
        setActivo(activoData);
        setHistorial(historialData);
      })
      .catch(() => setError('No se pudo cargar el activo.'))
      .finally(() => setCargando(false));
  }, [id]);

  const manejarAgregarNota = async () => {
    if (!nota.trim() || !id) return;
    setGuardandoNota(true);
    try {
      await servicioActivos.agregarNota(parseInt(id), nota.trim());
      const historialActualizado = await servicioActivos.obtenerHistorial(parseInt(id));
      setHistorial(historialActualizado);
      setNota('');
    } catch {
      alert('No se pudo guardar la nota.');
    } finally {
      setGuardandoNota(false);
    }
  };

  const manejarEliminar = async () => {
    if (!id || !confirm('¿Está seguro de que desea eliminar este activo?')) return;
    try {
      await servicioActivos.eliminar(parseInt(id));
      navegar('/activos');
    } catch {
      alert('No se pudo eliminar el activo.');
    }
  };

  if (cargando) return <Cargando />;
  if (error) return <MensajeError mensaje={error} />;
  if (!activo) return null;

  return (
    <div>
      {/* Encabezado */}
      <div className="d-flex justify-content-between align-items-start mb-4">
        <div>
          <Link to="/activos" className="text-decoration-none text-muted small">
            <i className="bi bi-arrow-left me-1"></i>Volver a activos
          </Link>
          <h4 className="mb-0 fw-bold mt-1">{activo.codigoActivo}</h4>
          <span className="text-muted">{activo.nombreDescriptivo}</span>
        </div>
        <div className="d-flex gap-2">
          {puedeEditar && (
            <Link to={`/activos/${activo.id}/editar`} className="btn btn-outline-primary btn-sm">
              <i className="bi bi-pencil me-1"></i>Editar
            </Link>
          )}
          {usuario?.rol === 'Administrador' && (
            <button className="btn btn-outline-danger btn-sm" onClick={manejarEliminar}>
              <i className="bi bi-trash me-1"></i>Eliminar
            </button>
          )}
        </div>
      </div>

      <div className="row g-3">
        {/* Columna izquierda — Información del activo */}
        <div className="col-lg-7">
          {/* Identificación */}
          <div className="card border-0 shadow-sm mb-3">
            <div className="card-header bg-white border-bottom">
              <h6 className="mb-0 fw-semibold">
                <i className="bi bi-tag me-2 text-primary"></i>Identificación
              </h6>
            </div>
            <div className="card-body">
              <div className="row g-2">
                <CampoInfo label="Código de activo" valor={activo.codigoActivo} />
                {activo.codigoQr && <CampoInfo label="Código QR" valor={activo.codigoQr} />}
                <CampoInfo label="Tipo" valor={activo.tipoActivo} />
                <CampoInfo label="Marca" valor={activo.marca} />
                <CampoInfo label="Modelo" valor={activo.modeloActivo ?? '—'} />
                {activo.color && <CampoInfo label="Color" valor={activo.color} />}
              </div>
              {activo.especificaciones && (
                <div className="mt-2">
                  <div className="small text-muted fw-semibold mb-1">Especificaciones</div>
                  <div className="small bg-light rounded p-2">{activo.especificaciones}</div>
                </div>
              )}
            </div>
          </div>

          {/* Estado y ubicación */}
          <div className="card border-0 shadow-sm mb-3">
            <div className="card-header bg-white border-bottom">
              <h6 className="mb-0 fw-semibold">
                <i className="bi bi-geo-alt me-2 text-primary"></i>Estado y ubicación
              </h6>
            </div>
            <div className="card-body">
              <div className="row g-2">
                <div className="col-6">
                  <div className="small text-muted fw-semibold mb-1">Estado</div>
                  <EstadoBadge estado={activo.estadoActivo} />
                </div>
                <CampoInfo label="Edificio" valor={activo.edificio ?? '—'} />
                <CampoInfo label="Piso" valor={activo.piso ?? '—'} />
                <CampoInfo label="Espacio" valor={activo.espacio ?? '—'} />
                <CampoInfo label="Área" valor={activo.area ?? '—'} />
                {activo.direccionIp && (
                  <CampoInfo label="Dirección IP" valor={activo.direccionIp} />
                )}
              </div>
            </div>
          </div>

          {/* Asignación */}
          <div className="card border-0 shadow-sm mb-3">
            <div className="card-header bg-white border-bottom">
              <h6 className="mb-0 fw-semibold">
                <i className="bi bi-person me-2 text-primary"></i>Asignación
              </h6>
            </div>
            <div className="card-body">
              <div className="row g-2">
                <CampoInfo
                  label="Asignado a"
                  valor={activo.usuarioAsignado ?? 'Sin asignar'}
                />
                {activo.fechaAdquisicion && (
                  <CampoInfo
                    label="Fecha de adquisición"
                    valor={new Date(activo.fechaAdquisicion).toLocaleDateString('es-BO')}
                  />
                )}
              </div>
              {activo.observaciones && (
                <div className="mt-2">
                  <div className="small text-muted fw-semibold mb-1">Observaciones</div>
                  <div className="small bg-light rounded p-2">{activo.observaciones}</div>
                </div>
              )}
            </div>
          </div>
        </div>

        {/* Columna derecha — Historial */}
        <div className="col-lg-5">
          <div className="card border-0 shadow-sm">
            <div className="card-header bg-white border-bottom">
              <h6 className="mb-0 fw-semibold">
                <i className="bi bi-clock-history me-2 text-primary"></i>
                Historial ({historial.length})
              </h6>
            </div>

            {/* Agregar nota manual */}
            {puedeEditar && (
              <div className="card-body border-bottom pb-3">
                <div className="input-group input-group-sm">
                  <input
                    type="text"
                    className="form-control"
                    placeholder="Agregar nota al historial..."
                    value={nota}
                    onChange={(e) => setNota(e.target.value)}
                    onKeyDown={(e) => e.key === 'Enter' && manejarAgregarNota()}
                  />
                  <button
                    className="btn btn-outline-primary"
                    onClick={manejarAgregarNota}
                    disabled={guardandoNota || !nota.trim()}
                  >
                    {guardandoNota ? (
                      <span className="spinner-border spinner-border-sm" />
                    ) : (
                      <i className="bi bi-send"></i>
                    )}
                  </button>
                </div>
              </div>
            )}

            {/* Lista de eventos */}
            <div className="card-body p-0" style={{ maxHeight: 420, overflowY: 'auto' }}>
              {historial.length === 0 ? (
                <div className="text-center text-muted py-4 small">
                  Sin eventos en el historial.
                </div>
              ) : (
                <ul className="list-group list-group-flush">
                  {historial.map((evento) => (
                    <li key={evento.id} className="list-group-item px-3 py-2">
                      <div className="d-flex justify-content-between align-items-start">
                        <div className="d-flex gap-2">
                          <IconoEvento tipo={evento.tipoEvento} esNota={evento.esNotaManual} />
                          <div>
                            <div className="small">{evento.descripcion}</div>
                            {evento.valorAnterior && (
                              <div className="small text-muted">
                                <span className="text-danger">{evento.valorAnterior}</span>
                                <i className="bi bi-arrow-right mx-1"></i>
                                <span className="text-success">{evento.valorNuevo}</span>
                              </div>
                            )}
                            {evento.usuario && (
                              <div className="text-muted" style={{ fontSize: 11 }}>
                                {evento.usuario}
                              </div>
                            )}
                          </div>
                        </div>
                        <div className="text-muted text-end" style={{ fontSize: 11, whiteSpace: 'nowrap' }}>
                          {new Date(evento.creadoEn).toLocaleDateString('es-BO')}
                          <br />
                          {new Date(evento.creadoEn).toLocaleTimeString('es-BO', {
                            hour: '2-digit',
                            minute: '2-digit',
                          })}
                        </div>
                      </div>
                    </li>
                  ))}
                </ul>
              )}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

function CampoInfo({ label, valor }: { label: string; valor: string }) {
  return (
    <div className="col-6">
      <div className="small text-muted fw-semibold mb-0">{label}</div>
      <div className="small">{valor}</div>
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

function IconoEvento({ tipo, esNota }: { tipo: string; esNota: boolean }) {
  if (esNota) return <i className="bi bi-chat-left-text text-secondary mt-1" style={{ fontSize: 13 }}></i>;
  const iconos: Record<string, string> = {
    CREACION: 'bi-plus-circle text-success',
    CAMBIO_ESTADO: 'bi-arrow-repeat text-warning',
    CAMBIO_UBICACION: 'bi-geo-alt text-info',
    CAMBIO_ASIGNACION: 'bi-person-check text-primary',
    BAJA: 'bi-x-circle text-danger',
    ACTUALIZACION: 'bi-pencil text-secondary',
  };
  return <i className={`bi ${iconos[tipo] ?? 'bi-circle text-muted'} mt-1`} style={{ fontSize: 13 }}></i>;
}
