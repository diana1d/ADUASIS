/**
 * comunes.ts
 *
 * Tipos e interfaces compartidas en toda la aplicación.
 * Los tipos específicos de cada módulo se definirán en sus
 * respectivas carpetas dentro de Caracteristicas/.
 */

/** Respuesta estándar paginada del API */
export interface RespuestaPaginada<T> {
  datos: T[];
  total: number;
  pagina: number;
  tamanoPagina: number;
  totalPaginas: number;
}

/** Estado genérico de una operación asíncrona */
export interface EstadoCarga {
  cargando: boolean;
  error: string | null;
}

/** Estructura de error devuelta por la API */
export interface ErrorApi {
  mensaje: string;
  detalles?: string[];
  codigo?: string;
}
