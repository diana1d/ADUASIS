using Aduasis.Api.DTOs.Solicitudes;
using FluentValidation;

namespace Aduasis.Api.Validaciones;

/// <summary>
/// Reglas de validación para la solicitud de login.
/// FluentValidation las ejecuta automáticamente antes de que
/// el controlador procese la solicitud.
/// </summary>
public class ValidadorLogin : AbstractValidator<SolicitudLogin>
{
    public ValidadorLogin()
    {
        RuleFor(x => x.Credencial)
            .NotEmpty().WithMessage("El correo o nombre de usuario es obligatorio.")
            .MaximumLength(150).WithMessage("La credencial no puede exceder 150 caracteres.");

        RuleFor(x => x.Contrasena)
            .NotEmpty().WithMessage("La contraseña es obligatoria.")
            .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.")
            .MaximumLength(100).WithMessage("La contraseña no puede exceder 100 caracteres.");
    }
}
