using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Clientes.Commands.CreateClienteCommand
{
    //CREANDO VALIDACION DEL MODELO

                                                               //Modelo que necesita para validar
   public class CreateClienteCommandValidator:AbstractValidator<CreateClienteCommand>
    {

        public CreateClienteCommandValidator()
        {

            RuleFor(p => p.Nombre)//Mapea el nombre de la variable
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
                .MaximumLength(80).WithMessage("{PropertyName} exedio los {MaxLength}  caracteres.");


            RuleFor(p => p.Apellidos)
              .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
              .MaximumLength(80).WithMessage("{PropertyName} exedio los {MaxLength}  caracteres.");

            RuleFor(p => p.FechaNacimiento)
             .NotEmpty().WithMessage("Fecha de Nacimiento no puede ser vacio.");

            //Verificar la expresión regular este bien planteada***
            RuleFor(p => p.Telefono)
          .NotEmpty().WithMessage("El {PropertyName} no puede ser vacio.")
          .Matches(@"(\1) \2-\3 ").WithMessage("{PropertyName} debe cumplir con el formato establecido ")
          .MaximumLength(10).WithMessage("{PropertyName} exedio los {MaxLength}  caracteres.");


            RuleFor(p => p.Email)
             .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
             .EmailAddress().WithMessage("La dirección de Email debe ser valida")
             .MaximumLength(100).WithMessage("{PropertyName} exedio los {MaxLength}  caracteres.");



            RuleFor(p => p.Direccion)
              .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
              .MaximumLength(100).WithMessage("{PropertyName} exedio los {MaxLength}  caracteres.");





        }





    }
}
