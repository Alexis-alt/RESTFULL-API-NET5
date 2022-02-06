using FluentValidation;

namespace Application.Feautres.Clientes.Commands.CreateClienteCommand
{
                                                                   //Obj que se va a validar proveniente del Cliente
    public class CreateClienteCommandValidator : AbstractValidator<CreateClienteCommand>
    {
        //Clase que hereda de una Clase del paquete FluentVlidation

        //Se encarga se validar en el Middleware que los campos necesarios para un Command o Query esten correctos
        //Si no estan correctos se envia un mensaje con el error del dato

        public CreateClienteCommandValidator()
        {
            RuleFor(p => p.Nombre)
                   .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
                   .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

            RuleFor(p => p.Apellido)
               .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
               .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

            RuleFor(p => p.FechaNacimiento)
               .NotEmpty().WithMessage("Fecha de Nacimiento no puede ser vacia.");

            RuleFor(p => p.Telefono)
               .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
               .Matches(@"^\d{4}-\d{4}$").WithMessage("{PropertyName} debe cumplir el formato 0000-0000")
               .MaximumLength(9).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

            RuleFor(p => p.Email)
               .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
               .EmailAddress().WithMessage("{PropertyName} debe ser una direccion de email valida")
               .MaximumLength(100).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

            RuleFor(p => p.Direccion)
               .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
               .MaximumLength(120).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");
        }
    }
}
