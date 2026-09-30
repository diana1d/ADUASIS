import { useState, type FormEvent } from 'react';
import { useNavigate } from 'react-router-dom';
import { usarSesion } from '../Hooks/usarSesion';
import servicioAutenticacion from '../Servicios/servicioAutenticacion';

/**
 * Página de inicio de sesión.
 * Acepta correo o nombre de usuario como credencial.
 */
export default function PaginaLogin() {
  const [credencial, setCredencial] = useState('');
  const [contrasena, setContrasena] = useState('');
  const [error, setError] = useState('');
  const [cargando, setCargando] = useState(false);

  const { iniciarSesion } = usarSesion();
  const navegar = useNavigate();

  const manejarLogin = async (e: FormEvent) => {
    e.preventDefault();
    setError('');
    setCargando(true);

    try {
      const respuesta = await servicioAutenticacion.login({ credencial, contrasena });
      iniciarSesion(respuesta.accessToken, respuesta.tokenRefresco, respuesta.usuario);
      navegar('/panel');
    } catch {
      setError('Credenciales inválidas. Verifica tu usuario y contraseña.');
    } finally {
      setCargando(false);
    }
  };

  return (
    <div
      className="min-vh-100 d-flex align-items-center justify-content-center"
      style={{ backgroundColor: '#f0f4f8' }}
    >
      <div className="card shadow border-0" style={{ width: '100%', maxWidth: 420 }}>
        {/* Encabezado */}
        <div
          className="card-header text-white text-center py-4"
          style={{ backgroundColor: '#1a3a5c' }}
        >
          <h4 className="mb-0 fw-bold">ADUASIS</h4>
          <small className="opacity-75">Aduana Nacional – Gerencia Regional La Paz</small>
        </div>

        {/* Formulario */}
        <div className="card-body p-4">
          <h6 className="text-muted mb-4 text-center">Iniciar sesión</h6>

          {error && (
            <div className="alert alert-danger d-flex align-items-center gap-2 py-2" role="alert">
              <i className="bi bi-exclamation-triangle-fill"></i>
              <small>{error}</small>
            </div>
          )}

          <form onSubmit={manejarLogin}>
            <div className="mb-3">
              <label htmlFor="credencial" className="form-label small fw-semibold">
                Correo o nombre de usuario
              </label>
              <div className="input-group">
                <span className="input-group-text">
                  <i className="bi bi-person"></i>
                </span>
                <input
                  id="credencial"
                  type="text"
                  className="form-control"
                  placeholder="admin o admin@aduasis.gob.bo"
                  value={credencial}
                  onChange={(e) => setCredencial(e.target.value)}
                  required
                  autoFocus
                />
              </div>
            </div>

            <div className="mb-4">
              <label htmlFor="contrasena" className="form-label small fw-semibold">
                Contraseña
              </label>
              <div className="input-group">
                <span className="input-group-text">
                  <i className="bi bi-lock"></i>
                </span>
                <input
                  id="contrasena"
                  type="password"
                  className="form-control"
                  placeholder="••••••••"
                  value={contrasena}
                  onChange={(e) => setContrasena(e.target.value)}
                  required
                />
              </div>
            </div>

            <button
              type="submit"
              className="btn btn-primary w-100"
              style={{ backgroundColor: '#1a3a5c', borderColor: '#1a3a5c' }}
              disabled={cargando}
            >
              {cargando ? (
                <>
                  <span className="spinner-border spinner-border-sm me-2" />
                  Ingresando...
                </>
              ) : (
                <>
                  <i className="bi bi-box-arrow-in-right me-2"></i>
                  Ingresar
                </>
              )}
            </button>
          </form>
        </div>

        <div className="card-footer text-center text-muted py-2">
          <small>Sistema de Gestión de Activos Tecnológicos</small>
        </div>
      </div>
    </div>
  );
}
