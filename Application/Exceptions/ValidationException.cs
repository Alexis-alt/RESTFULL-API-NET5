using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace Application.Exceptions
{
    public class ValidationException : Exception
    {
                                       //Se le pasa a la propiedada ErrorMessage de la clasePadre un mensaje de error string
        public ValidationException() : base("Se han producido uno o más errores de validación")
        {
            Errors = new List<string>();
        }


        public List<string> Errors { get; }

        //Constructor que se usa para cuando recibimos una lista de errores
        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            //Por cada error que se reciba se guarda su mensaje 
            foreach (var failure in failures)
            {
                Errors.Add(failure.ErrorMessage);
            }
        }



    }
}
