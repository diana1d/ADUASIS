/**
 * clienteApi.ts
 *
 * Cliente HTTP centralizado para todas las llamadas a la API de ADUASIS.
 * Todas las solicitudes al backend pasan por aquí.
 *
 * Ventaja: si cambia la URL base o necesitamos agregar headers comunes
 * (como el token JWT), solo lo modificamos en un lugar.
 */

const URL_BASE_API = import.meta.env.VITE_API_URL ?? 'http://localhost:5020';

interface OpcionesSolicitud extends RequestInit {
  token?: string;
}

async function solicitar<T>(
  ruta: string,
  opciones: OpcionesSolicitud = {}
): Promise<T> {
  const { token, ...opcionesHttp } = opciones;

  const cabeceras: HeadersInit = {
    'Content-Type': 'application/json',
    ...(token ? { Authorization: `Bearer ${token}` } : {}),
    ...(opcionesHttp.headers ?? {}),
  };

  const respuesta = await fetch(`${URL_BASE_API}${ruta}`, {
    ...opcionesHttp,
    headers: cabeceras,
  });

  if (!respuesta.ok) {
    const error = await respuesta.text();
    throw new Error(`Error ${respuesta.status}: ${error}`);
  }

  // Si la respuesta es 204 No Content, retornar vacío
  if (respuesta.status === 204) {
    return undefined as T;
  }

  return respuesta.json() as Promise<T>;
}

export const clienteApi = {
  obtener: <T>(ruta: string, token?: string) =>
    solicitar<T>(ruta, { method: 'GET', token }),

  crear: <T>(ruta: string, cuerpo: unknown, token?: string) =>
    solicitar<T>(ruta, {
      method: 'POST',
      body: JSON.stringify(cuerpo),
      token,
    }),

  actualizar: <T>(ruta: string, cuerpo: unknown, token?: string) =>
    solicitar<T>(ruta, {
      method: 'PUT',
      body: JSON.stringify(cuerpo),
      token,
    }),

  eliminar: <T>(ruta: string, token?: string) =>
    solicitar<T>(ruta, { method: 'DELETE', token }),
};
