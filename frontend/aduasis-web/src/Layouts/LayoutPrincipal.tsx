import { NavLink, Outlet, useNavigate } from 'react-router-dom';
import { usarSesion } from '../Hooks/usarSesion';
import servicioAutenticacion from '../Servicios/servicioAutenticacion';

/**
 * Layout principal de la aplicación.
 * Contiene la barra de navegación lateral y el área de contenido.
 * Renderiza el componente hijo correspondiente a la ruta activa
 * mediante <Outlet /> de react-router-dom.
 */
export default function LayoutPrincipal() {
  const { usuario, cerrarSesion } = usarSesion();
  const navegar = useNavigate();

  const manejarCerrarSesion = async () => {
    try {
      await servicioAutenticacion.cerrarSesion();
    } finally {
      cerrarSesion();
      navegar('/');
    }
  };

  return (
    <div className="d-flex vh-100 overflow-hidden">
      {/* ── Barra lateral ── */}
      <nav
        className="d-flex flex-column flex-shrink-0 p-3 text-white"
        style={{ width: '240px', backgroundColor: '#1a3a5c' }}
      >
        {/* Logo / nombre del sistema */}
        <div className="mb-4 text-center">
          <div className="fw-bold fs-5">ADUASIS</div>
          <div className="text-white-50 small">Aduana Nacional</div>
        </div>

        <hr className="border-secondary" />

        {/* Menú de navegación */}
        <ul className="nav nav-pills flex-column gap-1 mb-auto">
          <li className="nav-item">
            <NavLink
              to="/panel"
              className={({ isActive }) =>
                `nav-link text-white ${isActive ? 'active' : 'opacity-75'}`
              }
            >
              <i className="bi bi-speedometer2 me-2"></i>
              Panel
            </NavLink>
          </li>
          <li className="nav-item">
            <NavLink
              to="/activos"
              className={({ isActive }) =>
                `nav-link text-white ${isActive ? 'active' : 'opacity-75'}`
              }
            >
              <i className="bi bi-pc-display me-2"></i>
              Activos
            </NavLink>
          </li>
          {usuario?.rol === 'Administrador' && (
            <li className="nav-item">
              <NavLink
                to="/ubicaciones"
                className={({ isActive }) =>
                  `nav-link text-white ${isActive ? 'active' : 'opacity-75'}`
                }
              >
                <i className="bi bi-geo-alt me-2"></i>
                Ubicaciones
              </NavLink>
            </li>
          )}
        </ul>

        <hr className="border-secondary" />

        {/* Información del usuario */}
        <div className="d-flex align-items-center gap-2 mb-2">
          <div
            className="rounded-circle bg-white d-flex align-items-center justify-content-center text-primary fw-bold"
            style={{ width: 36, height: 36, fontSize: 14 }}
          >
            {usuario?.nombreCompleto?.charAt(0).toUpperCase() ?? 'U'}
          </div>
          <div>
            <div className="small fw-semibold text-truncate" style={{ maxWidth: 140 }}>
              {usuario?.nombreCompleto}
            </div>
            <div className="text-white-50" style={{ fontSize: 11 }}>
              {usuario?.rol}
            </div>
          </div>
        </div>

        <button
          className="btn btn-outline-light btn-sm"
          onClick={manejarCerrarSesion}
        >
          <i className="bi bi-box-arrow-left me-1"></i>
          Cerrar sesión
        </button>
      </nav>

      {/* ── Área de contenido ── */}
      <main className="flex-grow-1 overflow-auto bg-light">
        <div className="p-4">
          <Outlet />
        </div>
      </main>
    </div>
  );
}
