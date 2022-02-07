using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Behaviours
{

                                                           //Por un pipeline pasa la request antes de entrar al servidor 
                                                           //Y la respuesta antes de llegar al cliente 

    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;




        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            //Contiene una colección de validaciones
            //Las cuales configuramos manualmente para cada Model
            _validators = validators;
        }





       
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            //Si hay alguna validación
            if(_validators.Any())
            {

                var context = new FluentValidation.ValidationContext<TRequest>(request);

                //Regresa una lista de validadores cada uno con su lista de errores
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                //Hace una lita de los errores
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                //Si se encuentra algun error se produce una exception de tipo ValidationException
                if (failures.Count != 0)
                    throw new Exceptions.ValidationException(failures);
            }

            //Si no se encuentra niguna exception se pasa al siguiente pipe
            //De eso se encarga el delegado next() que recibimos por parametro
            return await next();
        }
    }
}
