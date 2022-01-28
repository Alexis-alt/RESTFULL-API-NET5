﻿using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Behaviours
{
    //Se canalizan previamente las Solicitudes antes de procesarse en el server
    //Se valida cada peticion y se controlan las exceptions a través de una clase propia creada para mostrar exceptions

    class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {

        private readonly IEnumerable<IValidator<TRequest>> _validators;


        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {

            _validators = validators;


        }




        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {

            if (_validators.Any())
            {

                var context = new FluentValidation.ValidationContext<TRequest>(request);
                var validationsResult = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationsResult.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if(failures.Count() != 0)
                {

                    throw new ValidationException(failures);

                }

            }

            return await next();

        }
    }
}