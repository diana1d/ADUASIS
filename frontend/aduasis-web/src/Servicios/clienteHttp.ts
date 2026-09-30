import axios from 'axios';

/**
 * Cliente HTTP centralizado basado en Axios.
 * Reemplaza el clienteApi.ts manual creado en el Sprint 0.
 *
 * Ventajas de Axios sobre fetch:
 * - Interceptores para agregar el token automáticamente
 * - Manejo de errores más sencillo
 * - Serialización/deserialización JSON automática
 */

const clienteHttp = axios.create({
  baseURL: import.meta.env.VITE_API_URL ?? 'http://localhost:5020',
  headers: {
    'Content-Type': 'application/json',
  },
});

// Interceptor de solicitud: agrega el token JWT a cada petición
clienteHttp.interceptors.request.use((config) => {
  const token = localStorage.getItem('aduasis_token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

// Interceptor de respuesta: maneja errores globales
clienteHttp.interceptors.response.use(
  (respuesta) => respuesta,
  (error) => {
    if (error.response?.status === 401) {
      // Token expirado o inválido → limpiar sesión y redirigir al login
      localStorage.removeItem('aduasis_token');
      localStorage.removeItem('aduasis_token_refresco');
      localStorage.removeItem('aduasis_usuario');
      window.location.href = '/';
    }
    return Promise.reject(error);
  }
);

export default clienteHttp;
