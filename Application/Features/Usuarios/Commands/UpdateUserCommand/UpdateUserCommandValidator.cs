using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Usuarios.Commands.UpdateUserCommand
{
    class UpdateUserCommandValidator: AbstractValidator<UpdateUserCommand>
    {

        public UpdateUserCommandValidator()
        {
            RuleFor(p => p.IdUser)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

            RuleFor(p => p.Nombre)
               .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
               .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

            RuleFor(p => p.Apellido)
               .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
               .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");


            RuleFor(p => p.Email)
               .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
               .EmailAddress().WithMessage("{PropertyName} debe ser una direccion de email valida")
               .MaximumLength(100).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

            RuleFor(p => p.UserName)
               .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.")
               .MaximumLength(10).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");
        }


    }
}
