import { Navigate } from 'react-router-dom';
import { usarSesion } from '../../Hooks/usarSesion';

interface PropsRutaProtegida {
  children: React.ReactNode;
}

/**
 * Componente guardián de rutas.
 * Si el usuario no está autenticado, redirige al login.
 * Si está autenticado, renderiza el contenido normalmente.
 */
export default function RutaProtegida({ children }: PropsRutaProtegida) {
  const { estaAutenticado } = usarSesion();

  if (!estaAutenticado()) {
    return <Navigate to="/" replace />;
  }

  return <>{children}</>;
}
