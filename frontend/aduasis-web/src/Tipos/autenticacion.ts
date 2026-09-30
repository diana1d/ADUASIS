/**
 * Tipos relacionados con autenticación y sesión del usuario.
 */

export interface UsuarioAutenticado {
  id: number;
  nombreCompleto: string;
  nombreUsuario: string;
  correo: string;
  rol: 'Administrador' | 'Funcionario' | 'Pasante';
}

export interface RespuestaAutenticacion {
  accessToken: string;
  tokenRefresco: string;
  expiraEn: string;
  usuario: UsuarioAutenticado;
}

export interface SolicitudLogin {
  credencial: string;
  contrasena: string;
}
