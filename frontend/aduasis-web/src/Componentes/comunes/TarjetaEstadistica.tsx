interface PropsTarjetaEstadistica {
  titulo: string;
  valor: number | string;
  icono: string;
  colorIcono?: string;
}

/**
 * Tarjeta de estadística para el dashboard.
 * Muestra un número destacado con un ícono y título.
 */
export default function TarjetaEstadistica({
  titulo,
  valor,
  icono,
  colorIcono = 'text-primary',
}: PropsTarjetaEstadistica) {
  return (
    <div className="card h-100 shadow-sm border-0">
      <div className="card-body d-flex align-items-center gap-3">
        <div className={`fs-1 ${colorIcono}`}>
          <i className={`bi ${icono}`}></i>
        </div>
        <div>
          <div className="fs-2 fw-bold">{valor}</div>
          <div className="text-muted small">{titulo}</div>
        </div>
      </div>
    </div>
  );
}
