import clienteHttp from './clienteHttp';
import type { Activo, SolicitudCrearActivo, HistorialActivo, Asignacion } from '../Tipos/activos';

/**
 * Servicio de activos tecnológicos.
 * Centraliza todas las llamadas HTTP al módulo de activos.
 */
const servicioActivos = {
  obtenerTodos: async (): Promise<Activo[]> => {
    const respuesta = await clienteHttp.get<Activo[]>('/api/activos');
    return respuesta.data;
  },

  obtenerPorId: async (id: number): Promise<Activo> => {
    const respuesta = await clienteHttp.get<Activo>(`/api/activos/${id}`);
    return respuesta.data;
  },

  crear: async (solicitud: SolicitudCrearActivo): Promise<Activo> => {
    const respuesta = await clienteHttp.post<Activo>('/api/activos', solicitud);
    return respuesta.data;
  },

  actualizar: async (id: number, solicitud: Partial<SolicitudCrearActivo>): Promise<Activo> => {
    const respuesta = await clienteHttp.put<Activo>(`/api/activos/${id}`, solicitud);
    return respuesta.data;
  },

  eliminar: async (id: number): Promise<void> => {
    await clienteHttp.delete(`/api/activos/${id}`);
  },

  obtenerHistorial: async (id: number): Promise<HistorialActivo[]> => {
    const respuesta = await clienteHttp.get<HistorialActivo[]>(`/api/activos/${id}/historial`);
    return respuesta.data;
  },

  agregarNota: async (id: number, descripcion: string): Promise<void> => {
    await clienteHttp.post(`/api/activos/${id}/historial/notas`, { descripcion });
  },

  obtenerAsignaciones: async (id: number): Promise<Asignacion[]> => {
    const respuesta = await clienteHttp.get<Asignacion[]>(`/api/activos/${id}/asignaciones`);
    return respuesta.data;
  },
};

export default servicioActivos;
