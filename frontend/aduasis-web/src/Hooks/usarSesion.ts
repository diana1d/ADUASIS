import { create } from 'zustand';
import type { UsuarioAutenticado } from '../Tipos/autenticacion';

/**
 * Estado global de la sesión del usuario.
 * Almacena el token JWT y los datos del usuario autenticado.
 *
 * Zustand crea una "tienda" global accesible desde cualquier componente
 * sin necesidad de prop-drilling ni Context API complejo.
 */

interface EstadoSesion {
  token: string | null;
  usuario: UsuarioAutenticado | null;
  iniciarSesion: (token: string, tokenRefresco: string, usuario: UsuarioAutenticado) => void;
  cerrarSesion: () => void;
  estaAutenticado: () => boolean;
}

export const usarSesion = create<EstadoSesion>((set, get) => ({
  // Intentar recuperar sesión guardada en localStorage al cargar
  token: localStorage.getItem('aduasis_token'),
  usuario: (() => {
    const datos = localStorage.getItem('aduasis_usuario');
    return datos ? JSON.parse(datos) : null;
  })(),

  iniciarSesion: (token, tokenRefresco, usuario) => {
    localStorage.setItem('aduasis_token', token);
    localStorage.setItem('aduasis_token_refresco', tokenRefresco);
    localStorage.setItem('aduasis_usuario', JSON.stringify(usuario));
    set({ token, usuario });
  },

  cerrarSesion: () => {
    localStorage.removeItem('aduasis_token');
    localStorage.removeItem('aduasis_token_refresco');
    localStorage.removeItem('aduasis_usuario');
    set({ token: null, usuario: null });
  },

  estaAutenticado: () => {
    const { token } = get();
    return token !== null;
  },
}));
