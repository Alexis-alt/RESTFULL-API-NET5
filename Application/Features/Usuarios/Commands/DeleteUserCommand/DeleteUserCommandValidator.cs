using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Usuarios.Commands.DeleteUserCommand
{
    class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {

        public DeleteUserCommandValidator()
        {
            RuleFor(p => p.IdUser)
                 .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");
        }


    }
}
