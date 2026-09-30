/**
 * Tipos para los catálogos del sistema.
 */

export interface TipoActivo {
  id: number;
  nombre: string;
  descripcion?: string;
}

export interface Marca {
  id: number;
  nombre: string;
}

export interface ModeloActivo {
  id: number;
  marcaId: number;
  nombre: string;
}

export interface EstadoActivo {
  id: number;
  nombre: string;
  descripcion?: string;
}

export interface Edificio {
  id: number;
  nombre: string;
}

export interface Piso {
  id: number;
  edificioId: number;
  nombre: string;
  orden: number;
}

export interface Area {
  id: number;
  pisoId: number;
  nombre: string;
}

export interface Espacio {
  id: number;
  pisoId: number;
  nombre: string;
}
