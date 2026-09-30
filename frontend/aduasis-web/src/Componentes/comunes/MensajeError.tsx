interface PropsMensajeError {
  mensaje: string;
}

/**
 * Alerta de error reutilizable.
 */
export default function MensajeError({ mensaje }: PropsMensajeError) {
  return (
    <div className="alert alert-danger d-flex align-items-center gap-2" role="alert">
      <i className="bi bi-exclamation-triangle-fill"></i>
      <span>{mensaje}</span>
    </div>
  );
}
