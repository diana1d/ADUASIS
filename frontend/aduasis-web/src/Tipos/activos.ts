/**
 * Tipos relacionados con activos tecnológicos.
 */

export interface Activo {
  id: number;
  codigoActivo: string;
  codigoQr?: string;
  nombreDescriptivo: string;
  especificaciones?: string;
  color?: string;

  tipoActivoId: number;
  tipoActivo: string;
  marcaId: number;
  marca: string;
  modeloActivoId?: number;
  modeloActivo?: string;

  estadoActivoId: number;
  estadoActivo: string;

  espacioId?: number;
  espacio?: string;
  areaId?: number;
  area?: string;
  piso?: string;
  edificio?: string;

  usuarioAsignadoId?: number;
  usuarioAsignado?: string;

  direccionIp?: string;
  fechaAdquisicion?: string;
  observaciones?: string;

  creadoEn: string;
  actualizadoEn: string;
}

export interface SolicitudCrearActivo {
  codigoActivo: string;
  codigoQr?: string;
  nombreDescriptivo: string;
  especificaciones?: string;
  color?: string;
  tipoActivoId: number;
  marcaId: number;
  modeloActivoId?: number;
  estadoActivoId: number;
  espacioId?: number;
  areaId?: number;
  usuarioAsignadoId?: number;
  direccionIp?: string;
  fechaAdquisicion?: string;
  observaciones?: string;
}

export interface HistorialActivo {
  id: number;
  tipoEvento: string;
  descripcion: string;
  valorAnterior?: string;
  valorNuevo?: string;
  esNotaManual: boolean;
  usuario?: string;
  creadoEn: string;
}

export interface Asignacion {
  id: number;
  activoId: number;
  usuarioId?: number;
  nombreUsuario?: string;
  fechaInicio: string;
  fechaFin?: string;
  estaActiva: boolean;
  observaciones?: string;
  registradoPor?: string;
}
