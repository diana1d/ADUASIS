import clienteHttp from './clienteHttp';
import type { TipoActivo, Marca, ModeloActivo, EstadoActivo, Edificio, Piso, Area, Espacio } from '../Tipos/catalogos';

/**
 * Servicio de catálogos.
 * Carga los datos de referencia necesarios para los formularios.
 */
const servicioCatalogos = {
  obtenerTiposActivo: async (): Promise<TipoActivo[]> => {
    const r = await clienteHttp.get<TipoActivo[]>('/api/catalogos/tipos-activo');
    return r.data;
  },

  obtenerMarcas: async (): Promise<Marca[]> => {
    const r = await clienteHttp.get<Marca[]>('/api/catalogos/marcas');
    return r.data;
  },

  obtenerModelos: async (marcaId?: number): Promise<ModeloActivo[]> => {
    const url = marcaId ? `/api/catalogos/modelos?marcaId=${marcaId}` : '/api/catalogos/modelos';
    const r = await clienteHttp.get<ModeloActivo[]>(url);
    return r.data;
  },

  obtenerEstadosActivo: async (): Promise<EstadoActivo[]> => {
    const r = await clienteHttp.get<EstadoActivo[]>('/api/catalogos/estados-activo');
    return r.data;
  },

  obtenerEdificios: async (): Promise<Edificio[]> => {
    const r = await clienteHttp.get<Edificio[]>('/api/ubicaciones/edificios');
    return r.data;
  },

  obtenerPisos: async (edificioId?: number): Promise<Piso[]> => {
    const url = edificioId ? `/api/ubicaciones/pisos?edificioId=${edificioId}` : '/api/ubicaciones/pisos';
    const r = await clienteHttp.get<Piso[]>(url);
    return r.data;
  },

  obtenerAreas: async (pisoId?: number): Promise<Area[]> => {
    const url = pisoId ? `/api/ubicaciones/areas?pisoId=${pisoId}` : '/api/ubicaciones/areas';
    const r = await clienteHttp.get<Area[]>(url);
    return r.data;
  },

  obtenerEspacios: async (pisoId?: number): Promise<Espacio[]> => {
    const url = pisoId ? `/api/ubicaciones/espacios?pisoId=${pisoId}` : '/api/ubicaciones/espacios';
    const r = await clienteHttp.get<Espacio[]>(url);
    return r.data;
  },
};

export default servicioCatalogos;
