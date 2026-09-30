/**
 * Spinner de carga centrado en pantalla.
 * Se usa mientras se espera una respuesta del servidor.
 */
export default function Cargando() {
  return (
    <div className="d-flex justify-content-center align-items-center py-5">
      <div className="spinner-border text-primary" role="status">
        <span className="visually-hidden">Cargando...</span>
      </div>
    </div>
  );
}
