import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap-icons/font/bootstrap-icons.css';

import RutaProtegida from './Componentes/comunes/RutaProtegida';
import LayoutPrincipal from './Layouts/LayoutPrincipal';
import PaginaLogin from './Paginas/PaginaLogin';
import PaginaPanel from './Paginas/PaginaPanel';
import PaginaActivos from './Paginas/PaginaActivos';
import PaginaDetalleActivo from './Paginas/PaginaDetalleActivo';

/**
 * Componente raíz de la aplicación.
 * Define la estructura de rutas y navegación.
 *
 * Rutas públicas:  /  (login)
 * Rutas protegidas: /panel, /activos, /activos/:id
 */
export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        {/* Ruta pública — Login */}
        <Route path="/" element={<PaginaLogin />} />

        {/* Rutas protegidas — requieren autenticación */}
        <Route
          element={
            <RutaProtegida>
              <LayoutPrincipal />
            </RutaProtegida>
          }
        >
          <Route path="/panel" element={<PaginaPanel />} />
          <Route path="/activos" element={<PaginaActivos />} />
          <Route path="/activos/:id" element={<PaginaDetalleActivo />} />
          {/* Ruta por defecto dentro del layout */}
          <Route path="/panel/*" element={<Navigate to="/panel" replace />} />
        </Route>

        {/* Cualquier ruta desconocida redirige al login */}
        <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>
    </BrowserRouter>
  );
}
