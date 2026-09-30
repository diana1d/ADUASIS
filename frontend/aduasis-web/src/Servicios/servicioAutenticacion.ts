import clienteHttp from './clienteHttp';
import type { RespuestaAutenticacion, SolicitudLogin } from '../Tipos/autenticacion';

/**
 * Servicio de autenticación.
 * Centraliza todas las llamadas HTTP relacionadas con login/logout.
 */
const servicioAutenticacion = {
  login: async (solicitud: SolicitudLogin): Promise<RespuestaAutenticacion> => {
    const respuesta = await clienteHttp.post<RespuestaAutenticacion>(
      '/api/autenticacion/login',
      solicitud
    );
    return respuesta.data;
  },

  cerrarSesion: async (): Promise<void> => {
    const tokenRefresco = localStorage.getItem('aduasis_token_refresco');
    if (tokenRefresco) {
      await clienteHttp.post('/api/autenticacion/cerrar-sesion', {
        tokenRefresco,
      });
    }
  },
};

export default servicioAutenticacion;
